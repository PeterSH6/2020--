using System.Collections.Generic;
using System;
using System.Linq;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Org.BouncyCastle.Asn1.Crmf;
using System.Xml.Serialization;
namespace OrderApi.Models
{
    public class Order:IComparable
    {
        [Key]
        public int OrderID{get;set;}
        private List<OrderItem> orderitem = new List<OrderItem>();
        public List<OrderItem> orderitems { get => orderitem; set => orderitem = value; }
        public string customerID{get;set;}
        //private float sum;
        //public float sumall { get { return orderitems.Sum(s => s.sum); }
            //set { sum = orderitems.Sum(s => s.sum); } }
        //private DateTime n;
        //public DateTime now{ get { return DateTime.Now; } set { n = DateTime.Now; } }

        public Order()
        {
            OrderID = 0;
            orderitems = new List<OrderItem>();
            customerID = "non-ID";
        }
        public Order(int oid,string cid)
        {
            OrderID = oid;
            customerID = cid;
        }
        public Order(int oid,string cid,List<OrderItem> list)
        {
            OrderID = oid;
            customerID = cid;
            foreach(OrderItem p in list)
                orderitems.Add(p);
        }
        //用于增加order商品,此处其实只用输入一个ID便可对应到某样商品的名称和价格
        //因此参数只有id和num，名字和价格为了简便只取默认值
        public void AddItem(OrderItem temp,int num)
        {
            //OrderItem sth = new OrderItem(id,"sth",num,2);
            temp.num = num;
            if(orderitems.Contains(temp))
                throw new OrderException($"orderItem-{temp} is existed!");
            orderitems.Add(temp);
            Console.WriteLine("在订单"+OrderID+"增加商品"+temp.ItemID+",名称为:"+ temp.itemName+",份数:"+num+",价格:"+2);
        }
        //减少订单明细
        public void DelItem(OrderItem sth)
        {
            //OrderItem sth = SearchItemID(itemid);
            if(orderitems.Remove(sth))
                Console.WriteLine("成功删除编号为"+sth.ItemID+"的商品");
            else throw new OrderItemException("编号为"+sth.ItemID+"的商品不存在");
        }
        //用于修改商品明细，一般可以修改的为商品的数量,此方法只能通过OrderService来调用,因此为internal
        internal void ModifItem(OrderItem p,int modify)
        {
            //OrderItem p = SearchItemID(itemid);//此处不成功可抛出异常
            if(orderitems.Count(t => t.ItemID == p.ItemID) == 0)
                throw new OrderItemException("编号为"+p.ItemID+"的商品"+p.itemName+"不存在");
            p.num = modify;
            Console.WriteLine("修改订单"+OrderID+"中的商品"+p.ItemID+",份数为"+modify);
        }
        //在订单中通过商品号搜索特定的订单明细
        internal OrderItem SearchItemID(string itemid)
        {
           OrderItem temp =  orderitems.Where(s => s.ItemID == itemid).FirstOrDefault();
           if(temp == null) throw new OrderItemException("编号为"+itemid+"的商品不存在");
           else
           {
               return temp;
           } 
           //根据商品号查询，只有此一种商品
        }
        //System.Linq.Expressions.Expression<Func<Charity, bool>>
        internal bool InOrder(string itemid)
        {
            var iter = from n in orderitems where n.ItemID != null select n.ItemID;
            foreach(string temp in iter)
            {
                if(temp.Equals(itemid))
                    return true;
            }
            return false;
        }


        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach(OrderItem p in orderitems)
            {
                sb.Append(p.ToString());
            }
            string temp = "Order ID: "+OrderID  + Environment.NewLine 
                    +"Customer ID: "+ customerID + Environment.NewLine
                    +"Time: "  + Environment.NewLine
                    +"Item List: " + Environment.NewLine
                        + sb.ToString();
            return temp;
        }
        //public bool searchItem(string)
        //作业的订单和订单明细类需要重写Equals方法，确保添加的订单不重复，每个订单的订单明细不重复。
        public override bool Equals(object obj)
        {
            var order = obj as Order;
            return order!= null && OrderID == order.OrderID;
        }

        public int CompareTo(object obj)
        {
            Order p = obj as Order;
            if(p == null)
                throw new System.ArgumentException();
            return this.OrderID.CompareTo(p.OrderID);
        }
    
    }
}