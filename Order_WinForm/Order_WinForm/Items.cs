using System;
using OrderItems;
namespace Items
{
    public static class Item
    {
        public static OrderItem Apple = new OrderItem("1", "apple", 1, 8.0f);

        public static OrderItem Banana = new OrderItem("2", "banana", 1, 10.0f);

        public static OrderItem Pen = new OrderItem("3", "pen", 1, 3.0f);

        public static OrderItem Bottle = new OrderItem("4", "bottle", 1, 15.0f);

        public static OrderItem Egg = new OrderItem("5", "egg", 1, 1.0f);

    }
}