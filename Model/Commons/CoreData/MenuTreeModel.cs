using System.Runtime.Serialization;

namespace Model.Commons.CoreData
{
    /// <summary>
    /// 菜单树序列化模型
    /// </summary>
    public class MenuTreeModel
    {
        /// <summary>
        /// Icon
        /// </summary>
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 前端组件物理地址
        /// </summary>
        public string Component { get; set; } = string.Empty;

        /// <summary>
        /// 后端权限标识路由
        /// </summary>
        public string Router { get; set; } = string.Empty;

        /// <summary>
        /// 前端浏览器路由地址
        /// </summary>
        public string BrowserPath { get; set; } = string.Empty;

        /// <summary>
        /// id
        /// </summary>
        public long Id { get; set; } = 0;

        /// <summary>
        /// 类型
        /// </summary>
        /// <see cref="SharedLibrary.Enums.MenuTreeTypeEnum"/>
        public int Type { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 菜单权重
        /// </summary>
        public int Weight { get; set; } = 0;

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHidden { get; set; } = false;

        /// <summary>
        /// 父id
        /// </summary>
        public long PId { get; set; } = 0;

        /// <summary>
        /// 父节点名称
        /// </summary>
        [IgnoreDataMember]
        public string PName { get; set; } = string.Empty;

        /// <summary>
        /// 父节点Icon
        /// </summary>
        [IgnoreDataMember]
        public string PIcon { get; set; } = string.Empty;

        /// <summary>
        /// 父节点路径
        /// </summary>
        [IgnoreDataMember]
        public string PPath { get; set; } = string.Empty;
    }
}
