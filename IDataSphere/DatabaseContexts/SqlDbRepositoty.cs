using Microsoft.EntityFrameworkCore;
using Model.Repositotys;

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
        /// 目录表
        /// </summary>
        public DbSet<T_Directory> DirectoryRep { get; set; }

        /// <summary>
        /// 租户目录表
        /// </summary>
        public DbSet<T_TenantDirectory> TenantDirectoryRep { get; set; }

        /// <summary>
        /// 菜单表
        /// </summary>
        public DbSet<T_Menu> MenuRep { get; set; }

        /// <summary>
        /// 租户菜单表
        /// </summary>
        public DbSet<T_TenantMenu> TenantMenuRep { get; set; }

        /// <summary>
        /// 菜单按钮表
        /// </summary>
        public DbSet<T_MenuButton> MenuButtonRep { get; set; }

        /// <summary>
        /// 租户菜单按钮表
        /// </summary>
        public DbSet<T_TenantMenuButton> TenantMenuButtonRep { get; set; }

        /// <summary>
        /// 角色表
        /// </summary>
        public DbSet<T_Role> RoleRep { get; set; }

        /// <summary>
        /// 角色菜单表
        /// </summary>
        public DbSet<T_RoleMenu> RoleMenuRep { get; set; }

        /// <summary>
        /// 租户表
        /// </summary>
        public DbSet<T_Tenant> TenantRep { get; set; }

        /// <summary>
        /// 用户权限表
        /// </summary>
        public DbSet<T_UserRole> UserRoleRep { get; set; }

        /// <summary>
        /// 角色屏蔽按钮
        /// </summary>
        public DbSet<T_RoleBlockButton> RoleBlockButtonsRep { get; set; }

    }
}
