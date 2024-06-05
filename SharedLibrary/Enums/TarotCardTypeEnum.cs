namespace SharedLibrary.Enums
{
    /// <summary>
    /// 牌类型
    /// </summary>
    public enum TarotCardTypeEnum : byte
    {
        /// <summary>
        /// 大阿卡纳
        /// </summary>
        GrandArcana = 1,
        /// <summary>
        /// 小阿卡纳-权杖
        /// </summary>
        Sceptre = 2,
        /// <summary>
        /// 小阿卡纳-圣杯
        /// </summary>
        Grail = 3,
        /// <summary>
        /// 小阿卡纳-星币
        /// </summary>
        Starcoins = 4,
        /// <summary>
        /// 小阿卡纳-宝剑
        /// </summary>
        Sword = 5,
    }

    /// <summary>
    /// 正位或者逆位
    /// </summary>
    public enum ForwardOrReverse
    {
        /// <summary>
        /// 正位
        /// </summary>
        Forward = 1,
        /// <summary>
        /// 逆位
        /// </summary>
        Reverse = 2,
    }

    /// <summary>
    /// 卡牌描述类型
    /// </summary>
    public enum CardDescriptionTypeEnum : byte
    {
        /// <summary>
        /// 爱情运势
        /// </summary>
        LoveLuck =1,

        /// <summary>
        /// 事业运势
        /// </summary>
        CareerLuck =2,

        /// <summary>
        /// 所属元素过去
        /// </summary>
        Past =3,

        /// <summary>
        /// 所属元素现在
        /// </summary>
        Now =4,

        /// <summary>
        /// 所属元素未来
        /// </summary>
        Future = 5,

        /// <summary>
        /// 阻力/障碍/困难
        /// </summary>
        Obstacles =6,

        /// <summary>
        /// 优势/有利因素
        /// </summary>
        Strengths =7,

        /// <summary>
        /// 环境
        /// </summary>
        Environment =8,

        /// <summary>
        /// 希望
        /// </summary>
        Hope = 9,

        /// <summary>
        /// 其他补充，来自于实际占卜结果
        /// </summary>

        Other =10,
    }
}
