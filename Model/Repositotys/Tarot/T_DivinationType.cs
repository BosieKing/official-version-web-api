using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Repositotys.Tarot
{
    /// <summary>
    /// 占卜类型
    /// </summary>
    [Table(nameof(T_DivinationType))]
    public class T_DivinationType : EntityBaseDO
    {
        public string Name { get; set; }
    }
}
