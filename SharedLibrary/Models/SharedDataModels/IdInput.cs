using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.Models.SharedDataModels
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
