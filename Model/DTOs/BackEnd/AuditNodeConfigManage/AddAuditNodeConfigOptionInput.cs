using System.ComponentModel.DataAnnotations;
namespace Model.DTOs.BackEnd.AuditNodeConfigManage 
{
    /// <summary>
    /// �����ӽڵ�
    /// </summary>
    public class AddAuditNodeConfigOptionInput
    {
        /// <summary>
        /// ������˵�id
        /// </summary>
        public long AuditNodeConfigId { get; set; }

        /// <summary>
        /// �ڼ���
        /// </summary>
        public int AuditLevel { get; set; }

        /// <summary>
        /// �����������Դ
        /// </summary>
        public long AuditType { get; set; }

        /// <summary>
        /// ���������
        /// </summary>
        /// <remarks>����дΪ0��ʱ��Ĭ��Ϊ��Ҫȫ���������͵��û�ȫ��ͨ����Ž����¼���ˣ�����0���ϵͳĬ�������n��</remarks>
        public int AuditUserCount { get; set; }

        /// <summary>
        /// ��˲���
        /// </summary>
        /// <see cref="SharedLibrary.Enums.AuditApproveStrategyEnum"/>
        public int ApproveStrategy { get; set; }

        /// <summary>
        /// ��������Ϊ��ͨ����ʱ����Ҫ���˵ķ�֧
        /// </summary>
        /// <remarks>�絽���������ˣ�������3������˲�ͨ�����޸��������޸��ٴ��ύ�������3����ˣ�ǰ����������</remarks>
        public int FailRetrunLevel { get; set; }
    }  
}