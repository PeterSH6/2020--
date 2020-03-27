using System;
using OrderServices;
using Exceptions;
/*
写一个订单管理的控制台程序，能够实现添加订单、删除订单、修改订单、查询订单（按照订单号、商品名称、客户等字段进行查询）功能。
提示：主要的类有Order（订单）、OrderItem（订单明细项），OrderService（订单服务），
订单数据可以保存在OrderService中一个List中。在Program里面可以调用OrderService的方法完成各种订单操作。
要求：
（1）使用LINQ语言实现各种查询功能，查询结果按照订单总金额排序返回。
（2）在订单删除、修改失败时，能够产生异常并显示给客户错误信息。
（3）作业的订单和订单明细类需要重写Equals方法，确保添加的订单不重复，每个订单的订单明细不重复。
（4）订单、订单明细、客户、货物等类添加ToString方法，用来显示订单信息。
（5）OrderService提供排序方法对保存的订单进行排序。默认按照订单号排序，也可以使用Lambda表达式进行自定义排序。
*/
namespace 第五节课
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderService service = new OrderService();
            Console.WriteLine("订单服务成功开启！");
            Console.WriteLine("请输入用户ID(此处简便不输入了）");
            string customer = "123";//Console.ReadLine() ;
            service.Addorder(customer);
            try
            {
                service.Modiforder("1", "add", "1", 1);
                service.Modiforder("1", "add", "2", 1);
                service.Modiforder("1", "add", "3", 1);
                service.Modiforder("1", "add", "4", 1);
                service.Modiforder("1", "add", "5", 1);
                service.Export("s.xml");
                service.Modiforder("1", "del", "4", 1);
                service.Modiforder("1", "mod", "1", 2);
                service.SearchOrderID("1");
                service.SearchOrder("123");
                service.SearchOrder(null, "3");
                service.Addorder("2");
                service.Modiforder("2", "add", "aa", 5);
                service.SearchOrderID("2");
                service.Delorder("1");
                service.Delorder("3");
            }
            catch (OrderException e)
            {
                Console.WriteLine("出现异常：{0}", e.Message);
                if (e.InnerException != null)
                    Console.WriteLine("出现异常：{0}", e.InnerException.Message);
            }

        }

    }
}
