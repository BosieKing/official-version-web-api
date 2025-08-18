using Model.Commons.SharedData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.FronDesk.Home
{
    public class GetMyCourseSignUpListInput:PageInput
    {
        public int SearchType { get; set; }
    }
}
