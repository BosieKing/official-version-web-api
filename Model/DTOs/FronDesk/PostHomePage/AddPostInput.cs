using System.ComponentModel.DataAnnotations;

namespace Model.DTOs.FronDesk.PostHomePage
{
    public class AddPostInput
    {
        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(20)]
        public string Title { get; set; }

        /// <summary>
        /// 帖子内容
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// 标签，#号分割
        /// </summary>
        public string Tag{ get; set; }

    }
}
