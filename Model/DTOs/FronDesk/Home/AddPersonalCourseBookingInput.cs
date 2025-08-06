using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.FronDesk.Home
{
    public class AddPersonalCourseBookingInput
    {
        /// <summary>
        /// 预约的私教id
        /// </summary>
        public long[] Ids { get; set; }
    }
}
