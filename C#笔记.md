# C#笔记

1. It is like the [ref](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/ref) or [out](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/out-parameter-modifier) keywords, except that `in` arguments cannot be modified by the called method. Whereas `ref` arguments may be modified, `out` arguments must be modified by the called method, and those modifications are observable in the calling context.

2. 新建C#NetCore文件
   dotnet new console -o helloworld
   
3. using System.Threading
   Thread.Sleep(5000);//停止5s
   
4. 含有get和set的public属性会自动加入数据库

5. ###### C#HttpClient关闭ssl(https)方法，暨System.Net.Http.WinHttpException: 发生了安全错误 解决方案：

```c#
var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = delegate { return true; };
            
 var client = new HttpClient(handler);
```

6. 在EF Core中，需要使用FluentAPI来确定复合主键。只能使用 Fluent API 配置复合键 - 不能使用约定来设置复合键，也不能使用数据注释来配置复合键。

http://gitblogs.com/blogs/details?id=03351e11-464a-4ba7-afde-4cc69e2625a7

```c#
class MyContext : DbContext
{
    public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>()
            .HasKey(c => new { c.State, c.LicensePlate });
    }
}

class Car
{
    public string State { get; set; }
    public string LicensePlate { get; set; }

    public string Make { get; set; }
    public string Model { get; set; }
}
```

7. 当出现Failed to bind to address https://127.0.0.1:5001: address already in use的故障时。

通过指令`lsof -i:5001`查看该端口的pid状态，发现应该为dotnet...Listened。

然后通过`kill -9 <pid>`杀掉该进程即可解决问题

8. yield完成foreach

1. ```c#
   public delegate T FuncOnOne(T x);
     
          public IEnumerable<T> ForEach(FuncOnOne func)
          {
              var n = head;
              while (n != null)
              {
                  yield return func(n.Data);
                  n = n.Next;
              }
          }
     
          public delegate void ProcudureOnOne(T x);
     
          public void ForEach(ProcudureOnOne func)
          {
              var n = head;
              while (n != null)
              {
                  func(n.Data);
                  n = n.Next;
              }
          }
     
          public delegate T FuncOnTwo(T x, T y);
     
          public T Reduce(FuncOnTwo func, T init)
          {
              var n = head;
              while (n != null)
              {
                  init = func(n.Data, init);
                  n = n.Next;
              }
     
              return init;
          }
      }
     
      class Program
      {
          static void Main(string[] args)
          {
              GenericList<int> list = new GenericList<int>(0);
              foreach (var i in Enumerable.Range(1, 10))
              {
                  list.Add(i);
              }
     
              list.ForEach(x => Console.Write($"{x} "));
              Console.WriteLine();
              Console.WriteLine($"Total: {list.Reduce((x, y) => x + y, 0)}");
              Console.WriteLine($"Max: {list.Reduce(( x, y) => x > y ? x : y, int.MinValue)}");
              Console.WriteLine($"Min: {list.Reduce((x, y) => x < y ? x : y, int.MaxValue)}");
     
              var list2 = list.ForEach(x => x * x);
              foreach (var i in list2)
              {
                  Console.Write($"{i} ");
              }
              Console.ReadLine();
          }
      }
   }
   ```

   