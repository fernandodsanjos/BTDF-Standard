using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTask
{
    class Program
    {
        static void Main(string[] args)
        {
            string task = null;
            string integration = null;
            string applicationName = null;
            bool startReceive = false;

            if (args.Length > 0)
            {
                task = args[0];
                applicationName = args[1];
                integration = args[2];
                

                if(args.Length == 4)
                {
                    startReceive = bool.Parse(args[3]);
                }
            }
            else
            {//IntegrationTask.exe "Stop" "Purchase" "INT0123"
                // IntegrationTask.exe "Remove" "Purchase" "INT0123.Purchase.Order"
                task = "Stop";
                applicationName = "Purchase";// args[0];
                integration = "INT0123";// args[1];
            }
            
           

            switch (task)
            {
                case "Remove":
                    Actions.Remove(applicationName,  integration);
                    break;
                case "Stop":
                    Actions.Stop(applicationName, integration);
                    break;
                case "Start":
                    Actions.Start(applicationName, integration, startReceive);
                    break;
               
                default:
                    throw new NotSupportedException($"Action {task} is not supported");
                   
            }

          

        }
    }
}
