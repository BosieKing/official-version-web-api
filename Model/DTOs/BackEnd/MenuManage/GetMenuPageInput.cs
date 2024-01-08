using Model.Commons.SharedData;

namespace Model.DTOs.BackEnd.MenuManage
{
    public class GetMenuPageInput : PageInput
    {
        /// <summary>
        /// 名称
        /// </summary>       
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 图标--前端需要使用字段
        /// </summary>             
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        /// 路由地址-对应的控制器名称
        /// </summary>       
        public string Router { get; set; } = string.Empty;

        /// <summary>
        /// 组件地址--前端需要使用字段
        /// </summary>       
        public string Component { get; set; } = string.Empty;

        /// <summary>
        /// 菜单权重
        /// </summary>
        /// <see cref="Resource.Enums.MenuWeightTypeEnum"/>      
        public int Weight { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>      
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 创建开始时间
        /// </summary>
        public DateTime CreatedTimeStartTime { get; set; }

        /// <summary>
        /// 创建结束时间
        /// </summary>
        public DateTime CreatedTimeEndTime { get; set; }
    }
}
