using System;
using System.Collections.Generic;
using Orders;
using OrderItems;
using System.Linq;
namespace OrderServices
{
    public class OrderService
    {
        private List<Order> order;
        public void addorder(Order sth)
        {
            order.Add(sth);
            Console.WriteLine("成功生成一个新的订单!");
        }
        //在订单删除、修改失败时，能够产生异常并显示给客户错误信息。
        public void delorder(Order sth)
        {
            if(order.Remove(sth))
                Console.WriteLine("成功删除编号为"+sth.orderID+"的订单");
            else throw new Exception();
        }
        public void modiforder(Order sth,string key,string value)
        {

        }
        //（1）使用LINQ语言实现各种查询功能，查询结果按照订单总金额排序返回。
        //查询订单（按照订单号、商品名称、客户等字段进行查询）功能。
        
        public Order searchOrderID(string orderid)
        {
           Order temp =  order.Where(s => s.orderID == orderid).FirstOrDefault();
           Console.WriteLine("");//TODO show函数
           return temp; 
           //根据订单号查询，只有一个订单
        }

        public void searchOrder(string customid = null , string itemid = null)
        {
            var iter1 = from n in order where n.customerID == customid orderby n.sumall select n;
            var iter = from n in iter1 where n.orderitem.Contains() select n;//TODO一个方法搜商品
        }

    }
}