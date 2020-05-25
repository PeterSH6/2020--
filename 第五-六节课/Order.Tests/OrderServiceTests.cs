using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderServices;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Orders;
using OrderItems;
namespace OrderServices.Tests
{

    [TestClass()]
    public class OrderServiceTests
    {

        OrderItem apple = new OrderItem("1", "apple",1, 8.0f);
        OrderItem banana = new OrderItem("2", "banana",1, 10.0f);
        OrderItem pen = new OrderItem("3", "pen",1, 3.0f);
        OrderItem bottle = new OrderItem("4", "bottle",1, 15.0f);
        OrderItem egg = new OrderItem("5", "egg",1, 1.0f);
        OrderItem chicken = new OrderItem("6", "chicken",1, 30.0f);
        OrderItem nothing = new OrderItem("100", "nothing",1, 30.0f);
        public OrderService test;
        public OrderService test1;
        [TestInitialize]
        public void TestInitial()
        {
            test = new OrderService();
            test.Addorder("Peter");
            test.Addorder("Sam");
            test.Addorder("Bob");
            test1 = new OrderService();
        }
        [TestMethod()]
        public void AddorderTest()
        {
            Assert.AreEqual(test.id, 3);
        }
        [TestMethod()]
        public void AddorderTest1()
        {
            Assert.AreEqual(test.order.Count, 3);
        }

        [TestMethod()]
        public void DelorderTest()
        {
            test.Delorder("2");
            Assert.AreEqual(test.order.Count, 2);
        }
        public void DelorderTest1()
        {
            test.Delorder("2");
            Assert.AreEqual(test.order[1].customerID,"Bob" );
        }

        [TestMethod()]
        public void ModiforderTest_add()
        {
            test.Modiforder("1", "add", apple, 3);
            Assert.AreEqual(test.order[0].orderitem.Count, 1);
        }

        [TestMethod()]
        public void ModiforderTest_add1()
        {
            test.Modiforder("1", "add", apple, 3);
            Assert.AreEqual(test.order[0].orderitem[0].num, 3);
        }
        [TestMethod()]
        public void ModiforderTest_add2()
        {
            test.Modiforder("1", "add", apple, 3);
            Assert.AreEqual(test.order[0].orderitem[0].itemID, "1");
        }

        [TestMethod()]
        public void ModiforderTest_del()
        {
            test.Modiforder("1", "add", apple, 3);
            test.Modiforder("1", "add", banana, 2);
            test.Modiforder("1", "add", pen, 5);
            test.Modiforder("1", "add", bottle, 7);
            test.Modiforder("1", "add", egg, 1);
            test.Modiforder("1", "add", chicken, 33);
            test.Modiforder("1", "del", banana);
            Assert.AreEqual(test.order[0].orderitem.Count, 5);
        }
        [TestMethod()]
        public void ModiforderTest_del1()
        {
            test.Modiforder("1", "add", apple, 3);
            test.Modiforder("1", "add", banana, 2);
            test.Modiforder("1", "add", pen, 5);
            test.Modiforder("1", "add", bottle, 7);
            test.Modiforder("1", "add", egg, 1);
            test.Modiforder("1", "add", chicken, 33);
            test.Modiforder("1", "del", banana);
            Assert.AreEqual(test.order[0].orderitem[1].itemID, "3");
        }
        [TestMethod()]
        [ExpectedException(typeof(OrderException))]
        public void ModiforderTest_del2()
        {
            test.Modiforder("1", "add", apple, 3);
            test.Modiforder("1", "add", banana, 2);
            test.Modiforder("1", "add", pen, 5);
            test.Modiforder("1", "add", bottle, 7);
            test.Modiforder("1", "add", egg, 1);
            test.Modiforder("1", "add", chicken, 33);
            test.Modiforder("1", "del", nothing);
        }

        [TestMethod()]
        public void ModiforderTest_mod()
        {
            test.Modiforder("1", "add", apple, 3);
            test.Modiforder("1", "add", banana, 2);
            test.Modiforder("1", "add", pen, 5);
            test.Modiforder("1", "add", bottle, 7);
            test.Modiforder("1", "add", egg, 1);
            test.Modiforder("1", "add", chicken, 33);
            test.Modiforder("1", "mod", banana,4);
            Assert.AreEqual(test.order[0].orderitem[1].itemID, "2");
        }
        [TestMethod()]
        public void ModiforderTest_mod1()
        {
            test.Modiforder("1", "add", apple, 3);
            test.Modiforder("1", "add", banana, 2);
            test.Modiforder("1", "add", pen, 5);
            test.Modiforder("1", "add", bottle, 7);
            test.Modiforder("1", "add", egg, 1);
            test.Modiforder("1", "add", chicken, 33);
            test.Modiforder("1", "mod", banana, 4);
            Assert.AreEqual(test.order[0].orderitem[1].num, 4);
        }

        [TestMethod()]
        public void ModiforderTest_mod2()
        {
            test.Modiforder("1", "add", apple, 3);
            test.Modiforder("1", "add", banana, 2);
            test.Modiforder("1", "add", pen, 5);
            test.Modiforder("1", "add", bottle, 7);
            test.Modiforder("1", "add", egg, 1);
            test.Modiforder("1", "add", chicken, 33);
            test.Modiforder("1", "mod", banana, 4);
            Assert.AreEqual(test.order[0].orderitem.Count, 6);
        }

        [TestMethod()]
        [ExpectedException(typeof(OrderException))]
        public void ModiforderTest_mod3()
        {
            test.Modiforder("1", "add", apple, 3);
            test.Modiforder("1", "add", banana, 2);
            test.Modiforder("1", "add", pen, 5);
            test.Modiforder("1", "add", bottle, 7);
            test.Modiforder("1", "add", egg, 1);
            test.Modiforder("1", "add", chicken, 33);
            test.Modiforder("1", "mod", banana, 4);
            test.Modiforder("1", "mod", nothing, 4);
        }


        [TestMethod()]
        public void SearchOrderIDTest()
        {
            Order temp = test.SearchOrderID("1");
            Assert.AreEqual(temp, test.order[0]);
        }

        [TestMethod()]
        [ExpectedException(typeof(OrderException))]
        public void SearchOrderIDTest1()
        {
            Order temp = test.SearchOrderID("5");
        }

        [TestMethod()]
        public void SortTest()
        {
            test.Sort();
            bool flag = true;
            for(int i = 1; i < test.order.Count; i++)
            {
                if (test.order[i].CompareTo(test.order[i-1]) < 0)
                    flag = false;
            }
            Assert.AreEqual(flag, true);
        }

        [TestMethod()]
        public void SortSumTest()
        {
            test.SortSum();
            bool flag = true;
            for (int i = 1; i < test.order.Count; i++)
            {
                if (test.order[i].sumall < test.order[i - 1].sumall)
                    flag = false;
            }
            Assert.AreEqual(flag, true);
        }
    }
}