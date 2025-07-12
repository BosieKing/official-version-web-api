using System.ComponentModel.DataAnnotations;
using Yitter.IdGenerator;

namespace Model.Repositotys
{
    /// <summary>
    /// 基类
    /// </summary>
    public class EntityBaseDO
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public EntityBaseDO()
        {
            CreatedTime = DateTime.Now;
            Id = YitIdHelper.NextId();
        }

        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedUserId { get; set; }

        /// <summary>
        /// 软删除，false0默认有效数据，true1代表非有效数据
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 更新人
        /// </summary>
        public long UpdateUserId { get; set; } = 0;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }   
}
