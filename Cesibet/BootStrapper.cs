using Caliburn.Micro;
using Cesibet.ViewModels;
using System.Windows;

namespace Cesibet
{
    class BootStrapper : BootstrapperBase
    {
        public BootStrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

    }
}
