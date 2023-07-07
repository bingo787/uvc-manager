using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace LionSDKDotDemo
{
    static class Program
    {

        static Mutex mutex = new Mutex(true, "RayonXrayDetectionSystem");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                try
                {
                    // 在此执行你的应用程序逻辑
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Demo());
                    Console.WriteLine("应用程序已启动");

                    Console.ReadLine();
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
            else
            {
                Console.WriteLine("已经有一个实例在运行中");
                Console.ReadLine();
                MessageBox.Show("X光检测软件已经打开，请不要重复打开");
            }


           
        }
    }
}
