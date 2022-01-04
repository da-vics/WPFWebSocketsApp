using Caliburn.Micro;
using Cesibet.Helpers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Cesibet.ViewModels
{
    class ValidationsViewModel : Screen
    {
        private string _report { get; set; }
        public string Report
        {
            get => _report;
            set
            {
                _report = value;
                NotifyOfPropertyChange();
            }
        }

        private Visibility _btnVisibility { get; set; }
        public Visibility BtnVisibility { get => _btnVisibility; set { _btnVisibility = value; NotifyOfPropertyChange(); } }

        public ValidationsViewModel()
        {
            BtnVisibility = Visibility.Visible;
        }

        public void InitReport(string report) => Report = report;

        public void NextCommand()
        {
            if (QuestionsViewModel.IsServer == true)
            {
                var conductor = this.Parent as IConductor;
                var questionViewModel = new QuestionsViewModel();
                var index = CreateServer.questionIndex();
                questionViewModel.SetQuestion(index);
                conductor.ActivateItemAsync(questionViewModel);
                CreateServer.Sendmessage($"Next:{index}");
            }
            //

            else
            {
                BtnVisibility = Visibility.Hidden;
                Task.Run(() =>
                {
                    while (JoinServer.Pageup == false)
                    {
                        Thread.Sleep(1000);
                    }

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        JoinServer.Pageup = false;
                        BtnVisibility = Visibility.Visible;
                        var conductor = this.Parent as IConductor;
                        var questionViewModel = new QuestionsViewModel();
                        questionViewModel.SetQuestion(JoinServer.CurIndex);
                        conductor.ActivateItemAsync(questionViewModel);
                    });
                });
            }
        }

    }
}
