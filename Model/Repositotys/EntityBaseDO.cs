using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yitter.IdGenerator;

namespace Model.Repositotys
{
    /// <summary>
    /// 基类（实体基类）
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
        /// 主键ID（使用Yitter雪花ID生成器）
        /// </summary>
        [Key]
        [Column("Id")]
        public long Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("CreatedTime")]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 创建人ID（关联用户表）
        /// </summary>
        [Column("CreatedUserId")]
        public long CreatedUserId { get; set; }

        /// <summary>
        /// 软删除标记（false-有效数据，true-已删除）
        /// </summary>
        [Column("IsDeleted")]
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 最后更新人ID（关联用户表）
        /// </summary>
        [Column("UpdateUserId")]
        public long UpdateUserId { get; set; } = 0;

        /// <summary>
        /// 最后更新时间（null表示从未更新）
        /// </summary>
        [Column("UpdateTime")]
        public DateTime? UpdateTime { get; set; }
    }
}
// Add-Migration v1 -Project IDataSphere
// Update-Database -Project IDataSphere