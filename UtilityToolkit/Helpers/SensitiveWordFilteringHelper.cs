using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityToolkit.Helpers
{
    /// <summary>
    /// 敏感词过滤策略枚举
    /// </summary>
    public enum ValidSensitiveWordStrategy
    {
        /// <summary>
        /// 找到所有的敏感词就返回
        /// </summary>
        FindAllWords = 1,
        /// <summary>
        /// 找到第一个敏感词就返回
        /// </summary>
        FindOneWords = 2,
        /// <summary>
        /// 替换掉所有敏感词
        /// </summary>
        Harmonious = 3,
    }

    /// <summary>
    /// 敏感词过滤器帮助类
    /// </summary>
    public class SensitiveWordFilteringHelper
    {
        private static Hashtable SensitiveThesaurus;
        // 启用延迟加载，线程安全
        private static Lazy<SensitiveWordFilteringHelper> helper = new Lazy<SensitiveWordFilteringHelper>(() => new SensitiveWordFilteringHelper());

        /// <summary>
        /// 私有构造函数避免外部实例化
        /// </summary>
        private SensitiveWordFilteringHelper() { }

        /// <summary>
        /// 静态构造函数，第一次调用时候初始化值
        /// </summary>
        static SensitiveWordFilteringHelper()
        {
            // 第一次使用先实例化敏感词库
            SensitiveThesaurus = BuildSensitiveThesaurus();
        }

        /// <summary>
        /// 判断输入的文本是否有敏感词
        /// </summary>
        /// <param name="str">输入的文本</param>
        /// <param name="isFindFirstWordStop">true：找到第一个敏感词就返回 false找到所有敏感词再返回</param>
        /// <param name="isReplace">是否替换敏感词</param>
        /// <param name="replaceStr">替换的字符</param>
        /// <returns>根据策略返回结果，findfirstwordreturn为true时返回单个敏感字，false时返回全部敏感词。isreplace为true时，返回和谐好的文本</returns>
        public StringBuilder Invoke(string str, bool isFindFirstWordStop, bool isReplace, char replaceStr)
        {
            // 获取敏感词库
            Hashtable sensitiveThesaurus;
            if (SensitiveThesaurus == null)
            {
                sensitiveThesaurus = BuildSensitiveThesaurus();
            }
            else
            {
                sensitiveThesaurus = SensitiveThesaurus;
            }
            // 把需要检测的文本转为stringbuilder
            StringBuilder strBuilder = new StringBuilder(str);
            // 存放这个文本里面出现的所有敏感词
            StringBuilder allSensitiveWord = new StringBuilder();
            // 单个敏感词
            StringBuilder sensitiveWord = new StringBuilder();
            for (int startIndex = 0; startIndex <= strBuilder.Length - 1; startIndex++)
            {
                bool result = FindWord(startIndex, sensitiveThesaurus, isReplace, replaceStr, ref strBuilder, ref startIndex, ref sensitiveWord);
                // 有匹配到敏感词
                if (result)
                {
                    // 匹配到敏感词就返回
                    if (isFindFirstWordStop)
                    {
                        return sensitiveWord;
                    }
                    // 找到文本内全部敏感词再返回
                    else
                    {
                        allSensitiveWord.AppendLine(sensitiveWord.ToString());
                        sensitiveWord.Clear();
                    }
                }
            }
            return isReplace ? strBuilder : allSensitiveWord;
        }

        /// <summary>
        /// 查找敏感词
        /// </summary>
        /// <param name="index">字符索引</param>
        /// <param name="sensitiveThesaurus">敏感树枝</param>
        /// <param name="inputStr">需要处理的文本</param>
        /// <param name="sensitiveWord">单个敏感字</param>
        /// <param name="startIndex">下一次开始匹配的索引</param>
        /// <returns>true找到了敏感词，false 没有找到敏感词</returns>
        private bool FindWord(int index, Hashtable sensitiveThesaurus, bool isReplace, char? replaceStr, ref StringBuilder inputStr, ref int startIndex, ref StringBuilder sensitiveWord)
        {
            // 因为每一次递归都是为了找到下一个能否继续匹配，如果找不到的话返回false，如果能找到，那么会一直递归下去，直到无法匹配
            // （这么做就是为了能找到能匹配最完整的敏感词，所以一条完整的递归列的最后一个递归都是false的结果）
            // 那么每个递归都是会从下一个得到结果，如果得到结果是false，则代表下一个字无法匹配下去了，就要判断当前的词是否能构成敏感词了（这里就是找到最完整的敏感词的关键，因为每次递归都是从子回到父）
            // 如果当前词能构成一个完整的敏感词能就返回true，回到上一个递归，代表已经匹配到了一个完整的敏感字。不需要做删除多余已经存储的敏感字的处理
            bool hasFindWord = false;
            // 获取全部的敏感树枝
            Hashtable nextSensitive = sensitiveThesaurus;
            // 判断是不是文本的最后一个字了（递归了才会起作用）
            // 因为正常Invoke方法调用传来的index都不会存在等于字符的长度，只有发生了递归，index会+1，才会有可能进去)
            if (index == inputStr.Length)
            {
                // 这里的fasle，代表没有可递归的空间了，也就是无法继续往下匹配
                return false;
            }
            else
            {
                // 先找到有效的index
                index = FindValidCharIndex(index, inputStr);
                // 如果新的索引和当前索引不匹配，则代表当前索引的字是无效字符               
                // 获取当前字
                string key = inputStr[index].ToString();
                // 判断敏感字有没有在敏感树枝出现（Invoke调用拿到的是全量树枝，递归调用则是分叉）
                if (nextSensitive.ContainsKey(key))
                {
                    // 拿到这个字的分叉（就是他的子集，和代表当前这个字是不是终结点的isend）
                    nextSensitive = nextSensitive[key] as Hashtable;
                    // 保存当前字 
                    sensitiveWord.Append(key);
                    // 刷新下一次开始查找的索引
                    startIndex = index;
                    // 递归调用方法，判断这个字的下一个字是否能继续匹配
                    bool hasfind = FindWord(index + 1, nextSensitive, isReplace, replaceStr, ref inputStr, ref startIndex, ref sensitiveWord);
                    // 这个字的下一个字无法往下匹配了
                    if (!hasfind)
                    {
                        // 判断当前字是不是终结点，如果是删除这个字，然后回退，这里会返回false，因为当前字无法构成一个完整的敏感词，回到上一个递归
                        if (nextSensitive["IsEnd"].GetHashCode() == 0)
                        {
                            sensitiveWord.Remove(sensitiveWord.Length - 1, 1);
                        }
                        else
                        {
                            // 如果当前字是终结点，得到了一个完整的敏感词，和谐当前字，返回true，回到上一个递归
                            if (isReplace)
                            {
                                inputStr[index] = (char)replaceStr;
                            }
                            hasFindWord = true;
                            return hasFindWord;
                        }
                    }
                    else
                    {
                        // 和谐当前字，返回匹配成功，回到上一个递归                     
                        if (isReplace)
                        {
                            inputStr[index] = (char)replaceStr;
                        }
                        hasFindWord = true;
                        return hasFindWord;
                    }
                }
                else
                {
                    // 这个字在敏感词树枝匹配不到了，返回false，代表匹配不到    
                    return hasFindWord;
                }
            }
            // 刷新开始索引
            startIndex = index;
            return hasFindWord;
        }

        /// <summary>
        /// 判断字符不合法字符
        /// </summary>
        /// <param name="value">字</param>
        /// <returns>true代表字是无效字，false代表字是有效字</returns>
        private bool ValidChar(string value)
        {
            bool result = false;
            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            switch (value)
            {
                case "，":
                case "。":
                case "！":
                case "“":
                case "”":
                case "：":
                case "、":
                case "？":
                    result = true;
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// 获取有效的词的索引
        /// </summary>
        /// <param name="index"></param>
        /// <param name="inputstr"></param>
        /// <returns>有效的索引</returns>
        private int FindValidCharIndex(int index, StringBuilder inputstr)
        {
            // 获取当前索引的字
            var text = inputstr[index].ToString();
            // 是无效的
            if (ValidChar(text))
            {
                for (int i = index + 1; i < inputstr.Length; i++)
                {
                    string chars = inputstr[i].ToString();
                    var reuslt = ValidChar(chars);
                    if (!reuslt)
                    {
                        return i;
                    }
                }
            }
            return index;
        }

        /// <summary>
        /// 创建一个敏感字符树
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        private static Hashtable BuildSensitiveThesaurus()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "sensitive-words.txt");
            // 读取敏感词数组，省略结果中包含空字符串的数组元素
            string[] words = File.ReadAllText(path).Split(new string[1] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            // 存储所有敏感词的树干
            Hashtable allSensitiveWords = new Hashtable();
            // 循环所有的单词
            foreach (var word in words)
            {
                // 从总库开始
                Hashtable fatherSensitiveWord = allSensitiveWords;
                for (int i = 0; i < word.Length; i++)
                {
                    char c = word[i];
                    // 如果该字符已经出现在某个树枝的开头则从该分支继续
                    if (fatherSensitiveWord.ContainsKey(c))
                    {
                        fatherSensitiveWord = (Hashtable)fatherSensitiveWord[c];
                    }
                    else
                    {
                        // 否则不存在则添加该节点
                        Hashtable childSensitveWord = new Hashtable();
                        childSensitveWord.Add("IsEnd", 0);
                        fatherSensitiveWord.Add(c, childSensitveWord);
                        // 继续从该节点下操作
                        fatherSensitiveWord = childSensitveWord;
                    }
                    // 如果是最后一个字则
                    if (i == word.Length - 1)
                    {
                        // 判断包不包含isend
                        if (fatherSensitiveWord.ContainsKey("IsEnd"))
                        {
                            // 修改值为1，代表已经能构成一个敏感词
                            fatherSensitiveWord["IsEnd"] = 1;
                        }
                        else
                        {
                            fatherSensitiveWord.Add("IsEnd", 1);
                        }
                    }
                }
            }
            return allSensitiveWords;
        }
    }
}
