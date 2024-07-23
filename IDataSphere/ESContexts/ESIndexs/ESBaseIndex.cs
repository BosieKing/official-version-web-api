using Nest;
using Yitter.IdGenerator;


namespace IDataSphere.ESContexts.ESIndexs
{
    /// <summary>
    /// 基础索引
    /// </summary>
    public class ESBaseIndex
    {
        public ESBaseIndex()
        {
            this.Id = YitIdHelper.NextId().ToString();
            this.CreatedTime = DateTimeOffset.Now;

        }
        /// <summary>
        /// Id
        /// </summary>
        /// <remarks>string类型存储，设置为keyword，避免丢失精度</remarks>
        [Keyword(Name = nameof(Id), Index = true)]
        public string Id { get; set; }

        /// <summary>
        /// 创建时间戳
        /// </summary>
        /// <remarks>使用DateTimeOffset可以避免时区的问题，因为Es的时间类型要求为UTC时间的，也就是带有时区的，用了这个类型就不要用Date特性了</remarks>
        //[Date(Format = "yyyy-MM-dd'T'HH:mm:ssZ")]
        public DateTimeOffset CreatedTime { get; set; }
    }
}
