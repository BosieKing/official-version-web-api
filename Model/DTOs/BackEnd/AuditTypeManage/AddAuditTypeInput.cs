using System.ComponentModel.DataAnnotations;
namespace Model.DTOs.BackEnd.AuditTypeManage 
{
    public class AddAuditTypeInput
    {               
        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = "NameRequired")]        
        [MaxLength(50,ErrorMessage = "NameTooLong50")]   
        public string Name { get; set; }        
        } 

                
                   
}