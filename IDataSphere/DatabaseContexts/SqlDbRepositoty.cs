using Microsoft.EntityFrameworkCore;
using Model.Repositotys.Service;

namespace IDataSphere.DatabaseContexts
{
    /// <summary>
    /// 数据库仓储
    /// </summary>
    public partial class SqlDbContext : DbContext
    {
        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<T_User> UserRep { get; set; }

        /// <summary>
        /// 老师表
        /// </summary>
        public DbSet<T_Manager> TeacherRep { get; set; }

        /// <summary>
        /// 课程表
        /// </summary>
        public DbSet<T_Course> CourseRep { get; set; }

        /// <summary>
        /// 用户预约课程表
        /// </summary>
        public DbSet<T_CourseSignUp> CourseSignUpRep { get; set; }

        /// <summary>
        /// 私教表
        /// </summary>
        public DbSet<T_PersonalCourse> PersonalCourseRep { get; set; }

        /// <summary>
        /// 用户预约私教表
        /// </summary>
        public DbSet<T_PersonalCourseSignUp> PersonalCourseSignUpRep { get; set; }

        /// <summary>
        /// 我的卡包
        /// </summary>
        public DbSet<T_MyCard> MyCardRep { get; set; }


        /// <summary>
        /// 管理者
        /// </summary>
        public DbSet<T_Manager> ManagerRep { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public DbSet<T_Role> RoleRep { get; set; }
    }
}
