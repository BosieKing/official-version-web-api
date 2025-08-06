using IDataSphere.DatabaseContexts;
using IDataSphere.Extensions;
using IDataSphere.Interfaces.FronDesk;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Commons.CoreData;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.FronDesk.Home;
using Model.Repositotys.Service;
using Nest;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Utilities.Encoders;
using SharedLibrary.Enums;
using TencentCloud.Ame.V20190916.Models;
using TencentCloud.Cdn.V20180606.Models;
using TencentCloud.Wedata.V20210820.Models;
using UtilityToolkit.Helpers.WxLogin;
using UtilityToolkit.Helpers.WxLogin.Dto;
using UtilityToolkit.Tools;

namespace DataSphere.FronDesk
{
    /// <summary>
    /// 前台权限业务数据访问实现类
    /// </summary>
    public class HomeDao : Repository<T_User>, IHomeDao
    {
        #region 构造函数
        private readonly WxLoginHelper _wxLoginHelper;
        public HomeDao(SqlDbContext dbContext, WxLoginHelper wxLoginHelper) : base(dbContext)
        {
            _wxLoginHelper = wxLoginHelper;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 获取导师列表
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> GetTeacherList(GetTeacherListInput input)
        {
            var baseUrl = ConfigSettingTool.SystemConfig.DomainAddress.TrimEnd('/');
            var query = _db.TeacherRep.Where(p => p.UserIdentityType == UserIdentityTypeEnum.Teacher)
                .Where(input.DanceType != null, p => p.DanceTypes.Any(d => d.Type == input.DanceType.Value)).Select(p => new
                {
                    UserId = p.Id,
                    NickName = p.NickName,
                    AvatarUrl = $"{baseUrl}/{p.AvatarUrl.TrimStart('/')}",
                    DanceType = p.DanceTypes.First().Type,
                    SelfIntroduce = p.SelfIntroduce
                });
            return await base.AdaptPage(query, input);
        }

        /// <summary>
        /// 获取私教预约列表
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> GetPersonalCourseList(GetPersonalCourseListInput input)
        {
            var baseUrl = ConfigSettingTool.SystemConfig.DomainAddress.TrimEnd('/');
            var query = _db.TeacherRep.Where(p => p.UserIdentityType == UserIdentityTypeEnum.Teacher)
                .Where(p => p.PersonalCourses.Any(p => p.CourseDate >= DateTime.Today))
                .Where(input.DanceType != null, p => p.DanceTypes.Any(d => d.Type == input.DanceType.Value)).Select(p => new
                {
                    UserId = p.Id,
                    NickName = p.NickName,
                    AvatarUrl = $"{baseUrl}/{p.AvatarUrl.TrimStart('/')}",
                    DanceType = p.DanceTypes.First().Type,
                    Count = p.PersonalCourses.Where(p => p.IsBooking == false && p.CourseDate >= DateTime.Today).Count(),
                    RateCount = new Random().Next(1, 4),
                    SelfIntroduce = p.SelfIntroduce
                });
            return await query.ToListAsync();
        }

        /// <summary>
        /// 获取课程列表
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> GetCourseList(GetCourseListInput input)
        {
            var baseUrl = ConfigSettingTool.SystemConfig.DomainAddress.TrimEnd('/');
            var query = _db.CourseRep.Where(input.DanceType != null, p => p.DanceType == input.DanceType)
                                           .Where(input.CourseDate != null, p => p.CourseDate == input.CourseDate)
                                           .Where(input.UserId != null, p => p.TeacherId == input.UserId)
                                           .Select(c => new
                                           {
                                               CourseId = c.Id,
                                               UserId = c.TeacherId,
                                               Title = c.Title,
                                               CourseType = c.CourseType,
                                               StartTime = c.StartTime,
                                               EndTime = c.EndTime,
                                               DanceType = c.DanceType,
                                               CourseDate = c.CourseDate,
                                               PosterUrl = $"{baseUrl}/{c.PosterUrl.TrimStart('/')}",
                                               Description = c.Description,
                                               MaxParticipants = c.MaxParticipants,
                                               Price = c.Price,
                                               IsActive = c.IsActive,
                                               IsBooking = false,
                                               Address = c.Address,
                                               Phone = c.Teacher.Phone,
                                               SignUpCount = c.CourseSignUp.Count,
                                               TeacherName = c.Teacher.NickName,
                                               SelfIntroduce = c.Teacher.SelfIntroduce
                                           }).OrderBy(p => p.CourseDate).ThenBy(p => p.StartTime);

            if (input.Limit != null)
            {
                return await query.Take(input.Limit.Value).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }

        }

        /// <summary>
        /// 获取课程报名用户头像列表
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> GetCourseSignUpAvaratUrlList(GetCourseSignUpAvaratUrlListInput input)
        {
            var baseUrl = ConfigSettingTool.SystemConfig.DomainAddress.TrimEnd('/');
            var query = _db.CourseSignUpRep
                .Where(p => p.CourseId == input.CourseId)
                .Select(c => new
                {
                    UserId = c.Id,
                    AvatarUrl = $"{baseUrl}/{c.User.AvatarUrl.TrimStart('/')}",
                });

            if (input.Limit > 0)
            {
                return await query.Take(input.Limit).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }

        }

        /// <summary>
        /// 获取私教预约列表
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> GetPersonalCourseTimeList(GetPersonalCoureseTimeListInput input)
        {
            var baseUrl = ConfigSettingTool.SystemConfig.DomainAddress.TrimEnd('/');
            var query = _db.PersonalCourseRep
                .Where(p => p.TeacherId == input.UserId)
                .Where(p => p.CourseDate == input.CourseDate)
                .Select(p => new
                {
                    PersonalCourseId = p.Id,
                    p.StartTime,
                    p.EndTime,
                    p.CourseDate,
                    p.IsBooking,
                });
            return await query.ToListAsync();
        }

        /// <summary>
        /// 获取我的卡包
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> GetMyCardList(GetMyCardListInput input)
        {
            var query = _db.MyCardRep
                .Where(p => p.UserId == 705499249971269)
                .Select(p => new
                {
                    MyCardId = p.Id,
                    p.Count,
                    p.CourseType,
                    p.DanceType,
                    p.ValidTo,
                });
            return await query.ToListAsync();
        }

        /// <summary>
        /// 获取我的卡包使用情况
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> GetUseCardRecodeList(GetUseCardRecodeListInput input)
        {
            if (input.CourseType == CourseTypeEnum.OneOnOne)
            {
                var query = _db.PersonalCourseSignUpRep
                                 .Where(p => p.UserId == 705499249971269)
                                 .Where(p => p.CardId == input.CardId)
                                 .Include(p => p.PersonalCourse).ThenInclude(p => p.Teacher)
                                 .Select(p => new
                                 {
                                     p.CreatedTime,
                                     TeacherName = p.PersonalCourse.Teacher.NickName + "私教课",
                                     StartTime = p.PersonalCourse.StartTime,
                                     EndTime = p.PersonalCourse.EndTime,
                                     CourseDate = p.PersonalCourse.CourseDate,
                                 });
                return await query.ToListAsync();
            }
            else
            {
                var query = _db.CourseSignUpRep
                                 .Where(p => p.UserId == 705499249971269)
                                 .Where(p => p.CardId == input.CardId)
                                 .Include(p => p.Course).ThenInclude(p => p.Teacher)
                                 .Select(p => new
                                 {
                                    p.CreatedTime,
                                    TeacherName = p.Course.Title,
                                    StartTime = p.Course.StartTime,
                                    EndTime = p.Course.EndTime,
                                    CourseDate = p.Course.CourseDate,
                                 });
                return await query.ToListAsync();
            }

        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增私教预约
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> AddPersonalCourseBooking(AddPersonalCourseBookingInput input)
        {
            var tra = await _db.Database.BeginTransactionAsync();
            try
            {
                var list = await _db.PersonalCourseRep.Where(p => input.Ids.Contains(p.Id) && p.IsBooking == false).ToListAsync();
                var card = await _db.MyCardRep.AsTracking().FirstOrDefaultAsync(p => p.UserId == 705499249971269 && p.CourseType == CourseTypeEnum.OneOnOne && list.FirstOrDefault().DanceType == p.DanceType && p.ValidTo >= DateTime.Now);
                if (card == null) 
                {
                    return ServiceResult.Fail("你还未拥有该舞蹈类型的私教卡");
                }
                if (input.Ids.Count() != list.Count)
                {
                    return ServiceResult.Fail("选择的时间已经被其他用户预定");
                }              
                if (card.Count - list.Count < 0 )
                {
                    return ServiceResult.Fail("卡片剩余次数不足以抵消预约时间段数量");
                }
                var signUpList = new List<T_PersonalCourseSignUp>();
                list.ForEach(item =>
                {
                    item.IsBooking = true;
                    T_PersonalCourseSignUp signUp = new T_PersonalCourseSignUp();
                    signUp.UserId = 705499249971269;
                    signUp.PersonalCourseId = item.Id;
                    signUp.CardId = card.Id;
                    signUpList.Add(signUp);
                });
                card.Count = card.Count - list.Count;
                _db.Update(card);
                _db.UpdateRange(list);
                _db.AddRange(signUpList);
                await _db.SaveChangesAsync();
                await tra.CommitAsync();
            }
            catch (Exception ex)
            {
                await tra.RollbackAsync();
            }
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 新增课程预约
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> AddCourseBooking(AddCourseBookingInput input)
        {
            var tra = await _db.Database.BeginTransactionAsync();
            try
            {
                var data = await _db.CourseRep.Where(p => input.Id == p.Id).Include(p => p.CourseSignUp).FirstOrDefaultAsync();
                if (data == null) {
                    return ServiceResult.Fail("课程不存在啊");
                }
                if (data.CourseSignUp.Any(p => p.CourseId == input.Id && p.UserId == 705499249971269 && p.IsCancel == false))
                {
                    return ServiceResult.Fail("您已经预定了该课程，请勿重复预约");
                }
                var card = await _db.MyCardRep.AsTracking().FirstOrDefaultAsync(p => p.UserId == 705499249971269 && p.CourseType == data.CourseType && p.DanceType == data.DanceType && p.ValidTo >= DateTime.Now);
                if (card == null)
                {
                    return ServiceResult.Fail("您还未用有该舞蹈类型的课程卡");
                }
                if (card.Count - 1 < 0)
                {
                    return ServiceResult.Fail("课程卡剩余次数不足");
                }
                if (data.CourseSignUp.Count() + 1 > data.MaxParticipants)
                {
                    return ServiceResult.Fail("预约人数已满，请选择其他课程");
                } 
                var signUp = new T_CourseSignUp();
                signUp.UserId = 705499249971269;
                signUp.CourseId = input.Id;
                signUp.CardId = card.Id;
                card.Count = card.Count - 1;
                _db.Add(signUp);
                _db.Update(card);
                await _db.SaveChangesAsync();
                await tra.CommitAsync();
            }
            catch (Exception ex)
            {
                await tra.RollbackAsync();
            }
            return ServiceResult.Successed();
        }
        #endregion

        #region 登录
        /// <summary>
        /// 微信登录
        /// </summary>
        public async Task<dynamic> WxLogin(WxLoginInput input)
        {
            var resutl = await _wxLoginHelper.Login(input);
            string token = string.Empty;
            // 判断系统中有没有该用户
            var user = await _db.UserRep.FirstOrDefaultAsync(p => p.OpenId == resutl.openId && p.Phone == resutl.phone);

            if (user != null)
            {
                var tokenInfoModel = new TokenInfoModel();
                tokenInfoModel.UserId = user.Id.ToString();
                tokenInfoModel.RoleIds = "";
                return new
                {
                    token = TokenTool.CreateToken(tokenInfoModel),
                    user = user
                };
            }
            else
            {
                var newUser = new T_User();
                newUser.NickName = "可爱的小羊";
                newUser.AvatarUrl = "/Avatars/teacher-default-avatar-boy.png";
                newUser.Phone = resutl.phone;
                newUser.Password = "sdfsdfsdfsdfsdfsdfsdfdfsdfsdf";
                newUser.Sex = SexEnum.UNKnown;
                newUser.OpenId = resutl.openId;
                newUser.SelfIntroduce = "爱生活、爱跳舞";
                await _db.AddAsync(newUser);
                await _db.SaveChangesAsync();
                var tokenInfoModel = new TokenInfoModel();
                tokenInfoModel.UserId = newUser.Id.ToString();
                tokenInfoModel.RoleIds = "";
                var baseUrl = ConfigSettingTool.SystemConfig.DomainAddress.TrimEnd('/');
                newUser.AvatarUrl = $"{baseUrl}/{newUser.AvatarUrl.TrimStart('/')}";
                return new
                {
                    token = TokenTool.CreateToken(tokenInfoModel),
                    user = newUser
                };
            }

        }


        #endregion
    }
}
