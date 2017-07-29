using System.ServiceProcess;

namespace FirstService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var servicesToRun = new ServiceBase[]
            {
                new TestService()
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}
