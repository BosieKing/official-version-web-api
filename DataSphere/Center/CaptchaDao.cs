using IDataSphere.DatabaseContexts;
using IDataSphere.Interfaces.Center;
using Microsoft.EntityFrameworkCore;

namespace DataSphere.Center
{
    /// <summary>
    /// 验证码业务访问数据实现类
    /// </summary>
    public class CaptchaDao : BaseDao, ICaptchaDao
    {
        public CaptchaDao(SqlDbContext dbContext) : base(dbContext)
        {
        }


        /// <summary>
        /// 通过Id查询用户电话号码
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> GetPhone(long userId)
        {
            var phone = await dbContext.UserRep.Where(p => p.Id == userId).Select(p => p.Phone).FirstOrDefaultAsync();
            return phone;
        }
    }
}
