using System.ComponentModel.DataAnnotations;
namespace Model.DTOs.BackEnd.TenantMenuManage
{
    /// <summary>
    /// �����⻧�˵���Ϣ������
    /// </summary>
    public class UpdateTenantMenuInput : AddTenantMenuInput
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required(ErrorMessage = "IdRequried")]
        public long Id { get; set; }
    }

}