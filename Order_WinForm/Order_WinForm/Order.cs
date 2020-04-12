using System.Collections.Generic;
using System;
using OrderItems;
using System.Linq;
using Exceptions;
namespace Orders
{
    public class Order:IComparable
    {
        public string orderID{get;set;}
        private List<OrderItem> orderitem = new List<OrderItem>();
        public List<OrderItem> orderitems { get => orderitem; set => orderitem = value; }
        public string customerID{get;set;}
        public float sumall { get { return orderitems.Sum(s => s.sum); } }
        public DateTime now{get{return DateTime.Now;}}

        public Order()
        {
            orderID = "non-ID";
            orderitems = new List<OrderItem>();
            customerID = "non-ID";
        }
        public Order(string oid,string cid)
        {
            orderID = oid;
            customerID = cid;
        }
        public Order(string oid,string cid,List<OrderItem> list)
        {
            orderID = oid;
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
            Console.WriteLine("在订单"+orderID+"增加商品"+temp.itemID+",名称为:"+ temp.itemName+",份数:"+num+",价格:"+2);
        }
        //减少订单明细
        public void DelItem(OrderItem sth)
        {
            //OrderItem sth = SearchItemID(itemid);
            if(orderitems.Remove(sth))
                Console.WriteLine("成功删除编号为"+sth.itemID+"的商品");
            else throw new OrderItemException("编号为"+sth.itemID+"的商品不存在");
        }
        //用于修改商品明细，一般可以修改的为商品的数量,此方法只能通过OrderService来调用,因此为internal
        internal void ModifItem(OrderItem p,int modify)
        {
            //OrderItem p = SearchItemID(itemid);//此处不成功可抛出异常
            if(!orderitems.Contains(p))
                throw new OrderItemException("编号为"+p.itemID+"的商品"+p.itemName+"不存在");
            p.num = modify;
            Console.WriteLine("修改订单"+orderID+"中的商品"+p.itemID+",份数为"+modify);
        }
        //在订单中通过商品号搜索特定的订单明细
        internal OrderItem SearchItemID(string itemid)
        {
           OrderItem temp =  orderitems.Where(s => s.itemID == itemid).FirstOrDefault();
           if(temp == null) throw new OrderItemException("编号为"+itemid+"的商品不存在");
           else
           {
               return temp;
           } 
           //根据商品号查询，只有此一种商品
        }

        internal bool InOrder(string itemid)
        {
            var iter = from n in orderitems where n.itemID != null select n.itemID;
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
            string temp = "Order ID: "+orderID  + Environment.NewLine 
                    +"Customer ID: "+ customerID + Environment.NewLine
                    +"Time: " + now.ToString() + Environment.NewLine
                    +"Item List: " + Environment.NewLine
                        + sb.ToString();
            return temp;
        }
        //public bool searchItem(string)
        //作业的订单和订单明细类需要重写Equals方法，确保添加的订单不重复，每个订单的订单明细不重复。
        public override bool Equals(object obj)
        {
            var order = obj as Order;
            return order!= null && orderID == order.orderID;
        }

        public int CompareTo(object obj)
        {
            Order p = obj as Order;
            if(p == null)
                throw new System.ArgumentException();
            return int.Parse(this.orderID).CompareTo(int.Parse(p.orderID));
        }
    
    }
}