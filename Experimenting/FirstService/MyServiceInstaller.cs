using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace FirstService
{
    [RunInstaller(true)]
    public class MyServiceInstaller : Installer
    {
        public MyServiceInstaller()
        {
            this.Installers.Add(new ServiceProcessInstaller { Account = ServiceAccount.LocalSystem });
            this.Installers.Add(new ServiceInstaller { ServiceName = "===== TEST SERVICE =====", Description = "My First NT Service!", StartType = ServiceStartMode.Automatic });
        }
    }
}
