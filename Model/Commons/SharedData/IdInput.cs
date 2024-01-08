using System.ComponentModel.DataAnnotations;

namespace Model.Commons.SharedData
{
    /// <summary>
    /// Id模型
    /// </summary>
    public class IdInput
    {
        [Required(ErrorMessage = "IdRequired")]
        public long Id { get; set; }
    }
}
