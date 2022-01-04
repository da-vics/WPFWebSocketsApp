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

        private Visibility _btnVisibility { get; set; }
        public Visibility BtnVisibility { get => _btnVisibility; set { _btnVisibility = value; NotifyOfPropertyChange(); } }

        public JoinServerViewModel()
        {
            BtnVisibility = Visibility.Visible;
            Url = "ws://127.0.0.1:7898/Echo"; // default
        }

        public void JoinCommand()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Url))
            {
                MessageBox.Show("field empty");
                return;
            }

            QuestionsViewModel.Name = UserName;

            BtnVisibility = Visibility.Hidden;
            Task.Run(() =>
            {
                JoinServer.connetToServer(Url, UserName, resetBtnVisibility);
            });
        }

        private void resetBtnVisibility(int index)
        {
            var conductor = this.Parent as IConductor;
            Application.Current.Dispatcher.Invoke(() =>
            {
                BtnVisibility = Visibility.Visible;
                var questionViewModel = new QuestionsViewModel();
                QuestionsViewModel.IsServer = false;
                questionViewModel.SetQuestion(index);
                conductor.ActivateItemAsync(questionViewModel);
            });
        }

    }//
}
