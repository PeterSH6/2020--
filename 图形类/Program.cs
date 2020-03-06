using System;
// // 1. 编写面向对象程序实现长方形、正方形、三角形等形状的类。
// 每个形状类都能计算面积、判断形状是否合法。 请尝试合理使用接口/抽象类、属性来实现。

// 2. 随机创建10个形状对象，计算这些对象的面积之和。 尝试使用简单工厂设计模式来创建对象。

//所有命名，所有的属性方法首字母大写，字段首字母小写
namespace Third
{
    class Program
    {
        public abstract class Shape
        {
            public abstract double area
            {
                get;
            }
            
            public abstract bool isLegal();
        }

        public class Circle:Shape
        {
            private double radius;
            public double MyRadius{ //此处命名应该是MyRaddius而不是myraidus
                get
                {
                    if(isLegal())
                        return radius;
                    else return -1;
                }
                set
                {
                    radius = value;
                }
            }
            public Circle(double radius)
            {
                this.radius = radius;
            }
            public override double area
            {
                get
                {
                    if(isLegal())
                        return Math.PI*radius*radius;
                    else
                    {
                        return -1;
                    }
                }
            }
            public override bool isLegal()
            {
                if(radius <= 0)
                    return false;
                return true;
            }
        }

        public class Triangle:Shape
        {
            private double a;
            private double b;
            private double c;
            public Triangle(double a, double b,double c)
            {          
                this.a = a;
                this.b = b;
                this.c = c;
            }
            public override double area
            {
                get//注意此处写法
                {
                    if(!isLegal()) return -1;
                    double p =(a+b+c)/2;
                    return Math.Sqrt(p*(p-a)*(p-b)*(p-c));
                }
            }

            public override bool isLegal()
            {
                return 
                !(a+b<=c || a+c<=b || b+c<=a || 
                Math.Abs(a-c)>=b || Math.Abs(a-b)>=c || Math.Abs(b-c)>=a|| 
                a<= 0 || b<=0 || c<=0);
            }
        }

        public class Rectangle:Shape
        {
            private double length;
            private double width;
            public double MyLength
            {
                get => length;
                set => length = value;
            }
            public double mywidth
            {
                get => width;
                set => width = value;
            }
            public Rectangle(double length,double width)
            {
                this.length = length;
                this.width = width;
            }
            public override double area
            {
                get
                {
                    if(isLegal())
                        return length*width;
                    else
                    {
                        Console.WriteLine("矩形不合法");
                        return -1;
                    }
                }
            }

            public override bool isLegal(){
                if(length <= 0 || width <= 0)
                    return false;
                else return true;
            }
        }

        public class Square:Rectangle
        {
            public Square(double side):base(side,side){}
        }

        public class Factory
    {
        public static Shape CreateShape(String type, params double[] side)
        {
            switch (type)
            {
                case "Square":
                    return new Square(side[0]);
                case "Circle":
                    return new Circle(side[0]);
                case "Rectangle":
                    return new Rectangle(side[0],side[1]);
                case "Triangle":
                    return new Triangle(side[0],side[1],side[2]);
                default:
                    throw new Exception();// default应该抛出异常，因为用户传来错误参数。
                                          //如果返回null，后面会出现空指针异常
            }
        }
    }

        static void Main(string[] args)
        {
            string[] shapes = new string[4]{"Circle","Triangle","Rectangle","Square"};
            Shape sshape;
            double sumOfShapes = 0;
            int legalNum = 0;
            int seed = 0;
            Random seed1 = new Random(Guid.NewGuid().GetHashCode());
            Random seed2 = new Random(Guid.NewGuid().GetHashCode());
            //Random 对象可以定义在for循环外面。后面直接用next就可以获得不同的随机值。
            while(legalNum < 10)
            {
                seed = seed1.Next(0,4); //作为string数组的下标
                sshape = Factory.CreateShape(shapes[seed],seed2.Next(0,10),seed2.Next(0,10),seed2.Next(0,10));
                
                if(!sshape.isLegal())
                {
                    Console.WriteLine("生成一个非法的"+shapes[seed]);
                    continue;
                }   
                
                Console.WriteLine("生成一个面积为"+sshape.area+"的"+ shapes[seed]);
                legalNum++;
                sumOfShapes+=sshape.area;
            }
            Console.WriteLine("生成的10个合法图形的总面积为"+sumOfShapes);
        }
    }
}
