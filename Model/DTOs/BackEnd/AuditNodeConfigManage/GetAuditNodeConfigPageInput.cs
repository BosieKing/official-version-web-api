using System.ComponentModel.DataAnnotations;
using Model.Commons.SharedData;
namespace Model.DTOs.BackEnd.AuditNodeConfigManage
{
    public class GetAuditNodeConfigPageInput : PageInput
    {
        /// <summary>
        /// ≈‰÷√¿‡–Õ
        /// </summary>
        public int AuditNodeConfigType { get; set; }
    }
                   
}