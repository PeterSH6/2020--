using System;

namespace second_3
{
    //3.用“埃氏筛法”求2~ 100以内的素数。2~ 100以内的数，先去掉2的倍数，再去掉3的倍数，再
    //去掉4的倍数，以此类推...最后剩下的就是素数。
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[100];
            for(int i = 0; i < 99; i++)
            {
                array[i] = i+2;
            }
            for(int i = 2; i <= 10 ; i++) //只要判断它不是从2-根号（最大数）的倍数，它就是质数,但是最后需要把10以内
            {
                for(int k = 0 ; k < 99 ; k++)
                {
                    if(array[k] != i && array[k] != 0 && array[k] % i == 0)
                        array[k] = 0;
                }
            }
            for(int k = 0; k< 99;k++)
            {
                if(array[k]!=0)
                    Console.Write(array[k]+" ");
            }
        }
    }
}
