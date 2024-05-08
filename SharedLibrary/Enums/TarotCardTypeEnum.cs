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
}
