using Caliburn.Micro;
using Cesibet.Helpers;
using System.Threading.Tasks;
using System.Windows;

namespace Cesibet.ViewModels
{
    class JoinServerViewModel : Screen
    {
        private string _userName { get; set; }
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                NotifyOfPropertyChange();
            }
        }

        private string _url { get; set; }
        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                NotifyOfPropertyChange();
            }
        }


        public JoinServerViewModel()
        {
            Url = "ws://127.0.0.1:7898/Echo"; // default
        }

        public void JoinCommand()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Url))
            {
                MessageBox.Show("field empty");
                return;
            }

            var conductor = this.Parent as IConductor;
            var joinServer = new JoinServer();

            Task.Run(() =>
            {
                joinServer.connetToServer(Url, UserName);
            });
            //conductor.ActivateItemAsync(new JoinServerViewModel());
        }
    }
}
