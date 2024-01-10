using IDataSphere.Extensions;
using Microsoft.EntityFrameworkCore;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.Repositotys;
using System.Linq.Expressions;
using System.Reflection;
using UtilityToolkit.Utils;

namespace IDataSphere.Extensions
{

    /// <summary>
    /// Linq查询拓展
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// 根据等式决定是否执行右边表达式
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sources">查询数据源</param>
        /// <param name="result">成立条件</param>
        /// <param name="expression">执行的表达式</param>
        /// <returns></returns>
        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> sources, bool result, Expression<Func<TSource, bool>> expression)
        {
            if (result)
            {
                return sources.Where(expression);
            }
            else
            {
                return sources;
            }
        }

        /// <summary>
        /// 无视租户过滤条件
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sources"></param>
        /// <returns></returns>
        public static IQueryable<TSource> IgnoreTenantFilter<TSource>(this IQueryable<TSource> sources) where TSource : EntityBaseDO
        {
            return sources.IgnoreQueryFilters().Where(p => !p.IsDeleted);
        }


        /// <summary>
        /// 单个表转为查询语句
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="SearchInput"></typeparam>
        /// <param name="sources">查询数据源</param>
        /// <param name="input">成立条件</param>
        /// <returns></returns>
        public static IQueryable<TSource> AddSearchCriteria<TSource, SearchInput>(this IQueryable<TSource> sources, SearchInput input)
        {
            ParameterExpression p = Expression.Parameter(typeof(TSource), "p");
            var propertyInfos = input.GetType().GetProperties();
            Expression result = Expression.MakeBinary(ExpressionType.Equal, Expression.Constant(1), Expression.Constant(1));
            foreach (var item in propertyInfos)
            {
                if (item.Name == nameof(PageInput.PageSize) || item.Name == nameof(PageInput.PageNo))
                {
                    continue;
                }
                // 构建成员表达式
                string fieldName = item.Name.Replace("StartTime", "").Replace("EndTime", "");
                object value = item.GetValue(input);
                ConstantExpression constantExpression = Expression.Constant(value);
                MemberExpression memberExpression = Expression.PropertyOrField(p, fieldName);
                BinaryExpression binaryExpression;
                MethodInfo like = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
                string typeName = item.PropertyType.Name;
                switch (typeName)
                {
                    case "Int16":
                    case "Int32":
                    case "Int64":
                    case "Double":
                    case "Decimal":
                        if (value != null && !value.ToString().Equals("0"))
                        {
                            binaryExpression = Expression.MakeBinary(ExpressionType.Equal, memberExpression, constantExpression);
                            result = Expression.AndAlso(result, binaryExpression);
                        }
                        break;
                    case "Boolean":
                        if (value != null)
                        {
                            binaryExpression = Expression.MakeBinary(ExpressionType.Equal, memberExpression, constantExpression);
                            result = Expression.AndAlso(result, binaryExpression);
                        }
                        break;
                    case "String":
                    case "Char":
                        if (value != null && value.ToString() != "")
                        {
                            Expression methodCallExpression = Expression.Call(memberExpression, like, constantExpression);
                            result = Expression.AndAlso(result, methodCallExpression);
                            // sources = sources.Where(Expression.Lambda<Func<TSource, bool>>(methodCallExpression, p));
                        }
                        break;
                    case "DateTime":
                        if (value != null && value.ToString() != "" && value.ToString() != "0001/1/1 0:00:00")
                        {
                            if (item.Name.EndsWith("StartTime"))
                            {
                                binaryExpression = Expression.MakeBinary(ExpressionType.GreaterThanOrEqual, memberExpression, constantExpression);
                            }
                            else if (item.Name.EndsWith("EndTime"))
                            {
                                binaryExpression = Expression.MakeBinary(ExpressionType.LessThanOrEqual, memberExpression, constantExpression);
                            }
                            else
                            {
                                binaryExpression = Expression.MakeBinary(ExpressionType.Equal, memberExpression, constantExpression);
                            }
                            //sources = sources.Where(Expression.Lambda<Func<TSource, bool>>(binaryExpression, p));
                            result = Expression.AndAlso(result, binaryExpression);
                        }
                        break;
                }
            }
            return sources.Where(Expression.Lambda<Func<TSource, bool>>(result, p));
        }

        /// <summary>
        /// 单个表转为下拉列表数据查询语句
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sources">查询数据源</param>
        /// <param name="fieldName">自定义采集字段</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IQueryable<DropdownDataResult> ConvertListSearch<TSource>(this IQueryable<TSource> sources, string fieldName = "")
        {
            Type type = typeof(TSource);
            if (fieldName.IsNullOrEmpty() && !type.GetProperties().Any(p => p.Name == "Name"))
            {
                throw new InvalidOperationException("实体表中没有Name字段，请设置默认数据采集字段名称");
            }
            Type resultType = typeof(DropdownDataResult);
            // 参数表达式，构建P
            ParameterExpression p = Expression.Parameter(typeof(TSource), "p");
            // 成员表达式，构建 p.id
            MemberExpression idMemberExpression = Expression.PropertyOrField(p, "Id");
            MemberExpression nameMemberExpression = Expression.PropertyOrField(p, fieldName.IsNullOrEmpty() ? "Name" : fieldName);
            // 赋值表达式，构建 Id => p.id
            MemberAssignment idMemberAssignment = Expression.Bind(resultType.GetProperty(nameof(DropdownDataResult.Id)), idMemberExpression);
            MemberAssignment nameMemberAssignment = Expression.Bind(resultType.GetProperty(nameof(DropdownDataResult.Name)), nameMemberExpression);
            // 规范返回模板
            NewExpression result = Expression.New(typeof(DropdownDataResult));
            // 按照指定规范返回模板和赋值表达式填充
            MemberInitExpression memberInitExpression = Expression.MemberInit(result, idMemberAssignment, nameMemberAssignment);
            // 转换为查询表达式
            var searchQuery = Expression.Lambda<Func<TSource, DropdownDataResult>>(memberInitExpression, p);
            return sources.Select(searchQuery);
        }


    }
}
