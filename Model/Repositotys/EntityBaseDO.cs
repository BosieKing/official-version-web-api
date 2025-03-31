using System.ComponentModel.DataAnnotations;
using Yitter.IdGenerator;

namespace Model.Repositotys
{
    /// <summary>
    /// 基类
    /// </summary>
    public class EntityBaseDO
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public EntityBaseDO()
        {
            CreatedTime = DateTime.Now;
            Id = YitIdHelper.NextId();
        }

        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 软删除，false0默认有效数据，true1代表非有效数据
        /// </summary>
        public bool IsDeleted { get; set; } = false;
   
    }
    /*
     * 导航属性：导航属性一般是指的在实体类中属性类型为List<T>、ICollection<T>、HashSet<T>类型的属性
     * 1.一般对于少量实体而言用List<T>、大量数据使用HashSet<T>
     * 
     * 2.只有在启用了延迟加载（即只有在访问到属性值的时候才会到数据库查询以提高性能，延迟加载会导致可能出现在循环或者迭代中出现n+1的问题）,要安装其他的包
     *   或者更改跟踪（即手动更改EntityState）的情况下，才能把导航属性设为虚拟导航（加上virtual修饰）
     *   什么意思呢？户表中有一个ICollection<Like> Likes的导航属性，写一个var user =  UserRep.where(p => p.id ==1).first()
     *  （1）设置为virtual且启用延迟加载，Likes属性不会加载数据，只是一个空集合或者空对象，只有当我调用Likes的比如first()，或者访问他的一个具体属性的时候，才会生成sql去查询
     *       好处是延迟加载，这样不会一次性带回来很多likes属性，坏处就是如果在循环中，每访问一次likes，那么都会生成一次sql去查，造成n+1。
     *  （2）不设置为virtual，Likes属性也不会加载数据，只是一个空集合或者空对象，除非你手动调用Include方法告诉EF你需要显示加载该数据出来。
     *   建议：选择显示加载，
     * 
     * 3.无需为导航属性创建数据表，比如public ICollection<Like> Likes ，user和likes是一个1对多的关系，
     *   按照设计数据库来讲，应该要有一个T_UserLike的表储存user和like的关系，不过EF不需要，他会自动管理这些交互
     * 
     * 4.禁止为导航属性创建表达式初始化， 比如ICollection<Like> Likes => new();这样会导致每次访问都创建一个新的集合，无法作为导航属性
     * 
     * 5.无需为导航属性初始化，因为EF中，即使你没有在代码中显式初始化集合导航属性，当EF从数据库中加载实体时，它也会自动处理null值，
     *   并为你创建一个新的空集合（如果数据库中没有关联的数据）
     *   
     * 6.对于可选关系，引用导航必须为空，比如当你创建了一个用户，但是Likes不是必须的那么Like属性必须设置为可空
     *   而Like创建的时候，必须有一个User属性，那么User不能为空。可以通过配置实现
     *   
     *  
     * 7.配置导航：通过OnModelCreating配置关系
     *   当你配置一对多（One-to-Many）关系时，你只需要在一个方向上配置导航属性和外键关系，EF会根据这个配置自动推断出另一方的关系
     */
}
