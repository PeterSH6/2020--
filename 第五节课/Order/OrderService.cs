using System;
using System.Collections.Generic;
using Orders;
using System.Linq;
using Exceptions;
using System.Xml.Serialization;
using System.IO;
namespace OrderServices
{
    [Serializable]
    public class OrderService
    {
        public List<Order> order;
        public int id;
        public OrderService()
        {
            order = new List<Order>();
            id = 0;
        }

/*
1、在OrderService中添加一个Export方法，可以将所有的订单序列化为XML文件；
    添加一个Import方法可以从XML文件中载入订单
*/
        public void Export(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
            using(FileStream fileStream = new FileStream(path,FileMode.Create))
            {
                xmlSerializer.Serialize(fileStream,this.order);
            }
        }

        public void Import(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
            using(FileStream fileStream = new FileStream(path,FileMode.Open))
            {
                this.order = (List<Order>)xmlSerializer.Deserialize(fileStream);
            }
        }

        public string generateID()
        {
            id++;
            return id.ToString();
        }
        public void Addorder(string customid)
        {
            Order sth = new Order(generateID(),customid);
            order.Add(sth);
            Console.WriteLine("成功生成一个新的订单!");
        }
        //在订单删除、修改失败时，能够产生异常并显示给客户错误信息。//TODO异常
        public void Delorder(string orderid)
        {
            Order sth = order.Where(s => s.orderID == orderid).FirstOrDefault();
            if(sth == null) throw new OrderException("订单"+orderid+"无法找到");
            if(order.Remove(sth))
                Console.WriteLine("成功删除编号为"+sth.orderID+"的订单");
            else throw new OrderException("订单"+orderid+"删除失败");
        }
        //可以修改的内容为order中的orderitem,可以增加,删除和修改商品
        //key可选择add，del，mod来修改。
        public void Modiforder(string orderid,string key,string id,int num = 0)
        {
            Order temp = order.Where(s => s.orderID == orderid).FirstOrDefault();
            if(key == "add")
            {
                temp.AddItem(id,num);
            }
            else if(key == "del")
            {
                try
                {
                    temp.DelItem(id);
                }//此函数可抛出异常
                catch(OrderItemException e)
                {
                    throw new OrderException("删除订单"+id+"中的明细失败",e);
                }
            }
            else if(key == "mod")
            {
                try
                {
                    temp.ModifItem(id,num);//此函数可抛异常
                }
                catch(OrderItemException e)
                {
                    throw new OrderException("修改订单"+id+"中的明细失败",e);
                }
            }
            else throw new Exception();
            
        }
        //（1）使用LINQ语言实现各种查询功能，查询结果按照订单总金额排序返回。
        //查询订单（按照订单号、商品名称、客户等字段进行查询）功能。
        public Order SearchOrderID(string orderid)
        {
           Order temp =  order.Where(s => s.orderID == orderid).FirstOrDefault();
           showOrder(temp);
           return temp; 
           //根据订单号查询，只有一个订单
        }
        //根据含有某种商品或者用户ID来确定订单,采用双重过滤,其中过滤itemid时需要调用Order中的inOrder方法
        public void SearchOrder(string customid = null , string itemid = null)
        {
            var iter1 = from n in order where n.customerID == customid orderby n.sumall select n;
            var iter = from n in iter1 where n.InOrder(itemid) select n;
            bool flag = false;
            foreach(Order n in iter)
            {
                showOrder(n);
                flag = true;
            }
            if(flag == false) Console.WriteLine("没有找到该订单");
        }

        public void showOrder(Order n)
        {
            Console.WriteLine("订单详情");
            Console.WriteLine(n.ToString());
        }
        //OrderService提供排序方法对保存的订单进行排序。
        //默认按照订单号排序，也可以使用Lambda表达式进行自定义排序。
        public void Sort()
        {
            order.Sort();
        }
        public void SortSum()
        {
            order.Sort((s1,s2) => {return (int)(s1.sumall - s2.sumall);});
        }
    }
}