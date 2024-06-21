using DataSphere.ES;
using Microsoft.AspNetCore.Mvc;
using Model.Commons.Domain;
using SharedLibrary.Enums;

namespace WebApi_Offcial.Controllers.FrontDesk
{
    /// <summary>
    /// 论坛首页管理
    /// </summary>
    [Route("PostHome")]
    [ApiController]
    [ApiDescription(SwaggerGroupEnum.FrontDesk)]
    public class PostHomeController : ControllerBase
    {
        private readonly PostIndexRepository _postIndexRepository;
        public PostHomeController(PostIndexRepository postIndexRepository)
        {
            this._postIndexRepository = postIndexRepository;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("getPostPage")]
        public async Task<ServiceResult> GetPostPage()
        {
            var c = await _postIndexRepository.IndexHases("PostIndex");
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 新增一条帖子
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> AddPost()
        {
            var c = await _postIndexRepository.IndexHases("PostIndex");
            return ServiceResult.Successed();
        }
    }
}
