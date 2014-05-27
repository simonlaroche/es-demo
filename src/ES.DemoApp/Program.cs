using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ES.DemoApp
{
    static class Program
    {
        private static Process _process;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Start the event store!
            var processInfo = new ProcessStartInfo(@"..\..\lib\EventStore-NET-v3.0.0rc2\EventStore.SingleNode.exe", "--run-projections=ALL");
            
            _process = new Process();
          
            _process.StartInfo = processInfo;
            _process.Start();
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit +=Application_ApplicationExit;
            Application.Run(new MainForm());
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            _process.Kill();
        }
    }
}
