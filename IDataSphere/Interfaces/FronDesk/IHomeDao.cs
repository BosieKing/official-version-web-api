using Microsoft.AspNetCore.Http;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.FronDesk.Home;
using Model.Repositotys.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityToolkit.Helpers.WxLogin.Dto;

namespace IDataSphere.Interfaces.FronDesk
{
    public interface IHomeDao : IRepository<T_User>
    {
        Task<dynamic> GetCourseSignUpAvaratUrlList(GetCourseSignUpAvaratUrlListInput input);
        Task<dynamic> GetCourseList(GetCourseListInput input);
        Task<dynamic> GetTeacherList(GetTeacherListInput input);
        Task<dynamic> GetPersonalCourseList(GetPersonalCourseListInput input);
        Task<dynamic> GetPersonalCourseTimeList(GetPersonalCoureseTimeListInput input);
        Task<ServiceResult> AddPersonalCourseBooking(AddPersonalCourseBookingInput input);
        Task<ServiceResult> AddCourseBooking(AddCourseBookingInput input);
        Task<dynamic> WxLoginByOpenId(WxLoginByOpenIdInput input);
        Task<dynamic> GetMyCardList(GetMyCardListInput input);
        Task<dynamic> GetUseCardRecodeList(GetUseCardRecodeListInput input);
        Task<dynamic> GetMyCourseSignUpList(GetMyCourseSignUpListInput input);
        Task<dynamic> GetMyPersonalCourseSignUpList(GetMyCourseSignUpListInput input);
        Task<ServiceResult> CancelPersonalCourseSignUp(IdInput input);
        Task<ServiceResult> CancelCourseSignUp(IdInput input);
        Task<ServiceResult> UploadAratav(IFormFile file);
        Task<dynamic> GetCourseSignUpDetail(IdInput input);
        Task<dynamic> WxLoginByPhone(WxLoginByPhoneInput input);
    }
}
