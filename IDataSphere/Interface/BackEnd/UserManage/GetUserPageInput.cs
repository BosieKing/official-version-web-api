using SharedLibrary.Models.SharedDataModels;
using System.ComponentModel.DataAnnotations;
namespace IDataSphere.Interface.BackEnd.UserManage
{
    public class GetUserPageInput : PageInput
    {      
        /// <summary>
        /// NickName
        /// </summary>
        [MaxLength(50, ErrorMessage = "NickNameTooLong50")]
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// Phone
        /// </summary>
        [MaxLength(50, ErrorMessage = "PhoneTooLong50")]
        public string Phone { get; set; } = string.Empty;
      
    }

}