namespace SharedLibrary.Consts
{
    /// <summary>
    /// 用户信息缓存库常量
    /// </summary>
    public class UserCacheConst
    {
        /// <summary>
        /// 用户信息Hash表
        /// </summary>
        /// <remarks>Key：常量+租户Id Field：用户id Value：用户信息<see cref="IDao.View.V_UserInfo"/></remarks>
        public const string USER_INFO_TABLE = "UserInfo_HashTable_";

        /// <summary>
        /// 用户黑TokenHash表
        /// </summary>
        /// <remarks>Key：常量 Field：用户id Value：<see cref="Models.CoreDataModels.BlackTokenModel"/></remarks>
        public const string USER_BLACK_TOKEN_TABLE = "User_BlackToken_HashTable";

        /// <summary>
        /// 超管租户标识
        /// </summary>
        /// <remarks>Key：常量+租户id Value：租户id</remarks>
        public const string SUPER_MANAGE_KEY = "Super_Manage_Key_";

        /// <summary>
        /// 用户需要重新刷新普通token的Hash表
        /// </summary>
        /// <remarks>Key：常量 Value：租户id</remarks>
        public const string MAKE_IN_TABLE = "Make_In_HashTable";

    }
}
