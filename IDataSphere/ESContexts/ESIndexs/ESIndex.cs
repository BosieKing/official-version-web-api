using Nest;
using Yitter.IdGenerator;

namespace IDataSphere.ESContexts.ESIndexs
{
    /// <summary>
    /// ES基本索引
    /// </summary>
    public class ESIndex
    {
        public ESIndex()
        {
            this.Id = YitIdHelper.NextId().ToString();
            this.CreatedTime = DateTimeOffset.Now;

        }
        /// <summary>
        /// Id
        /// </summary>
        /// <remarks>string类型存储，设置为keyword，避免丢失精度</remarks>
        [Keyword(Name = nameof(Id), Index = true)]
        public string Id { get; set; }

        /// <summary>
        /// 创建时间戳
        /// </summary>
        /// <remarks>使用DateTimeOffset可以避免时区的问题，因为Es的时间类型要求为UTC时间的，也就是带有时区的，用了这个类型就不要用Date特性了</remarks>
        //[Date(Format = "yyyy-MM-dd'T'HH:mm:ssZ")]
        public DateTimeOffset CreatedTime { get; set; }

        /// <summary>
        /// 子索引
        /// </summary>
        /// <remarks>NEST需要它来映射关系配置</remarks>
        public JoinField IndexRelations { get; set; }
    }

    #region 学习数据类型
    /*
     * Es的数据类型分为3种
     * 第一种：最基本的数据类型如integer、long、text、keyword、data
     * 第二种：复合类型，如object对象类型、array对象类型、nested嵌套类型
     * 第三种：特殊类型，ip、坐标，这个不是经常用，所以可以不用多说
     */
    #endregion

    #region 学习映射，即class属性到Es中Index的properties的关系
    /*
     * 首先在ES中，映射通过2个来讲，
     * 第一个mapping parameter映射参数，第二个dynamic mapping 动态映射
     
     

    卡4

     */
    #endregion

    #region 关于ES嵌套对象和父子文档（简单概述）
    /* 
     * ES不是关系型数据库，所以类似于leftjoin或者innerjoin这种操作是么有的，他是分布式的nosql数据库：
     * 那一般我们类似于帖子-点赞多对多的join查询如何设计呢？
     * 第一种:使用嵌套对象，都是放在一个doc里面
     * （一对一的关系）
     * 即设计的index包含一个object的类型用于保存他们的额外信息
     * 他的数据结构为{"Id":["1"],"Data.Name":["BB"], "Data.Age":["12"]}，其中Data就是nest的嵌套对象    
     * 优点：嵌套文档是直接放在document里面的，即用一个字段保存其相关数据，好处就是查询的效率高，保留了嵌套对象内部字段的的关联性
     * 缺点：但是由于保存的其实和一级没什么区别，所以不如转化为一级字段
     * 
     * （一对多的关系）
     * 即设计的index包含一个array的类型用于保存他们的额外信息，(对应netcore中的Array如string[] int[])
     * 他的数据结构为{"Id":["1"],"Datas.Name": ["AA","BB"],"Datas.Age":["11","12"]}，其中Datas就是nest的嵌套数组
     * 优点：所有信息都在一个文档中，没必要join，查询效率高
     * 缺点：1.因为保存在document内部，所以如果要对嵌套对象进行增修删操作，则必须对整个文档进行索引，导致性能下降
     *       2.存储的数据会被扁平化压缩，可以看到上方的不是按我们想要的方式存储的，所以这会导致name和age对应不起来，丢失了他们的关联性
     * 
     * 应用场景：适用于不经常修改、增加、删除的数据，比如携带的tags、详情、订单中携带的多个产品等，
     * 如果是Array类型，还需要额外注意的是如果是Array的尽量保存简单数据类型的数组，否则丢失关联性导致查询返回的数据不是理想值
     * 比如 你想查询age是11 name是bb的数据，上方其实是不符合的，但是bool+must，找出来了age=11的，找出来了name是bb的所以符合返回
     *       
     *  如何实现：在我们的class中，即index的模型类中，使用对象、List或者Array数组，就会是我们的嵌套对象
     * 
     * 
     * 
     * 
     * 第二种，使用嵌套文档  [Nested()]
     * 即在嵌套对象的基础上，映射属性为nested类型，即采用Nested特性标记属性即可，那么得到的数据类型存储就是我们想要的那种类型
     * 他的数据结构为{"Id":["1"],"Datas": [{"Name":["AA"], "Age":["11"]},{"Name":["BB"], "Age":["12"]}]}，其中Datas就是nest的嵌套文档
     * 优点：不会让数组内的元素分离开来
     * 缺点：1.每个嵌套文档都视作为一个独立doc存储，所以当你查询底层数量（_elasticClient.Cat.Indices().ApiCall的Response: "docs.count" : "3",）的时候，
     *         明明只有一条doc但是给你返回数量为3，就是因为doc里面包含了2条嵌套文档，因为这个语法是通过lucene来查询的，lucene视nested为独立文档
     *         所以查询出来3个也就不意外的，而通过聚合查询、search、cat.count则是1，所以可以根据业务情况实际调用
     *       2.其实他并不是真正的存在文档内部，只不过是es内部将我们做了join的处理，所以性能没有上方的嵌套对象好。
     *       3.更新一个嵌套文档，根文档同步更新
     * 
     * 注意：激活索引排序时不能有嵌套字段，查询必须使用特定的nested查询语法，数量方面查询也需要注意
     * 
     * 第三种：父子文档（一对1的关系）        
     * 通过特性[Join("postindex", "favorites")]特性标记object、直接使用属性JoinField、配置join类型设定relations
     * 
     * 好处：子文档和父文档都是独立储存，所以子文档更新不会重新索引父文档，父文档更新子文档也不会重新索引，子文档可以作为独立文档返回。
     * 注意：join类型的字段index只能包含一个，但是可以设置多子文档和多级父子文档
     *       子文档的分片必须和父文档保持在同一个分片上，
     *       索引子文档必须设置routing
     *
     */
    #endregion

