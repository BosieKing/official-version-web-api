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
        /// 目录表
        /// </summary>
        public DbSet<T_Directory> DirectoryRep { get; set; }

        /// <summary>
        /// 菜单表
        /// </summary>
        public DbSet<T_Menu> MenuRep { get; set; }

        /// <summary>
        /// 菜单按钮表
        /// </summary>
        public DbSet<T_MenuButton> MenuButtonRep { get; set; }

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
        /// 审核角色表
        /// </summary>
        public DbSet<T_AuditType> AuditTypeRep { get; set; }

        /// <summary>
        /// 用户与审核角色中间表
        /// </summary>
        public DbSet<T_UserAuditType> UserAuditTypeRep { get; set; }

        /// <summary>
        /// 审核流程配置表
        /// </summary>
        public DbSet<T_AuditNodeConfig> AuditNodeConfigRep { get; set; }


        /// <summary>
        /// 审核流程节点配置
        /// </summary>
        public DbSet<T_AuditNodeConfigOption> AuditNodeConfigOptionRep { get; set; }

        /// <summary>
        ///  审核节点策略审核人
        /// </summary>
        public DbSet<T_AuditNodeApproveStrategyUser> AuditNodeApproveStrategyUserRep { get; set; }
    }
}
