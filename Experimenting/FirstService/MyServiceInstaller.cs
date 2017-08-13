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
    //// Выбор файла-службы
    //private void Button3_Click(object sender, EventArgs e)
    //{
    //    if (openFileDialog1.ShowDialog() == DialogResult.OK)
    //    {
    //        textBox1.Text = openFileDialog1.SafeFileName;
    //    }
    //}
    //// Устанавка службы.
    //private void Button1_Click(object sender, EventArgs e)
    //{
    //        try
    //        {
    //            ManagedInstallerClass.InstallHelper(new[]
    //                                                    {
    //                                                            openFileDialog1.FileName
    //                                                        });
    //            MessageBox.Show("Сервис " + openFileDialog1.SafeFileName + " установлен!");
    //        }
    //        catch (Exception exc)
    //        {
    //            MessageBox.Show(exc.ToString());
    //        }
    //}
    //// Деинсталяция службы.
    //private void Button2_Click(object sender, EventArgs e)
    //{
    //        try
    //        {
    //            ManagedInstallerClass.InstallHelper(new[] { @"/u", openFileDialog1.FileName });
    //            MessageBox.Show("Сервис " + openFileDialog1.SafeFileName + " деинсталирован!");
    //        }
    //        catch (Exception exc)
    //        {
    //            MessageBox.Show(exc.ToString());
    //        }
    //}
}
