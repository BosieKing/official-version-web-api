using System.ComponentModel.DataAnnotations;
using Model.Commons.SharedData;
namespace Model.DTOs.BackEnd.AuditNodeConfigManage
{
    public class GetAuditNodeConfigPageInput : PageInput
    {
        /// <summary>
        /// Name
        /// </summary>
        [MaxLength(50,ErrorMessage = "NameTooLong50")]
        public string Name { get; set; } = String.Empty;
        
        /// <summary>
        /// NodeLevel
        /// </summary>
        public int NodeLevel { get; set; } = 0;
     }
                   
}