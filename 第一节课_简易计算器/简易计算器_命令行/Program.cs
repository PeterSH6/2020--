using System;

class Program{   
    static void Main(string[] args)
    {
        Console.WriteLine("请输入一个算式");
        int flag = 0;
        double first = 0;
        double last = 0;
        string ope = null;
        double result = 0;
        try{
            first = double.Parse(Console.ReadLine());
            ope = Console.ReadLine();
            last = double.Parse(Console.ReadLine());
             switch(ope)
            {
                case "+" :
                result = first+last;
                break;

                case "-" :
                result = first-last;
                break;

                case "*" :
                result = first*last;
                break;

                case "/" :
                result = first/last;
                break;

                default:
                {
                    Console.WriteLine("运算符输入错误");
                    flag = 1;
                    break;
                }
            }
        }
        catch(InvalidCastException){
            Console.WriteLine("输入格式有误！");
            return;
        }
        catch(OverflowException){
            Console.WriteLine("算数指溢出");
            return;
        }
        catch(DivideByZeroException)
        {
            Console.WriteLine("不可以除以0");
        } 
        catch(FormatException)
        {
            Console.WriteLine("输入格式有错误！");
        } 
        if(flag != 1)     
            Console.WriteLine(first+ope+last+"={0}",result);
    }

}