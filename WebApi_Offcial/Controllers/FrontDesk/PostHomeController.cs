using DataSphere.ES;
using IDataSphere.ESContexts.ESIndexs;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Model.Commons.Domain;
using Model.Commons.SharedData;
using Model.DTOs.FronDesk.PostHomePage;
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
        #region 构造函数
        private readonly PostIndexRepository _postIndexRepository;
        private readonly PostCommentIndexRepository _postComentIndexRepository;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="postIndexRepository"></param>
        public PostHomeController(PostIndexRepository postIndexRepository, PostCommentIndexRepository postComentIndexRepository)
        {
            this._postIndexRepository = postIndexRepository;
            _postComentIndexRepository = postComentIndexRepository;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("getPostPage")]
        public async Task<ServiceResult> GetPostPage([FromQuery] PostSearchInput input)
        {
            var data = await _postIndexRepository.GetPostPage(input);
            return ServiceResult.SetData(data);
        }

        /// <summary>
        /// 查询前10条评论
        /// </summary>
        /// <returns></returns>
        [HttpGet("getComment")]
        public async Task<ServiceResult> GetComment([FromQuery] IdInput input)
        {
            var data = await _postIndexRepository.GetComment(input);
            return ServiceResult.SetData(data);
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增一条帖子
        /// </summary>
        /// <returns></returns>
        [HttpPost("addPost")]
        public async Task<ServiceResult> AddPost([FromBody] AddPostInput input)
        {
            var data = input.Adapt<PostIndex>();
            data.Tags = input.Tag.Split("#", StringSplitOptions.RemoveEmptyEntries);
            await _postIndexRepository.AddPost(data);
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="input">帖子id</param>
        /// <returns></returns>
        [HttpPost("like")]
        public async Task<ServiceResult> Like([FromBody] IdInput input)
        {
            await _postIndexRepository.AddLike(input);
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 收藏
        /// </summary>
        /// <param name="input">帖子id</param>
        /// <returns></returns>
        [HttpPost("favorite")]
        public async Task<ServiceResult> Favorite([FromBody] IdInput input)
        {
            await _postIndexRepository.AddFavorite(input);
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 评论
        /// </summary>
        /// <returns></returns>
        [HttpPost("commnet")]
        public async Task<ServiceResult> Commnet([FromBody] CommentInput input)
        {
            await _postComentIndexRepository.AddCommnet(input);
            return ServiceResult.Successed();
        }
        #endregion

        #region 更新
        #endregion

        #region 删除
        /// <summary>
        /// 删除一个帖子
        /// </summary>
        /// <returns></returns>
        [HttpPost("deletePost")]
        public async Task<ServiceResult> DeletePost(IdInput input)
        {
            await _postIndexRepository.DeletePost(input);
            return ServiceResult.Successed();
        }

        /// <summary>
        /// 删除指定评论
        /// </summary>
        /// <returns></returns>
        [HttpPost("deleteComment")]
        public async Task<ServiceResult> DeleteComment(IdInput input)
        {
            await _postComentIndexRepository.DeleteComment(input);
            return ServiceResult.Successed();
        }
        #endregion
    }
}
