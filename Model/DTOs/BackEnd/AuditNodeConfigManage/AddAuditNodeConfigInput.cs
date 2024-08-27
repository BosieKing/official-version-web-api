using System.ComponentModel.DataAnnotations;
namespace Model.DTOs.BackEnd.AuditNodeConfigManage 
{
    public class AddAuditNodeConfigInput
    {               
        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = "NameRequired")]        
        [MaxLength(50,ErrorMessage = "NameTooLong50")]   
        public string Name { get; set; }        
                       
        /// <summary>
        /// NodeLevel
        /// </summary>
        [Required(ErrorMessage = "NodeLevelRequired")]   
        public int NodeLevel { get; set; }        
        } 

                
                   
}