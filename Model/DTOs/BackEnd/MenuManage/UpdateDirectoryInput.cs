using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.BackEnd.MenuManage
{
    /// <summary>
    /// 更新目录信息输入类
    /// </summary>
    public class UpdateDirectoryInput : AddDirectoryInput
    {
        /// <summary>
        /// 目录id
        /// </summary>
        [Required(ErrorMessage = "IdRequried")]
        public long Id { get; set; }
    }
}