    #region 关于路由推断和父子文档
    /*  
     * 第一步：配置父子文档的关系和确定路由
     * 在映射配置的时候，父子文档需要包含一个JoinField属性的字段，父文档是用于mapping的时候映射关系，子文档是需要索引的时候明确 
     *  在Map中，调用.Join(j => j.Name(p => p.IndexRelations)
                        .Relations(r => r.Join("postindex", nameof(LikeChildIndex).ToLower(), nameof(FavoriteChildIndex).ToLower(), nameof(CommentChildIndex).ToLower())))
     * 
     * 第二步：确定索引必填
     * 设置了routing为必填则附加的dsl语句必须带有routing字段，那么目的是保证父子文档在同一个分片是，优化查询性能和减少跨分片查询的复杂性
     * 首先： .RoutingField(p => p.Required() // 设置路由必填
     * 配置路由推断（查询的时候自动使用） 
     * 1.1从类型中推断：nest可以在索引中声明一个Routing（int、long String、Guid类型） 那么翻译的dsl语句会自动映射为_routing字段
     * 1.2从配置中显示指定推断：在ConnectionSettings 中配置.RoutingProperty(p => p.PostIndexId)，PostIndexId就会被翻译成_routing字段（优先级最高）
     *
     * 第三步：链接配置索引
     * 由于ES默认推断是拿的类名小写，由于父子文档存在于同一个索引中，子文档没有单独的索引，那么我们需要配置一下
     *  settings.DefaultMappingFor<子文档类型>(m => m.IndexName(父亲文档名称).RoutingProperty(p => p.PostIndexId));
     *
     * 第四步：插入文档             
     * 1.通过JoinField属性的（此字段不支持term查询）
     *  插入子文档的时候 joinField = JoinField.Link<子文档，父文档>(父文档对象)  = .Link<子文档>(父亲文档id) 
     *  插入父文档的时候 joinField= JoinField.Root<父文档>() = typeof(父文档) = "映射的时候配置的字段"
     *  
     * 2.插入的时候确定相同的路由键
     *  _elasticClient.Index(postIndex, post =>
                        {
                            post.Routing(postIndex.Id); // 父文档填写自己的id，子文档填写父文档的id，
                            return post;
                        }      
     * 注意：启用了路由后，则无法使用IndexDocument来插入文档，必须使用Index，因为IndexDocument不会附加_routing字段
     *       且插入后的文档routing属性不能更改，否则会导致他们不在同一个分片上
     * 
     * 第四步：查询文档 父亲文档为PostIndex、子文档为LikeChildIndex
     * 1.以父查子 
     * 满足父文档id为1001的子文档全部返回
     * var child = _elasticClient.Search<LikeChildIndex>(like => like.Query(q => q.HasParent<PostIndex>(post => post.Query(pq => pq.Term(t => t.Field(f => f.Id).Value(1001))))));
     * 2.以子查父
     * 满足子文档id为1002的父文档全部返回
     * var getParent = _elasticClient.Search<PostIndex>(post => post.Query(q => q.HasChild<LikeChildIndex>(like => like.Query(lq => lq.Term(t => t.Field(f => f.Id).Value(1002))))));

     */



    #endregion

}
