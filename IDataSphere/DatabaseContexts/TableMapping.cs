using Microsoft.EntityFrameworkCore;

namespace IDataSphere.DatabaseContexts
{
    public class TableMapping
    {
        /// <summary>
        /// 表关系映射
        /// </summary>
        /// <param name="entityType"></param>
        public void Mapping(ModelBuilder builder)
        {
            //builder.Entity<T_Directory>().HasMany(p => p.Menus).WithOne(p => p.BelongDirectory).HasForeignKey(p => p.DirectoryId);
            //builder.Entity<T_Menu>().HasMany(p => p.MenuButtons).WithOne(p => p.BelongMenu).HasForeignKey(p => p.MenuId);
            //builder.Entity<T_MenuButton>().HasOne(p => p.BelongMenu);
        }


    }
}
