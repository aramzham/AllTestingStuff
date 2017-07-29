using System.ServiceProcess;
using System.Diagnostics;
using System.Timers;
using System.Net;
using System.IO;
using System;

namespace FirstService
{
    public partial class TestService : ServiceBase
    {
        private Timer timer;

        public TestService()
        {
            InitializeComponent();
            timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                // Отправка HTTP запроса.
                const string url = "http://www.microsoft.com";
                var request = (HttpWebRequest)WebRequest.Create(url);
                var response = (HttpWebResponse)request.GetResponse();

                // Сохранение ответа в текстовый файл.
                var path = @"D:\log.txt";

                var file = new FileInfo(path);
                var writer = file.AppendText();

                //TextWriter writer = new StreamWriter(path, true);
                writer.WriteLine(DateTime.Now + " for " + url + ": " + response.StatusCode);
                writer.Close();

                // Закрытие HTTP запроса.
                response.Close();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Application", "Exception: " + ex.Message);
            }
        }

        protected override void OnStart(string[] args)
        {
            timer.Start();
        }

        protected override void OnStop()
        {
            timer.Stop();
        }

        protected override void OnContinue()
        {
            this.OnStart(null);
        }

        protected override void OnPause()
        {
            this.OnStop();
        }

        protected override void OnShutdown()
        {
            this.OnStop();
        }
    }
}
