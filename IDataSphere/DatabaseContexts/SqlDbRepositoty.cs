using Microsoft.EntityFrameworkCore;
using Model.Repositotys.BasicData;
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
        /// 角色表
        /// </summary>
        public DbSet<T_Role> RoleRep { get; set; }
    }
}
