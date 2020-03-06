using System;

namespace second
{
    class Program
    {
        static bool primenumber(int i){
            for(int k = 2; k <i/2; k++)
            {
                if(i % k == 0)
                    return false;
            }
            return true;
        }

            static void Main(string[] args)
        {
            try{
            Console.WriteLine("请输入一个数字:");
            int input = int.Parse(Console.ReadLine());
            Console.WriteLine("素数因子是:");
            for(int i = 2; i <= input ; i++)
            {
                if(input % i == 0 && primenumber(i))
                {
                    Console.WriteLine(i);
                    input = input/i;
                }   
            }
            }
            catch(FormatException)
            {
                Console.WriteLine("输入格式错误");
            }
        }

    }
}
