using System.ComponentModel.DataAnnotations;
using Model.Commons.SharedData;
namespace Model.DTOs.BackEnd.AuditTypeManage
{
    public class GetAuditTypePageInput : PageInput
    {
        /// <summary>
        /// Name
        /// </summary>
        [MaxLength(50,ErrorMessage = "NameTooLong50")]
        public string Name { get; set; } = String.Empty;
        }
                   
}