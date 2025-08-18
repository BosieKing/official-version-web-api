using IDataSphere.DatabaseContexts;
using IDataSphere.Extensions;
using IDataSphere.Interfaces.FronDesk;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Commons.CoreData;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.FronDesk.Home;
using Model.Repositotys.Service;
using Nest;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Utilities.Encoders;
using SharedLibrary.Enums;
using TencentCloud.Ame.V20190916.Models;
using TencentCloud.Cdn.V20180606.Models;
using TencentCloud.Mrs.V20200910.Models;
using TencentCloud.Wedata.V20210820.Models;
using UtilityToolkit.Extensions;
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
        private long __userid = 706182181593157;
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
                .Where(input.DanceType != null, p => p.DanceTypes.Any(d => d.DanceType == input.DanceType.Value)).Select(p => new
                {
                    TeacherId = p.Id,
                    NickName = p.NickName,
                    AvatarUrl = $"{baseUrl}/{p.AvatarUrl.TrimStart('/')}",
                    DanceType = p.DanceTypes.First().DanceType,
                    SelfIntroduce = p.SelfIntroduce
                });
            return await base.ToPage(query, input);
        }

        /// <summary>
        /// 获取私教预约列表
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> GetPersonalCourseList(GetPersonalCourseListInput input)
        {
            var baseUrl = ConfigSettingTool.SystemConfig.DomainAddress.TrimEnd('/');
            var now = DateTime.Now;
            var today = now.Date;

            var query = _db.TeacherRep
                .Where(p => p.UserIdentityType == UserIdentityTypeEnum.Teacher)
                .Where(p => p.PersonalCourses.Any(p => p.CourseDate >= today))
                .Where(input.TeacherId != 0, p => p.Id == input.TeacherId)
                .Where(input.DanceType != null, p => p.DanceTypes.Any(d => d.DanceType == input.DanceType.Value))
                .Select(p => new
                {
                    TeacherId = p.Id,
                    NickName = p.NickName,
                    AvatarUrl = $"{baseUrl}/{p.AvatarUrl.TrimStart('/')}",
                    DanceType = p.DanceTypes.First().DanceType,
                    Count = p.PersonalCourses
                        .Where(c => !c.IsBooking &&
                                   (c.CourseDate > today ||
                                   (c.CourseDate == today &&
                                    c.StartTime > now.TimeOfDay)))
                        .Count(),
                    RateCount = p.RateCount,
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
                                           .Where(input.CourseDate == null,p => p.CourseDate >= DateTime.Now.Date)
                                           .Where(input.CourseDate != null, p => p.CourseDate == input.CourseDate && p.CourseDate >= DateTime.Now.Date )
                                           .Where(input.TeacherId != null, p => p.TeacherId == input.TeacherId)
                                           .Select(c => new
                                           {
                                               CourseId = c.Id,
                                               TeacherId = c.TeacherId,
                                               Title = c.Title,
                                               CourseType = c.CourseType,
                                               StartTime = c.StartTime,
                                               EndTime = c.EndTime,
                                               DanceType = c.DanceType,
                                               CourseDate = c.CourseDate,
                                               PosterUrl = $"{baseUrl}/{c.PosterUrl.TrimStart('/')}",
                                               Description = c.Description,
                                               MaxParticipants = c.MaxParticipants,
                                               IsBooking = c.CourseSignUp.Any(p => p.UserId == __userid && p.IsCancel == false),
                                               Address = c.Address,
                                               Phone = c.Teacher.Phone,
                                               SignUpCount = c.CourseSignUp.Count(p => p.IsCancel == false),
                                               TeacherName = c.Teacher.NickName,
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
                .Where(p => p.CourseId == input.CourseId && !p.IsCancel)
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
                .Where(p => p.TeacherId == input.TeacherId)
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
                .Where(p => p.UserId == __userid)
                .Select(p => new
                {
                    MyCardId = p.Id,
                    p.Count,
                    p.TotalCount,
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
                                 .Where(p => p.UserId == __userid)
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
                                 .Where(p => p.UserId == __userid)
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

        /// <summary>
        /// 获取我的课程预约记录
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> GetMyCourseSignUpList(GetMyCourseSignUpListInput input)
        {
            var today = DateTime.Today;
            var dayAfterTomorrow = today.AddDays(2).AddDays(1); // 后天23:59:59
            var baseUrl = ConfigSettingTool.SystemConfig.DomainAddress.TrimEnd('/');
            var query = _db.CourseSignUpRep
                                 .Where(p => p.UserId == __userid)
                                 .Where(input.SearchType == 1 , c => !c.IsCancel && !c.IsFinish )
                                 .Where(input.SearchType == 2, c => c.IsFinish)
                                 .Select(p => new
                                 {
                                     CourseId = p.CourseId,
                                     SignUpId = p.Id,
                                     CourseType = p.Course.CourseType,
                                     DanceType = p.Course.DanceType,
                                     Address = p.Course.Address,
                                     IsCancel = p.IsCancel,
                                     CourseDate = p.Course.CourseDate,
                                     PicUrl = $"{baseUrl}/{p.Course.PosterUrl.TrimStart('/')}",
                                     SelfIntroduce = p.Course.Teacher.SelfIntroduce,
                                     CoureseTitle = p.Course.Title,
                                     Description = p.Course.Description,
                                     StartTime = p.Course.StartTime,
                                     EndTime = p.Course.EndTime,
                                     CreatedTime = p.CreatedTime,
                                     CardCourseType = p.Card.CourseType,
                                     CardDanceType = p.Card.DanceType,
                                     TeacherId = p.Course.TeacherId,
                                     IsFinish = p.IsFinish,
                                     TeacherName = p.Course.Teacher.NickName,
                                     TeacherPhone = p.Course.Teacher.Phone,
                                     MaxParticipants = p.Course.MaxParticipants,
                                 }).OrderByDescending(p => p.SignUpId);
            return await base.ToPage(query, input);
        }

        /// <summary>
        /// 获取我的私教预约记录
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> GetMyPersonalCourseSignUpList(GetMyCourseSignUpListInput input)
        {
            var today = DateTime.Today;
            var dayAfterTomorrow = today.AddDays(2).AddDays(1); // 后天23:59:59
            var baseUrl = ConfigSettingTool.SystemConfig.DomainAddress.TrimEnd('/');
            var query = _db.PersonalCourseSignUpRep
                                 .Where(p => p.UserId == __userid)
                                 .Where(input.SearchType == 1, c => !c.IsCancel && !c.IsFinish)
                                 .Where(input.SearchType == 2, c => c.IsFinish)
                                 .Select(p => new
                                 {
                                     SignUpId = p.Id,
                                     CourseType = CourseTypeEnum.OneOnOne,
                                     DanceType = p.PersonalCourse.DanceType,
                                     Address = p.PersonalCourse.Address,
                                     IsCancel = p.IsCancel,
                                     CourseDate = p.PersonalCourse.CourseDate,
                                     PicUrl = $"{baseUrl}/{p.PersonalCourse.Teacher.AvatarUrl.TrimStart('/')}",
                                     CoureseTitle = p.PersonalCourse.Teacher.NickName + "私教课",
                                     Description = p.PersonalCourse.Description,
                                     StartTime = p.PersonalCourse.StartTime,
                                     EndTime = p.PersonalCourse.EndTime,
                                     CreatedTime = p.CreatedTime,
                                     CardCourseType = p.Card.CourseType,
                                     CardDanceType = p.Card.DanceType,
                                     TeacherId = p.PersonalCourse.TeacherId,
                                     IsFinish = p.IsFinish,
                                 }).OrderByDescending(p => p.SignUpId);
            return await base.ToPage(query, input);
        }

        /// <summary>
        /// 获取课程详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<dynamic> GetCourseSignUpDetail(IdInput input)
        {

            var baseUrl = ConfigSettingTool.SystemConfig.DomainAddress.TrimEnd('/');
            var query = _db.CourseRep.Where(p => p.Id == input.Id)
                                 .Select(p => new
                                 {
                                     CourseId = p.Id,
                                     Title = p.Title,
                                     TeacherName = p.Teacher.NickName,
                                     p.CourseDate,
                                     p.StartTime,
                                     p.EndTime,
                                     p.Address,
                                     TeacherPhone = p.Teacher.Phone,
                                     p.Description,
                                     SignUpCount = p.CourseSignUp.Where(p => p.IsCancel == false).Count(),
                                     p.MaxParticipants,
                                     BookingInfo = p.CourseSignUp.Where(c => c.UserId == __userid && c.IsCancel == false).Select(p => new
                                     {
                                         p.IsFinish,
                                         p.IsCancel,
                                     }).FirstOrDefault(),

                                 });
            return await query.FirstOrDefaultAsync();


        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> GetSignCount() 
        {
            var result = await _db.UserRep
                             .Where(p => p.Id == __userid)
                             .Select(p => new {
                                 // 数量统计（仍在数据库执行）
                                 FinishCourseCount = p.CourseSignUps.Count(c => c.IsFinish),
                                 FinishPersonalCourseCount = p.PersonalCourseSignUps.Count(c => c.IsFinish),
                                 CourseCount = p.CourseSignUps.Count(c => !c.IsCancel && !c.IsFinish),
                                 PersonalCourseCount = p.PersonalCourseSignUps.Count(c => !c.IsCancel && !c.IsFinish),
                                 CourseTimes = p.CourseSignUps
                                     .Where(c => c.IsFinish)
                                     .Select(c => new { c.Course.StartTime, c.Course.EndTime })
                                     .ToList(),
                                 PersonalCourseTimes = p.PersonalCourseSignUps
                                     .Where(c => c.IsFinish)
                                     .Select(c => new { c.PersonalCourse.StartTime, c.PersonalCourse.EndTime })
                                     .ToList()
                             })
                             .AsNoTracking() // 提高性能
                             .FirstOrDefaultAsync();

           
                // 客户端计算总时长（小时）
                var courseTotalHours = result.CourseTimes?
                    .Sum(t => (t.EndTime - t.StartTime).TotalHours) ?? 0;

                var personalCourseTotalHours = result.PersonalCourseTimes?
                    .Sum(t => (t.EndTime - t.StartTime).TotalHours) ?? 0;

                // 最终结果
                var finalResult = new
                {
                    result.FinishCourseCount,
                    result.FinishPersonalCourseCount,
                    result.CourseCount,
                    result.PersonalCourseCount,
                    CourseTotalHours = courseTotalHours,
                    PersonalCourseTotalHours = personalCourseTotalHours
                };
            return finalResult;
        }

        /// <summary>
        /// 获取最近要上的私教课
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> GetRemindCourseList() 
        {
            var query = await _db.UserRep.Where(p => p.Id == __userid).Select(p => new { 
            
             CourseList = p.CourseSignUps.Select(s => new {
                 SignUpId = s.Id,
                 Title = s.Course.Title,
                 s.Course.StartTime,
                 s.Course.EndTime,
                 s.Course.CourseDate,
                 TeacherName = s.Course.Teacher.NickName,
                 Address =s.Course.Address,
                 Type  =1,
             }),
             PersonCourseList = p.PersonalCourseSignUps.Select(s => new {
                 SignUpId = s.Id,
                 Title = s.PersonalCourse.DanceType.GetDescription() + "私教课",
                 s.PersonalCourse.StartTime,
                 s.PersonalCourse.EndTime,
                 s.PersonalCourse.CourseDate,
                 TeacherName = s.PersonalCourse.Teacher.NickName,
                 Address = s.PersonalCourse.Address,
                 Type = 2,
             }),

            }).FirstOrDefaultAsync();
            return query.CourseList.Union(query.PersonCourseList).OrderByDescending(p => p.CourseDate).ThenByDescending(p => p.StartTime);
        
        
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
                var card = await _db.MyCardRep.AsTracking().FirstOrDefaultAsync(p => p.UserId == __userid && p.CourseType == CourseTypeEnum.OneOnOne && list.FirstOrDefault().DanceType == p.DanceType && p.ValidTo >= DateTime.Now);
                if (card == null)
                {
                    return ServiceResult.Fail("你还未拥有该舞蹈类型的私教卡");
                }
                if (input.Ids.Count() != list.Count)
                {
                    return ServiceResult.Fail("选择的时间已经被其他用户预定");
                }
                if (card.Count - list.Count < 0)
                {
                    return ServiceResult.Fail("卡片剩余次数不足以抵消预约时间段数量");
                }
                var signUpList = new List<T_PersonalCourseSignUp>();
                list.ForEach(item =>
                {
                    item.IsBooking = true;
                    T_PersonalCourseSignUp signUp = new T_PersonalCourseSignUp();
                    signUp.UserId = __userid;
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
                if (data == null)
                {
                    return ServiceResult.Fail("课程不存在啊");
                }
                if (data.CourseSignUp.Any(p => p.CourseId == input.Id && p.UserId == __userid && p.IsCancel == false))
                {
                    return ServiceResult.Fail("您已经预定了该课程，请勿重复预约");
                }
                var card = await _db.MyCardRep.AsTracking().FirstOrDefaultAsync(p => p.UserId == __userid && p.CourseType == data.CourseType && p.DanceType == data.DanceType && p.ValidTo >= DateTime.Now);
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
                signUp.UserId = __userid;
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
        /// 微信登录-企业认证版本
        /// </summary>
        public async Task<dynamic> WxLoginByOpenId(WxLoginByOpenIdInput input)
        {
            var baseUrl = ConfigSettingTool.SystemConfig.DomainAddress.TrimEnd('/');
            var resutl = await _wxLoginHelper.Login(input);
            string token = string.Empty;
            // 判断系统中有没有该用户
            var user = await _db.UserRep.FirstOrDefaultAsync(p => p.OpenId == resutl.openId && p.Phone == resutl.phone);

            if (user != null)
            {
                var tokenInfoModel = new TokenInfoModel();
                tokenInfoModel.UserId = user.Id.ToString();
                tokenInfoModel.RoleIds = "";
                user.AvatarUrl = $"{baseUrl}/{user.AvatarUrl.TrimStart('/')}";
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
                newUser.AvatarUrl = $"{baseUrl}/{newUser.AvatarUrl.TrimStart('/')}";
                return new
                {
                    token = TokenTool.CreateToken(tokenInfoModel),
                    user = newUser
                };
            }

        }

        /// <summary>
        /// 微信登录-个人认证版本-电话号码
        /// </summary>
        public async Task<dynamic> WxLoginByPhone(WxLoginByPhoneInput input)
        {
            var baseUrl = ConfigSettingTool.SystemConfig.DomainAddress.TrimEnd('/');
            string token = string.Empty;
            // 判断系统中有没有该用户
            var user = await _db.UserRep.FirstOrDefaultAsync(p => p.Phone == input.Phone);
            if (user != null)
            {
                var tokenInfoModel = new TokenInfoModel();
                tokenInfoModel.UserId = user.Id.ToString();
                tokenInfoModel.RoleIds = "";
                user.AvatarUrl = $"{baseUrl}/{user.AvatarUrl.TrimStart('/')}";
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
                newUser.Phone = input.Phone;
                newUser.Password = "sdfsdfsdfsdfsdfsdfsdfdfsdfsdf";
                newUser.Sex = SexEnum.UNKnown;
                newUser.SelfIntroduce = "爱生活、爱跳舞";
                await _db.AddAsync(newUser);
                await _db.SaveChangesAsync();
                var tokenInfoModel = new TokenInfoModel();
                tokenInfoModel.UserId = newUser.Id.ToString();
                tokenInfoModel.RoleIds = "";
                newUser.AvatarUrl = $"{baseUrl}/{newUser.AvatarUrl.TrimStart('/')}";
                return new
                {
                    token = TokenTool.CreateToken(tokenInfoModel),
                    user = newUser
                };
            }

        }

        #endregion

        #region 更新
        /// <summary>
        ///  取消课程预约
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult> CancelCourseSignUp(IdInput input)
        {

            var data = await _db.CourseSignUpRep.Where(p => p.Id == input.Id && p.UserId == __userid).Include(p => p.Course).AsTracking().FirstOrDefaultAsync();
            if (data.IsCancel)
            {
                return ServiceResult.Fail("已取消预约");
            }
            if (data.IsFinish)
            {
                return ServiceResult.Fail("课程已结束无法取消");
            }
            var startTime = data.Course.CourseDate.AddTicks(data.Course.StartTime.Ticks);
            if (DateTime.Now.AddHours(12) >= startTime)
            {
                return ServiceResult.Fail("无法取消预约，请至少提前12小时取消");
            }
            data.IsCancel = true;
            _db.Update(data);
            await _db.SaveChangesAsync();
            return ServiceResult.Successed();
        }

        /// <summary>
        ///  取消私教预约
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult> CancelPersonalCourseSignUp(IdInput input)
        {
            var tra = await _db.Database.BeginTransactionAsync();
            try
            {

                var data = await _db.PersonalCourseSignUpRep.Where(p => p.Id == input.Id && p.UserId == __userid).Include(p => p.PersonalCourse).Include(p => p.Card).AsTracking().FirstOrDefaultAsync();
                if (data.IsCancel)
                {
                    return ServiceResult.Fail("已取消预约");
                }
                if (data.IsFinish)
                {
                    return ServiceResult.Fail("已结束无法取消");
                }
                var startTime = data.PersonalCourse.CourseDate.AddTicks(data.PersonalCourse.StartTime.Ticks);
                if (DateTime.Now.AddHours(12) >= startTime)
                {
                    return ServiceResult.Fail("无法取消预约，请至少提前12小时取消");
                }
                data.IsCancel = true;
                data.PersonalCourse.IsBooking = false;
                data.Card.Count = data.Card.Count + 1;
                _db.Update(data);
                await _db.SaveChangesAsync();
                await tra.CommitAsync();
            }
            catch (Exception)
            {
                await tra.RollbackAsync();
                throw;
            }

            return ServiceResult.Successed();

        }

        /// <summary>
        /// 定时任务，自动确认已经完成的私教课程
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult> FinshPersonalCourseSignUp()
        {
            var tra = await _db.Database.BeginTransactionAsync();
            try
            {
                var now = DateTime.Now;
                var today = now.Date;
                var finishedCourses = await _db.PersonalCourseSignUpRep
                    .Include(p => p.PersonalCourse)
                    .Where(p => !p.IsCancel && !p.IsFinish)
                    .Where(p => p.PersonalCourse.CourseDate <= today)
                    .Where(p => now.TimeOfDay > p.PersonalCourse.EndTime)
                    .AsTracking()
                    .ToListAsync();
                finishedCourses.ForEach(p =>
                {
                    p.IsFinish = true;
                });
                 _db.UpdateRange(finishedCourses);
                await _db.SaveChangesAsync();
                await tra.CommitAsync();
            }
            catch (Exception ex)
            {
                await tra.RollbackAsync();
                throw ex;
            }
            return ServiceResult.Successed();

        }

        /// <summary>
        /// 定时任务，自动确认已经完成的班级课程
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult> FinshCourseSignUp()
        {
            var tra = await _db.Database.BeginTransactionAsync();
            try
            {
                var now = DateTime.Now;
                var today = now.Date;
                var finishedCourses = await _db.CourseSignUpRep
                    .Include(p => p.Course)
                    .Where(p => !p.IsCancel && !p.IsFinish)
                    .Where(p => p.Course.CourseDate <= today)
                    .Where(p => now.TimeOfDay > p.Course.EndTime)
                    .AsTracking()
                    .ToListAsync();
                finishedCourses.ForEach(p =>
                {
                    p.IsFinish = true;
                });
                _db.UpdateRange(finishedCourses);
                await _db.SaveChangesAsync();
                await tra.CommitAsync();
            }
            catch (Exception ex)
            {
                await tra.RollbackAsync();
                throw ex;
            }
            return ServiceResult.Successed();

        }
        #endregion

        #region 文件上传服务
        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<ServiceResult> UploadAratav(IFormFile file)
        {
            // 获取用户信息
            var user = await _db.UserRep.Where(p => p.Id == __userid).FirstOrDefaultAsync();
            if (user == null)
            {
                return ServiceResult.Fail("用户不存在");
            }

            // 检查是否是默认头像
            bool isDefaultAvatar = user.AvatarUrl == "/Avatars/teacher-default-avatar-girl.png" ||
                                  user.AvatarUrl == "/Avatars/teacher-default-avatar-boy.png";

            // 验证上传文件
            if (file == null || file.Length == 0)
            {
                return ServiceResult.Fail("请选择有效的文件");
            }

            // 验证文件类型
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            if (!allowedExtensions.Contains(fileExtension))
            {
                return ServiceResult.Fail("仅支持JPG、PNG、GIF格式的图片");
            }

            // 准备新头像路径
            var avatarsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Avatars");
            if (!Directory.Exists(avatarsDir))
            {
                Directory.CreateDirectory(avatarsDir);
            }

            // 生成唯一文件名（使用GUID+时间戳）
            var newFileName = $"{Guid.NewGuid().ToString("N")[..8]}_{DateTime.Now:yyMMddHHmmss}{fileExtension}";
            var newFilePath = Path.Combine(avatarsDir, newFileName);
            var newAvatarUrl = $"/Avatars/{newFileName}";

            try
            {
                // 保存新头像
                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // 记录旧头像路径（非默认头像时需要后续删除）
                string oldAvatarPath = null;
                if (!isDefaultAvatar && !string.IsNullOrEmpty(user.AvatarUrl))
                {
                    oldAvatarPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", user.AvatarUrl.TrimStart('/'));
                }

                // 更新数据库
                user.AvatarUrl = newAvatarUrl;
                _db.UserRep.Update(user);
                await _db.SaveChangesAsync();

                // 成功后删除旧头像（如果是自定义头像）
                if (oldAvatarPath != null && System.IO.File.Exists(oldAvatarPath))
                {
                    try
                    {
                        System.IO.File.Delete(oldAvatarPath);
                    }
                    catch (Exception ex)
                    {

                    }
                }

                // 返回完整URL
                var baseUrl = ConfigSettingTool.SystemConfig.DomainAddress.TrimEnd('/');
                var fullUrl = $"{baseUrl}{newAvatarUrl}"; // 注意这里不需要TrimStart，因为newAvatarUrl已经是正确格式

                return ServiceResult.SetData(fullUrl);
            }
            catch (Exception ex)
            {
                // 发生异常时删除可能已上传的新头像
                if (System.IO.File.Exists(newFilePath))
                {
                    System.IO.File.Delete(newFilePath);
                }
                return ServiceResult.Fail($"头像上传失败: {ex.Message}");
            }

        }
        #endregion
    }
}
    
