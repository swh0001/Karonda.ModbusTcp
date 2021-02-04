using System;
using System.Collections.Generic;
using System.Text;
using DotNetty.Buffers;
using S7.Net;
namespace Karonda.ModbusTcp.Server
{
    public static class MyBuffer
    {
        public static IByteBuffer buffer = Unpooled.Buffer();
        // public static ushort[] Registers  { get; private set; }
        public static System.Threading.Timer myTimer;// = new System.Threading.Timer(Display, "Processing timer event", 2000, 1000);
                                                     // 第一个参数是：回调方法，表示要定时执行的方法，第二个参数是：回调方法要使用的信息的对象，或者为空引用，第三个参数是：调用 callback 之前延迟的时间量（以毫秒为单位），指定 Timeout.Infinite 以防止计时器开始计时。指定零 (0) 以立即启动计时器。第四个参数是：定时的时间时隔，以毫秒为单位

        public static Plc S71200 = new Plc(CpuType.S71200, "192.168.0.1", 0, 1);
        //声明一个类型为PLC的内部变量
        //public static bool ErrRead = false;
        static int TimesCalled = 0;
        static ushort reg0;
        public static void Display(object state)
        {
            // S71200.Read("DB5.DBW0");
            //ErrRead = false;
            if (S71200.IsConnected)
            {
                try
                {
                    reg0 = (ushort)S71200.Read("DB5.DBW0");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //ErrRead = true;
                   
                    return;
                }


                Console.WriteLine("{0} {1} keep running. PLC is " + Convert.ToString(reg0), (string)state, ++TimesCalled);
                MyBuffer.buffer.SetUnsignedShort(0, reg0);
            }
        }


    }






}
