using System.ComponentModel.DataAnnotations;
namespace Model.DTOs.BackEnd.UserManage
{
    /// <summary>
    /// 更新用户信息输入类
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