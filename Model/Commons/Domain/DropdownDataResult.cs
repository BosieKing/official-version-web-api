namespace Model.Commons.Domain
{
    /// <summary>
    /// 下拉列表规范返回类
    /// </summary>
    public class DropdownDataResult
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
