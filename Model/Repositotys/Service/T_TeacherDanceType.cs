using SharedLibrary.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.Service
{
    /// <summary>
    /// 老师负责的舞蹈类型
    /// </summary>
    [Table("T_TeacherDanceType")]
    public class T_TeacherDanceType 
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Key]
        public long TeacherId { get; set; }

        /// <summary>
        /// 主要负责的舞蹈类型
        /// </summary>
        public DanceTypeEnum Type { get; set; }

    }
}
