using Orders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

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
namespace OrderItems
{
    public class OrderItem
    {
        public int num{get;set;} //商品数量
        public float prize{get;set;} //商品价格
        public float sum{get{return prize*num;}} //总价
        [Key,Column(Order = 0)]//使用复合主键
        public string ItemID{get;set;} //商品ID
        [Key,Column(Order = 1)]
        public int OrderID { get; set; }
        public string itemName{get;set;} //商品名
        /*
        [ForeignKey("OrderID")]
        [System.Xml.Serialization.XmlIgnore]
        public virtual Order Order { get; set; }*/


        public OrderItem()//支持序列化，必须有无参数的构造函数
        {
            ItemID = itemName = null;
            prize = 0;
            num = 0;
            OrderID = 0;
        }
        public OrderItem(string iid,string ina,int num,float p)
        {
            ItemID = iid;
            itemName = ina; 
            this.num = num;
            prize = p;
        }
        public override string ToString()
        {
            return "ItemID: "+ItemID + " " + " ,Item Name: "+itemName + " ,Item Price:" + prize.ToString() + " ,num:" 
                          + num.ToString() + " ,sum: " + sum.ToString() + Environment.NewLine;
        }
        //TODO: 作业的订单和订单明细类需要重写Equals方法，确保添加的订单不重复，每个订单的订单明细不重复。
        public override bool Equals(object obj)
        {
            if(obj is OrderItem)
            {
                OrderItem p = (OrderItem)obj;
                if(p.itemName == itemName && p.ItemID == ItemID)
                    return true;
            }
            return false;
        }
    }
}