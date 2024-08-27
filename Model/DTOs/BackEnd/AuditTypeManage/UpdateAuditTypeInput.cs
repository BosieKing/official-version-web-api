using System.ComponentModel.DataAnnotations;
namespace Model.DTOs.BackEnd.AuditTypeManage 
{
    public class UpdateAuditTypeInput :  AddAuditTypeInput
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required(ErrorMessage = "IdRequried")]
        public long Id { get; set; }
    }
                   
}