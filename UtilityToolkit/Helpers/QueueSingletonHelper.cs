using System.Collections.Concurrent;

namespace UtilityToolkit.Helpers
{
    /// <summary>
    /// 消息队列帮助类-单例模式
    /// </summary>
    public class QueueSingletonHelper<T>
    {
        /// <summary>
        /// 私有函数禁止new
        /// </summary>
        private QueueSingletonHelper() { }

        private static Lazy<QueueSingletonHelper<T>> lazyInstans = new(() => new());

        private static Lazy<ConcurrentQueue<T>> lazyQueueList = new(() => new());

        /// <summary>
        /// 单例
        /// </summary>
        public static QueueSingletonHelper<T> Instance = lazyInstans.Value;

        /// <summary>
        /// 储存数据
        /// </summary>
        private volatile static ConcurrentQueue<T> queueList = lazyQueueList.Value;

        public int Count() => queueList.Count;
        /// <summary>
        /// 获取元素
        /// </summary>
        /// <returns></returns>
        public T GetQueue()
        {
            // 移除并返回第一个元素
            queueList.TryDequeue(out T queueinfo);
            return queueinfo;
        }

        /// <summary>
        /// 获取指定数量的元素
        /// </summary>
        /// <returns></returns>
        public List<T> GetQueue(int count)
        {
            List<T> result = new();
            int num = count > queueList.Count ? queueList.Count : count;
            for (int i = 0; i < num; i++)
            {
                // 移除并返回第一个元素
                queueList.TryDequeue(out T queueinfo);
                result.Add(queueinfo);
            }
            return result;
        }

        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="list"></param>
        public void BatchAdd(List<T> list)
        {
            list.ForEach(p =>
            {
                queueList.Enqueue(p);
            });
        }

        /// <summary>
        /// 添加单个元素
        /// </summary>
        /// <param name="data"></param>
        public void Add(T data)
        {
            queueList.Enqueue(data);
        }
    }
}
