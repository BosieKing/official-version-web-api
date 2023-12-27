using System.ComponentModel.DataAnnotations;
using Yitter.IdGenerator;

namespace IDataSphere.Repositoty
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
        /// 软删除
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 更新人
        /// </summary>
        public long UpdateUserId { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }
}
