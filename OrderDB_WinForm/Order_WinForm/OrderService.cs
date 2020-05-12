using System;
using System.Collections.Generic;
using Orders;
using System.Linq;
using Exceptions;
using System.Xml.Serialization;
using System.IO;
using OrderItems;
using System.Data.Entity;
using Order_WinForm;
using Items;

namespace OrderServices
{
    [Serializable]
    public class OrderService
    {
        public List<Order> orders; //用于存放Query之后的OrderList
 
        public int id;
        public OrderService()
        {
            orders = new List<Order>();
            id = 0;
        }
        public void Export(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
            //this.orders = Query().ToList();//需要将IQueryable转为List
            using(FileStream fileStream = new FileStream(path,FileMode.Create))
            {
                xmlSerializer.Serialize(fileStream,this.orders);
            }
        }

        public void Import(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(DbSet<Order>));
            this.orders = Query().ToList();
            using(FileStream fileStream = new FileStream(path,FileMode.Open))
            {
                DbSet<Order> temp = (DbSet<Order>)xmlSerializer.Deserialize(fileStream);
                temp.ForEachAsync(order1 => {
                    if(!orders.Contains(order1))
                        orders.Add(order1);
                });
                //this.orders.AddRange((List<Order>)xmlSerializer.Deserialize(fileStream));
            }
        }

        public int generateID()
        {
            id++;
            return id;
        }
        public void Addorder(string customid)
        {
            using (var context = new OrderContext())
            {
                var order = new Order(generateID(), customid);
                if (context.Orders.Select(p => p.customerID).Contains(customid))//不可以直接使用Contains(order)
                    return;//throw new OrderException($"Add Order Error: Order with id {order.OrderID} already exists!");
                context.Orders.Add(order);
                context.SaveChanges();
                this.orders = Query();
                Console.WriteLine("成功生成一个新的订单!");
            }
           
        }
        //在订单删除、修改失败时，能够产生异常并显示给客户错误信息。
        public void Delorder(int orderid)
        {
            using(var context = new OrderContext())
            {
                var order = context.Orders.Include(b => b.orderitems).FirstOrDefault(p => p.OrderID == orderid);
                if (order != null)
                {
                    context.Orders.Remove(order);
                    context.SaveChanges();
                    this.orders = Query();
                }
                else
                    throw new OrderException("订单" + orderid + "无法找到");

            }
        }
        //可以修改的内容为order中的orderitem,可以增加,删除和修改商品
        //key可选择add，del，mod来修改。
        public void Modiforder(int orderid,string key,OrderItem item,int num = 0)
        {
            using (var context = new OrderContext())
            {
                //需要操作Order里面的OrderItems，所以需要使用Include查询
                Order order = context.Orders.Include("OrderItems").FirstOrDefault(p => p.OrderID == orderid);
                if (order != null)
                {
                    if (key == "add")
                    {
                        order.AddItem(item, num);
                        context.SaveChanges();
                    }
                    else if (key == "del")
                    {
                        try
                        {
                            order.DelItem(item);
                            context.SaveChanges();
                        }//此函数可抛出异常
                        catch (OrderItemException e)
                        {
                            throw new OrderException("删除订单" + id + "中的明细失败", e);
                        }
                    } 
                    else if (key == "mod")
                    {
                        try
                        {
                            var orderitemmod = context.OrderItems.FirstOrDefault(p => p.OrderID == orderid && p.ItemID == item.ItemID);
                            if(orderitemmod == null) 
                                throw new OrderItemException("编号为" + item.ItemID + "的商品" + item.itemName + "不存在");
                            orderitemmod.num = num;
                            context.SaveChanges();
                        }
                        catch (OrderItemException e)
                        {
                            throw new OrderException("修改订单" + id + "中的明细失败", e);
                        }
                    }
                    else throw new Exception();
                }
                else
                    throw new OrderException("订单" + orderid + "无法找到");
                this.orders = Query();
            }

        }
        //在每次对数据库操作完之后，需要进行一次查询，返回IQueryable给OrderService中的orders用于数据绑定
        public List<Order> Query()
        {
            using(var context = new OrderContext())
            {
                var query = context.Orders.Include("OrderItems").OrderBy(p => p.OrderID);
                if (query != null)
                    return query.ToList();
                else throw new OrderException("无订单");
            }
        }
        public Order SearchOrderID(int orderid)
        {
            using (var context = new OrderContext())
            {
                Order order = context.Orders.FirstOrDefault(p => p.OrderID == orderid);
                if (order == null) throw new OrderException("无法找到该订单");
                return order;
            }
           //根据订单号查询，只有一个订单
        }

        public List<Order> SearchCustomerID(string customerID)
        { 
            using(var context = new OrderContext())
            {
                var query = context.Orders
                    .Where(p => p.customerID == customerID)
                    .OrderBy(p => p.OrderID);
                if(query == null)
                    throw new OrderException("无法找到符合customerID的订单");
                return query.ToList();
            }
        }
        public IEnumerable<Order> SearchItemID(string itemID)
        {
            return from n in orders where n.InOrder(itemID) orderby n.sumall select n;
        }
        //根据含有某种商品或者用户ID来确定订单,采用双重过滤,其中过滤itemid时需要调用Order中的inOrder方法
        public void SearchOrder(string customid = null , string itemid = null)
        {
            using(var context = new OrderContext())
            {
                var query1 = context.Orders
                    .Where(p => p.customerID == customid)
                    .OrderBy(p => p.sumall);
                var query2 = query1.Where(p => p.InOrder(itemid));
                bool flag = false;
                foreach(Order n in query2)
                {
                    flag = true;
                }
                if (flag == false) throw new OrderException("无法找到符合的订单");
            }
        }

        public void showOrder(Order n)
        {
            Console.WriteLine("订单详情");
            Console.WriteLine(n.ToString());
        }
        //OrderService提供排序方法对保存的订单进行排序。
        //默认按照订单号排序，也可以使用Lambda表达式进行自定义排序。
        /*
        public void Sort()
        {
            orders.Sort();
        }
        public void SortSum()
        {
            orders.Sort((s1,s2) => {return (int)(s1.sumall - s2.sumall);});
        }*/
    }
}