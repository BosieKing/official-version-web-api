namespace Model.Commons.Domain
{
    /// <summary>
    /// 选中下拉列表规范返回类
    /// </summary>
    public class DropdownSelectionResult : DropdownDataResult
    {
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsCheck { get; set; }
    }
}
