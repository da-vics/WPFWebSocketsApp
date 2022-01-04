using Caliburn.Micro;
using Cesibet.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Cesibet.ViewModels
{
    class CreateServerViewModel : Screen
    {
        private string _noOfPlayers { get; set; }
        public string NoOfPlayers
        {
            get => _noOfPlayers;
            set
            {
                if (value.AreDigitsOnly())
                    _noOfPlayers = value;
                NotifyOfPropertyChange();
            }
        }


        private string _noOfRounds { get; set; }
        public string NoOfRounds
        {
            get => _noOfRounds;
            set
            {
                if (value.AreDigitsOnly())
                    _noOfRounds = value;
                NotifyOfPropertyChange();
            }
        }

        private Visibility _btnVisibility { get; set; }
        public Visibility BtnVisibility { get => _btnVisibility; set { _btnVisibility = value; NotifyOfPropertyChange(); } }

        public CreateServerViewModel()
        {
            BtnVisibility = Visibility.Visible;
            NoOfRounds = "0";
            NoOfPlayers = "0";
        }

        public void CreateCommand()
        {
            if (string.IsNullOrEmpty(NoOfPlayers) || string.IsNullOrEmpty(NoOfRounds))
            {
                MessageBox.Show("some fields are empty");
                return;
            }
            var createServer = new CreateServer();
            BtnVisibility = Visibility.Hidden;

            Task.Run(() =>
            {
                createServer.beginProcess(Int32.Parse(NoOfPlayers), Int32.Parse(NoOfRounds), resetBtnVisibility);
            });
        }

        private void resetBtnVisibility(int index)
        {
            var conductor = this.Parent as IConductor;
            Application.Current.Dispatcher.Invoke(() =>
            {
                BtnVisibility = Visibility.Visible;
                var questionViewModel = new QuestionsViewModel();
                QuestionsViewModel.IsServer = true;
                questionViewModel.SetQuestion(index);
                conductor.ActivateItemAsync(questionViewModel);
            });
        }

    }

    public static class StringExtensions
    {
        public static bool AreDigitsOnly(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return true;

            decimal num = 0;
            return decimal.TryParse(text, out num);
        }
    }

}
