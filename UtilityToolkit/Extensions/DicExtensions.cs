namespace System.Collections.Generic
{
    public static class DicExtensions
    {
        /// <summary>
        /// 合并字典
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="newdata"></param>
        /// <returns></returns>
        public static Dictionary<TSource, TValue> AddRange<TSource, TValue>(this Dictionary<TSource, TValue> source, Dictionary<TSource, TValue> newdata) where TSource : notnull
        {
            source = source.Concat(newdata).ToDictionary(p => p.Key, p => p.Value);
            return source;
        }
    }
}
