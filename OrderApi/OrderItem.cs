
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace OrderApi.Models
{
    public class OrderItem
    {
        public int num{get;set;} //商品数量
        public float prize{get;set;} //商品价格
        public float sum{get{return prize*num;}} //总价
        public string ItemID{get;set;} //商品ID
        public int OrderID { get; set; }
        public string itemName{get;set;}
    }
}