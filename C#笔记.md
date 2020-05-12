# C#笔记

1. It is like the [ref](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/ref) or [out](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/out-parameter-modifier) keywords, except that `in` arguments cannot be modified by the called method. Whereas `ref` arguments may be modified, `out` arguments must be modified by the called method, and those modifications are observable in the calling context.

2. 新建C#NetCore文件
   dotnet new console -o helloworld
   
3. using System.Threading
   Thread.Sleep(5000);//停止5s
   
4. 含有get和set的public属性会自动加入数据库

5. yield完成foreach

6. ```c#
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

   