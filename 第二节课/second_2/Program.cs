using System;

namespace second_2
{
    class Program
    {
        static void method(int length,int[] array,out int max,out int min,out int avg)
        {
            int sum  = 0;
            max = int.MinValue;
            min = int.MaxValue;
            for(int i = 0; i < length ; i++)
            {
                if(array[i] > max)
                    max = array[i];
            }
            for(int i = 0; i < length ; i++)
            {
                if(array[i] < min)
                    min = array[i];
            }
            for(int i = 0; i < length ; i++)
            {
                sum +=array[i];
            }
            avg = sum/length;

        }
        static void Main(string[] args)
        {
            int length = 0;
            int[] array;
            int avg = 0;
            int max = int.MinValue;
            int min = int.MaxValue;
            try{
            length = int.Parse(Console.ReadLine());
            array = new int[length];
            for(int i = 0; i < length ; i++)
            {
                array[i] = int.Parse(Console.ReadLine());
                //此输入方法需要换行;
            }
            method(length,array,out max,out min,out avg);
            Console.WriteLine("最大值为:"+max);//结果输入需要在catch之前
            Console.WriteLine("最小值为:"+min);
            Console.WriteLine("平均值为:"+avg);
            }
            catch(InvalidCastException)
            {
                Console.WriteLine("输入格式错误");
            }
            catch(FormatException)
            {
                Console.WriteLine("输入格式错误");
            }
            /*
            另一种输入数组的方式
            string str = Console.ReadLine();
            string[] spl = str.Split(" ");
            int[] nums = Array.ConvertAll<string,int>(spl,int.Parse);
            或者
            int[] nums = Array.ConvertAll(spl,int.Parse);
            */
        }
    }
}
