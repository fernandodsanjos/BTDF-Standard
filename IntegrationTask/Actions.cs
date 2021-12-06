using Microsoft.BizTalk.ExplorerOM;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTask
{
    public class Actions
    {
        
        public static void Remove(string applicationName, string integration)
        {
            Tasks.RemoveIntegration( applicationName, integration);
            Tasks.RemoveResources( applicationName,  integration);
        }

        public static void Start(string applicationName, string integration,bool startReceive)
        {
            

            using (BtsCatalogExplorer catalog = Tasks.GetCatalogExplorer())
            {

                Application application = catalog.Applications[applicationName];

                if (application != null)
                {


                    foreach (ReceivePort port in application.ReceivePorts)
                    {
                        if (port.Name.StartsWith(integration))
                        {
                            foreach (ReceiveLocation loc in port.ReceiveLocations)
                            {
                                loc.Enable = startReceive;
                            }
                        }
                    }

                    foreach (SendPort port in application.SendPorts)
                    {
                        if (port.Name.StartsWith(integration))
                        {
                            port.Status = PortStatus.Started;
                        }
                    }


                    foreach (SendPortGroup port in application.SendPortGroups)
                    {
                        if (port.Name.StartsWith(integration))
                        {
                            port.Status = PortStatus.Started;
                        }
                    }

                    foreach (BtsOrchestration inst in application.Orchestrations)
                    {
                        if (inst.FullName.StartsWith(integration))
                        {


                            inst.Status = OrchestrationStatus.Started;
                        }
                    }

                    catalog.SaveChanges();
                }
            }
        }

        public static void Stop(string applicationName, string integration)
        {
            using (BtsCatalogExplorer catalog = Tasks.GetCatalogExplorer())
            {
               
                Application application = catalog.Applications[applicationName];

                if (application != null)
                {


                    foreach (ReceivePort port in application.ReceivePorts)
                    {
                        if (port.Name.StartsWith(integration))
                        {
                            foreach (ReceiveLocation loc in port.ReceiveLocations)
                            {
                                loc.Enable = false;
                            }
                        }
                    }

                    foreach (BtsOrchestration inst in application.Orchestrations)
                    {
                        if (inst.FullName.StartsWith(integration))
                        {
                            //To enable unenlist of sendports
                            foreach (OrchestrationPort port in inst.Ports)
                            {
                                port.SendPort = null;
                                port.ReceivePort = null;
                            }

                            inst.Status = OrchestrationStatus.Unenlisted;
                        }
                    }

                    foreach (SendPort port in application.SendPorts)
                    {
                        if (port.Name.StartsWith(integration))
                        {
                            port.Status = PortStatus.Bound;
                        }
                    }


                    foreach (SendPortGroup port in application.SendPortGroups)
                    {
                        if (port.Name.StartsWith(integration))
                        {
                            port.Status = PortStatus.Stopped;
                        }
                    }



                    catalog.SaveChanges();
                }
            }
        
    }
    }
}
