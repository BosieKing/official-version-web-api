using System.Collections;

namespace Model.Commons.CoreData
{
    /// <summary>
    /// 构建树帮助类
    /// </summary>
    public interface ITreeStructure
    {
        /// <summary>
        /// 获取节点id
        /// </summary>
        /// <returns></returns>
        long GetId();

        /// <summary>
        /// 获取节点父id
        /// </summary>
        /// <returns></returns>
        long GetPid();

        /// <summary>
        /// 设置Children
        /// </summary>
        /// <param name="children"></param>
        void SetChildren(IList children);
    }

    /// <summary>
    /// 递归工具类，用于遍历有父子关系的节点，例如菜单树，字典树等等
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeBuildUtil<T> where T : ITreeStructure
    {
        /// <summary>
        /// 顶级节点的父节点Id(默认0)
        /// </summary>
        private long _rootParentId = 0L;

        /// <summary>
        /// 每个节点迭代的深度
        /// </summary>
        private int _depth = 0;

        /// <summary>
        /// 设置顶级节点
        /// </summary>
        /// <param name="rootParentId">查询数据可以设置其他节点为根节点，避免父节点永远是0，查询不到数据的问题</param>
        public void SetRootParentId(long rootParentId)
        {
            _rootParentId = rootParentId;
        }

        /// <summary>
        /// 设置迭代深度
        /// </summary>
        /// <param name="depth">每个节点迭代的深度，即查找子类的深度</param>
        public void SetDepth(int depth)
        {
            _depth = depth;
        }

        /// <summary>
        /// 构造树节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public List<T> Build(List<T> nodes)
        {
            if (nodes.Count == 0)
                return new List<T>();
            // 循环每一条数据找到他的子结构
            var result = nodes.Where(p => p.GetPid() == _rootParentId).ToList();
            result.ForEach(p => BuildChildNodes(nodes, p, new List<T>()));
            return result;
        }

        /// <summary>
        /// 构造子节点集合
        /// </summary>
        /// <param name="totalNodes"></param>
        /// <param name="node"></param>
        /// <param name="childNodeList"></param>
        /// <param name="nowdeep"></param>
        private void BuildChildNodes(List<T> totalNodes, T node, List<T> childNodeList, int nowdeep = 0)
        {
            var nodeSubList = new List<T>();
            if (_depth - 1 == 0 || !nowdeep.Equals(0) && nowdeep.Equals(_depth - 1))
                return;
            nowdeep++;
            totalNodes.ForEach(u =>
            {
                if (u.GetPid().Equals(node.GetId()))
                    nodeSubList.Add(u);
            });
            nodeSubList.ForEach(u => BuildChildNodes(totalNodes, u, new List<T>(), nowdeep));
            childNodeList.AddRange(nodeSubList);
            node.SetChildren(childNodeList);
        }
    }
}
