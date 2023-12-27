using IDataSphere.Interface.CenterService;
using IDataSphere.Repositoty;
using Microsoft.EntityFrameworkCore;

namespace DataSphere.Center
{
    /// <summary>
    /// 验证码访问数据库接口
    /// </summary>
    public class CaptchaDao : BaseDao<T_User>, ICaptchaDao
    {
        public CaptchaDao(SqlDbContext dMDbContext) : base(dMDbContext)
        {
        }


        /// <summary>
        /// 通过Id查询用户电话号码
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> GetPhone(long userId)
        {
            var phone = await dMDbContext.UserRep.Where(p => p.Id == userId).Select(p => p.Phone).FirstOrDefaultAsync();
            return phone;
        }
    }
}
