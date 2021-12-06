using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Microsoft.BizTalk.ExplorerOM;
using System.Diagnostics;

namespace IntegrationTask
{
    public class Tasks
    {

        public static string mgmtServer = null;
        public static string mgmtDbName = null;
        public static BtsCatalogExplorer GetCatalogExplorer()
        {
           

            using (RegistryKey rk = Registry.LocalMachine)
            {
                using (RegistryKey rk2 = rk.OpenSubKey(@"SOFTWARE\Microsoft\BizTalk Server\3.0\Administration"))
                {
                    mgmtServer = (string)rk2.GetValue("MgmtDBServer");
                    mgmtDbName = (string)rk2.GetValue("MgmtDBName");
                }
            }

            System.Diagnostics.EventLog.WriteEntry("Application", $"GetCatalogExplorer server {mgmtServer} , database {mgmtDbName}");

            BtsCatalogExplorer catalog = new BtsCatalogExplorer();
            catalog.ConnectionString =
                string.Format("Server={0};Initial Catalog={1};Integrated Security=SSPI;", mgmtServer, mgmtDbName);
            return catalog;
        }
        public static void RemoveResources(string applicationName, string integration)
        {

            using (BtsCatalogExplorer catalog = GetCatalogExplorer())
            {

                Application application = catalog.Applications[applicationName];

                if (application != null)
                {

                    if (application.Assemblies != null)
                    {

                        foreach (BtsAssembly ass in application.Assemblies)
                        {

                            if (ass.DisplayName.StartsWith(integration) && ass.Orchestrations.Count > 0)
                            {
                                RemoveResource(applicationName, ass.DisplayName);
                            }
                        }

                        catalog.SaveChanges();

                        foreach (BtsAssembly ass in application.Assemblies)
                        {

                            if (ass.DisplayName.StartsWith(integration) && ass.Pipelines.Count > 0)
                            {
                                RemoveResource(applicationName, ass.DisplayName);
                            }
                        }

                        catalog.SaveChanges();

                        foreach (BtsAssembly ass in application.Assemblies)
                        {

                            if (ass.DisplayName.StartsWith(integration) && ass.Transforms.Count > 0)
                            {
                                RemoveResource(applicationName, ass.DisplayName);
                            }
                        }

                        catalog.SaveChanges();

                        foreach (BtsAssembly ass in application.Assemblies)
                        {

                            if (ass.DisplayName.StartsWith(integration) && ass.Schemas.Count > 0)
                            {
                                RemoveResource(applicationName, ass.DisplayName);
                            }
                        }

                        catalog.SaveChanges();

                    }
                }
            }

        }

        public static void RemoveIntegration(string applicationName, string integration)
        {

            using (BtsCatalogExplorer catalog = GetCatalogExplorer())
            {
                Application application = catalog.Applications[applicationName];

                if (application != null)
                {

                    foreach (ReceivePort port in application.ReceivePorts)
                    {
                        if (port.Name.StartsWith(integration))
                        {
                            catalog.RemoveReceivePort(port);
                        }
                    }

                    foreach (SendPort port in application.SendPorts)
                    {
                        if (port.Name.StartsWith(integration))
                        {
                            catalog.RemoveSendPort(port);
                        }
                    }


                    foreach (SendPortGroup port in application.SendPortGroups)
                    {
                        if (port.Name.StartsWith(integration))
                        {
                            catalog.RemoveSendPortGroup(port);
                        }
                    }



                    catalog.SaveChanges();
                }
            }
        }
       
        private static void RemoveResource(string applicationName,string assemblyName)
        {
            ProcessStartInfo psi = new ProcessStartInfo("BTSTask.exe", $"RemoveResource  /ApplicationName:{applicationName} /Server:{mgmtServer} /Database:{mgmtDbName} /Luid:\"{assemblyName}\"");
            psi.UseShellExecute = false;
            psi.RedirectStandardError = false;
            psi.RedirectStandardInput = false;
            psi.RedirectStandardOutput = false;

            
            Process p = Process.Start(psi);

            p.WaitForExit();
        }
    }
}
