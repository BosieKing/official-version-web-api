using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.BackEnd.System
{
    /// <summary>
    /// 上传轮播图
    /// </summary>
    public class UpdateCarouselInput
    {
        /// <summary>
        /// 文件名（不含路径）
        /// </summary>
        public string FileName { get; set; }

       
    }
}