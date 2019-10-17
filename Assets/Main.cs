using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        c08Predicate_Find();
        //c08Predicate();
        //C08Comparison();
        //C08ForEach();
    }

    static List<Pig> GetPigList()
    {
        List<Pig> list = new List<Pig>()
        {
                new Pig(){ PigID=1 ,Name="小猪",Age=1, TypeID=1},
                new Pig(){ PigID=2 , Name="成年猪",Age=2, TypeID=1},
                new Pig(){ PigID=5 , Name="成年猪1",Age=2, TypeID=2},
                new Pig(){ PigID=3 , Name="黑猪",Age=3, TypeID=3},
                new Pig(){ PigID=6 , Name="黑猪1",Age=3, TypeID=4},
                new Pig(){ PigID=4 , Name="八戒",Age=1300, TypeID=5}
         };
        return list;
    }

    static List<PigType> GetPigTypeList()
    {
        List<PigType> plist = new List<PigType>()
        {
             new PigType(){ TypeID=1 , TypeName="家养"},
             new PigType(){ TypeID=2 , TypeName="野生"},
             new PigType(){ TypeID=3 , TypeName="肥猪"},
             new PigType(){ TypeID=4, TypeName="瘦猪"},
             new PigType(){ TypeID=5, TypeName="变异猪"}
        };
        return plist;
    }

    void c08Predicate_Find()
    {
        List<Pig> list = GetPigList();
        Pig pig = list.Find(p => p.PigID == 100);
        if (pig == null)
        {
            Debug.LogError("pig == null");
        }
        else
        {
            Debug.LogError("pig != null");
            Debug.LogError(pig.PigID);
        }
    }

    void c08Predicate()
    {
        List<Pig> list = GetPigList();

        //FindAll
        List<Pig> newlist = list.FindAll(delegate (Pig pig) { return pig.Age == 1 || pig.Name == "八戒"; });
        foreach (var item in newlist)
        {
            Debug.LogError(item.ToString());
        }
    }

    //系统委托 之 Comparison ：用来比较 两个元素大小的委托
    void C08Comparison()
    {
        List<Pig> list = GetPigList();
        list.Sort(delegate (Pig p1, Pig p2) { return p2.Age - p1.Age; });

        foreach (var item in list)
        {
            Debug.LogError(item.ToString());
        }
    }

    void C08ForEach()
    {
        List<Pig> list = GetPigList();
        List<Pig> newlist = list.FindAll(delegate (Pig pig) { return pig.Age > 2; });
        newlist.ForEach(delegate (Pig pig)
        {
            Debug.LogError(pig.ToString());
        });
    }


    void C09Lambda()
    {
        List<Pig> list = GetPigList();
        //1.0 从匿名委托演变到Lambda 表达式的过程演示
        IEnumerable<Pig> slist = list.Where(delegate (Pig pig) { return pig.Age >= 500; });
        //var nlist = list.Where((Pig pig) => { return pig.Age >= 500; });//演变过程1
        //var nlist = list.Where((Pig pig) => pig.Age >= 500);//演变过程2
        //var nlist = list.Where(pig => pig.Age >= 500);//演变过程3

        //2.0 Lambda表达式的应用举例

        //2.1利用lambda 表达式 实现pig集合的倒叙排序
        //list.Sort((x, y) => y.Age - x.Age);

        //2.2利用lambda 表达式 实现list集合的遍历并打印出所有的信息
        //list.ForEach(pig=> { Debug.LogError(pig.ToString()); });
    }

    //10.1 SQO方法Where() 根据条件从集合中查询出返回值
    void SQO_Where()
    {
        List<Pig> list = GetPigList();
        var dd = list.Where(pig => pig.Age == 1);
    }

    void SQO_Count()
    {
        List<Pig> list = GetPigList();
        int count = list.Count(p => p.Name.Contains("猪"));
        Debug.LogError("包含猪字样的数据有：" + count);
    }

    //SQO方法Select() 可以实现将一个集合投影到另外一个集合中
    void SQO_Select()
    {
        List<Pig> list = GetPigList();
        #region 将pig对象集合转换成匿名类对象集合，匿名类对象中属性值可以由程序员自行定义
        //var nlist = list.Select(c => new { pname = c.Name, page = c.Age }).ToList();
        //nlist.ForEach(dd => Debug.LogError(dd.ToString()));
        #endregion

        #region 传统的将pig对象转换成smallpig对象的写法
        //List<SmallPig> slists = new List<SmallPig>();

        //foreach (Pig item in list)
        //{
        //    slists.Add(new SmallPig() { Name = item.Name, Age = item.Age });
        //}
        #endregion

        #region 使用投影方法来将pig对象转换成smallpig对象的写法
        //var smallpiglist = list.Select(c => new SmallPig { Name = c.Name, Age = c.Age }).ToList();
        //smallpiglist.ForEach(c => Debug.LogError(c.ToString()));

        //var smallpiglist = list.Select(c => new SmallPig {Name=c.Name,Age=c.Age }).ToList();
        //smallpiglist.ForEach(c=>Debug.LogError(c.ToString()));

        #endregion
    }

    //Any()如果源序列中的任何元素都通过指定谓词中的测试，则为 true；否则为 false。
    void SQO_Any()
    {
        List<Pig> list = GetPigList();
        bool flg = list.Any(c => c.Age > 2000);
        Debug.LogError(flg);//false
    }

    //SQO方法OrderByDescending() 对List<Pig> 进行倒序排列 ，OrderBy（）正序排列
    void SQO_DesOrderby()
    {
        List<Pig> list = GetPigList();

        //按照pig的年龄倒序排列
        List<Pig> slist = list.OrderByDescending(c => c.Age).ToList();

        //按照pig的年龄正序排列
        list.OrderBy(c => c.Age).ToList();
    }

    #region 10.5 先根据pig对象的年龄倒序排列，再根据pig对象的pigid正序排列
    /*
     * 根据多个条件来排序要注意：
     * 1、第一个排序字段可以使用方法OrderByDescending（）实现倒序排列，OrderBy（）实现正序排列
     * 2、第一个以后的排序字段必须放在ThenByDescending() 倒序排列 或者ThenBy() 正序排列 (切记，不是用OrderBy（）或者OrderByDescending（）)
     */
    void SQO_OrderBy()
    {
        List<Pig> list = GetPigList();
        List<Pig> nlist = list.OrderByDescending(c => c.Age).ThenByDescending(c => c.PigID).ToList();
    }
    #endregion


    #region 10.6 Join() 集合连接

    /*
     * 以下链接查询类似于sqlserver中的inner join链接查询语句
     * select  pigID = c.PigID, pigName = c.Name, pigAge = c.Age, typename = p.TypeName from  piglist c 
     * inner join typelist p on (c.TypeID=p.TypeID)
     */
    static void SQO_Join()
    {
        List<Pig> piglist = GetPigList();
        List<PigType> typelist = GetPigTypeList();

        #region 1.0 链式编程一次性输出结果
        //piglist.Join(
        //   typelist
        //   , c => c.TypeID
        //   , p => p.TypeID
        //    , (c, p) => new { pigID = c.PigID, pigName = c.Name, pigAge = c.Age, typename = p.TypeName }
        //   ).ToList().ForEach(c => Console.WriteLine(c.ToString())); 
        #endregion

        #region 2.0 分解操作

        //1.0 链接集合piglist 和 typelist 得到一个匿名类 以 IEnumerable<TResult> 泛型集合返回
        var res = piglist.Join(
            typelist
            , c => c.TypeID
            , p => p.TypeID
            , (c, p) => new { pigID = c.PigID, pigName = c.Name, pigAge = c.Age, typename = p.TypeName });
        //2.0  使用ToList() 方法将 IEnumerable<TResult> 泛型集合 转换成List<TResult> 集合
        var resList = res.ToList();

        #endregion
    }

    #endregion



    /*
 * select TypeID,count(1) from list group by TypeID
 */
    static void SQO_GroupBy()
    {
        List<Pig> list = GetPigList();

        //1.0 调用GroupBy()方法分组以后返回类型为：IEnumerable<IGrouping<TKey, TSource>>
        var grouplist = list.GroupBy(c => c.TypeID);
        //2.0 将IEnumerable<IGrouping<TKey, TSource>> 利用ToList()方法转换成List<IGrouping<TKey, TSource>> 集合
        var groupList1 = grouplist.ToList();

        //3.0 遍历groupList1 打印出所有的数据
        groupList1.ForEach(c =>
        {
            foreach (var item in c)
            {
            }
        });
    }

    #region 10.8 分页 Skip() 和 Take()
    void SQO_Paging()
    {
        var list = GetPigListByPage(2, 2);
    }
    List<Pig> GetPigListByPage(int pageindex, int pagesize)
    {
        List<Pig> list = GetPigList();
        //1.0 先计算出开始索引值 
        int startIndex = (pageindex - 1) * pagesize;

        //2.0 利用SQO方法 执行对list的分页操作
        var pagelist = list.Skip(startIndex).Take(pagesize);

        return pagelist.ToList();
    }
    #endregion



    #region Linq 的写法
    void C10Linq()
    {
        List<Pig> list = GetPigList();

        //IEnumerable<Pig> slist = list.Where(c => c.Age > 500);
        //slist.ToList().ForEach(c => Console.WriteLine(c.ToString()));

        //利用linq的写法达到上面的效果
        //sql脚本 select * from list where Age>500            
        var llist = (from c in list where c.Age > 500 select c).ToList();
    }
    #endregion


}
