using System;
using System.Threading;


namespace 第四节课_2
{
    /* 2、使用事件机制，模拟实现一个闹钟功能。闹钟可以有嘀嗒（Tick）事件和响铃（Alarm）两个事件。
    在闹钟走时时或者响铃时，在控制台显示提示信息。 */
    class Program
    {
        public delegate void ClockHandler(ClockEventArgs Args);

        public class ClockEventArgs:EventArgs 
        {
            private DateTime currenttime;
            public DateTime CurrentTime{get=>currenttime;set=>currenttime = value;}
            private DateTime settime;
            public DateTime SetTime{get=>settime;set=>settime = value;}
            
        }
        public class ClockEvent
        {
            private bool flag;//用于记录是否将闹钟按停止
            public event ClockHandler TravelTime;
            public event ClockHandler Ring;

            public void CallClock(DateTime clockring)
            {   //TODO for
                ClockEventArgs args = new ClockEventArgs();
                args.SetTime = clockring;
                while(true)//无限循环
                {
                    args.CurrentTime = System.DateTime.Now;
                    if(DateTime.Compare(args.CurrentTime,args.SetTime) > 0 && !flag)
                    //不可以用==0，很难严格相等，大于即开始闹钟
                    {
                        Console.WriteLine("该起床了！！！");
                        if(Ring != null)
                            Ring(args);
                        if(Console.ReadLine() != null) flag = true;
                        //当用户键入任意值(直接回车），停止闹钟，继续进入走时。
                    }
                    else 
                    {
                        Console.WriteLine("现在的时间是："+ DateTime.Now.ToString());
                        if(TravelTime != null)
                            TravelTime(args);
                    }
                    Thread.Sleep(1000);
                }
            }
        }

        public class User
        {
            static void Main()
            {
                var clockevent = new ClockEvent();
                DateTime set = new DateTime();
                int y,m,d,h,mi,s;
                Console.WriteLine("请输入时间：年，月，日，小时，分钟，秒");
                try{
                    y = int.Parse(Console.ReadLine());
                    m = int.Parse(Console.ReadLine());
                    d = int.Parse(Console.ReadLine());
                    h = int.Parse(Console.ReadLine());
                    mi = int.Parse(Console.ReadLine());
                    s = int.Parse(Console.ReadLine());
                    set = new DateTime(y,m,d,h,mi,s);
                    Console.WriteLine("闹钟时间是："+ set.ToString());
                }
                catch(ArgumentOutOfRangeException)
                {
                    Console.WriteLine("输入的时间超过范围");
                }
                catch(FormatException)
                {
                    Console.WriteLine("输入格式有误");
                }
                finally
                {
                    clockevent.CallClock(set);//开启时钟,无论输入是否正确
                }
            }

            /* static void show(ClockEventArgs args)
            {
            } */
        }
       
    }
}
