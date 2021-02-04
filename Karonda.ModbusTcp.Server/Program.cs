using DotNetty.Common.Internal.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Threading.Tasks;

namespace Karonda.ModbusTcp.Server
{
    class Program
    {
        static async Task RunServerAsync()
        {
            //InternalLoggerFactory.DefaultFactory.AddProvider(new ConsoleLoggerProvider((s, level) => true, false));

            ModbusResponse response = new ModbusResponse();
            ModbusServer server = new ModbusServer(response);

            await server.Start();

            Console.WriteLine("Server Started");

            //打开PLC
            try
            {
                MyBuffer.S71200.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                goto exitprog;

            }
            //new 即 start(),所以不能在定义处new
            MyBuffer.myTimer = new System.Threading.Timer(MyBuffer.Display, "Processing timer event", 2000, 10000);


            Console.ReadLine();
        exitprog:
            if (MyBuffer.myTimer != null )
            {
               MyBuffer.myTimer.Dispose();
                Console.WriteLine("myTimer Disposed");
            }
            if (MyBuffer.S71200.IsConnected)
            {
                Console.WriteLine("S71200 Closed");
                MyBuffer.S71200.Close();
            }

            await server.Stop();

        }

        static void Main() => RunServerAsync().Wait();
    }
}
