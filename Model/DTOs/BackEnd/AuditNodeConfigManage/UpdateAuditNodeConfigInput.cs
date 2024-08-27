using System.ComponentModel.DataAnnotations;
namespace Model.DTOs.BackEnd.AuditNodeConfigManage 
{
    public class UpdateAuditNodeConfigInput :  AddAuditNodeConfigInput
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required(ErrorMessage = "IdRequried")]
        public long Id { get; set; }
    }
                   
}