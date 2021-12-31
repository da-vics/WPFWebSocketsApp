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


        public CreateServerViewModel()
        {
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
            var conductor = this.Parent as IConductor;
            var createServer = new CreateServer();

            Task.Run(() =>
            {
                createServer.beginProcess(Int32.Parse(NoOfPlayers), Int32.Parse(NoOfRounds));
            });
            //conductor.ActivateItemAsync(new JoinServerViewModel());
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
