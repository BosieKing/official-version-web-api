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
        public SensitiveWordFilteringHelper()
        {



        }

        public void Valid(string validStr, int strategy)
        {
            StringBuilder allWords = new();
            var table = Create(new string[] { "猪头", "笨猪" });
            for (int i = 0; i < validStr.Length; i++)
            {
                StringBuilder word = new();
                int nowStartIndex = i;
                bool result = SearchWord(validStr, table, word, i, ref i);
            }
        }

        /// <summary>
        /// 找寻敏感词
        /// </summary>
        /// <param name="allSensitiveWordsTable">敏感词库</param>
        /// <param name="word">存储找到得敏感词</param>
        /// <param name="startegy">策略</param>
        /// <param name="searchWordLoopIndex">当前递归循环得索引</param>
        /// <param name="validNextLoopIndex">Valid方法下次循环得索引</param>
        /// <returns></returns>
        public bool SearchWord(string validStr, Hashtable allSensitiveWordsTable, StringBuilder word, int searchWordLoopIndex, ref int validNextLoopIndex)
        {
            char key = validStr[searchWordLoopIndex];
            // 如果在总库里面找到了这个key
            if (allSensitiveWordsTable.ContainsKey(key))
            {
                // 从该分支继续找
                allSensitiveWordsTable = (Hashtable)allSensitiveWordsTable[key];               
                word.Append(key);
                validNextLoopIndex = searchWordLoopIndex++;            
                bool result = SearchWord(validStr, allSensitiveWordsTable, word, searchWordLoopIndex, ref validNextLoopIndex);
                // 找不到了
                if (result == false)
                {
                    // 当前不能构成一个敏感词
                    if (allSensitiveWordsTable["IsEnd"].ToString() == "0")
                    {
                        // 移除这个字
                        word.Remove(word.Length - 1, 1);
                        // 索引减1
                        validNextLoopIndex = searchWordLoopIndex--;
                   }
                }
                return result;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 创建一个敏感字符树
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public Hashtable Create(string[] words)
        {
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
