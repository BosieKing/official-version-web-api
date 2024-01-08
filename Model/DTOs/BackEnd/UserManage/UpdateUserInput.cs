using System.ComponentModel.DataAnnotations;
namespace Model.DTOs.BackEnd.UserManage
{
    /// <summary>
    /// �����û���Ϣ������
    /// </summary>
    public class UpdateUserInput : AddUserInput
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required(ErrorMessage = "IdRequried")]
        public long Id { get; set; }
    }

}