using System.Collections.Generic;
using System;
using OrderItems;
using System.Linq;
namespace Orders
{
    public class Order
    {
        public string orderID{get;set;}
        public List<OrderItem> orderitem = new List<OrderItem>();
        internal float sumall{get{return orderitem.Sum(s=>s.sum);}}
        public string customerID{get;set;}
        public DateTime now{get{return DateTime.Now;}}

        public Order()
        {
            orderID = "non-ID";
            orderitem = new List<OrderItem>();
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
                orderitem.Add(p);
        }
        public void addItem(OrderItem sth)
        {
            orderitem.Add(sth);
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach(OrderItem p in orderitem)
            {
                sb.Append(p.ToString());
            }
            string temp = orderID  + Environment.NewLine 
                        + customerID + Environment.NewLine
                        + now.ToString() + Environment.NewLine
                        + sb.ToString();
            return temp;
        }

        //public bool searchItem(string)

        public override bool Equals(object? obj)
        {
            var order = obj as Order;
            return order!= null && orderID == order.orderID;
        }


        
    }
}