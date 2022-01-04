using Caliburn.Micro;
using Cesibet.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Cesibet.ViewModels
{
    class QuestionsViewModel : Screen
    {
        public static string Name = "HOST";

        private string _playerName { get; set; }
        public string PlayerName
        {
            get => _playerName;
            set
            {
                _playerName = value;
                NotifyOfPropertyChange();
            }
        }

        private string _question { get; set; }
        public string Question
        {
            get => _question;
            set
            {
                _question = value;
                NotifyOfPropertyChange();
            }
        }


        private string _estimatedNumber { get; set; }
        public string EstimatedNumber
        {
            get => _estimatedNumber;
            set
            {
                if (value.AreDigitsOnly())
                    _estimatedNumber = value;
                NotifyOfPropertyChange();
            }
        }


        private bool _isYes { get; set; }
        public bool IsYes
        {
            get => _isYes;
            set
            {
                _isYes = value;
                NotifyOfPropertyChange();
            }
        }


        private bool _isNo { get; set; }
        public bool IsNo
        {
            get => _isNo;
            set
            {
                _isNo = value;
                NotifyOfPropertyChange();
            }
        }

        private Visibility _btnVisibility { get; set; }
        public Visibility BtnVisibility { get => _btnVisibility; set { _btnVisibility = value; NotifyOfPropertyChange(); } }


        public QuestionsViewModel()
        {
            PlayerName = Name;
            BtnVisibility = Visibility.Visible;
            IsYes = true;
            IsNo = false;
            CreateServer.realeaseBtnQuestions += resetBtnVisibility;
            JoinServer.realeaseBtnQuestions += resetBtnVisibility;
        }

        public static bool IsServer = false;

        public void SetQuestion(int index)
        {
            var genQuestion = new GenerateQuestions();
            Question = genQuestion.Questions[index].Question;
        }

        public void SubmitCommand()
        {
            if (string.IsNullOrEmpty(EstimatedNumber))
            {
                MessageBox.Show("some fields are empty");
                return;
            }

            var str = (IsYes == true) ? "Yes" : "No";

            BtnVisibility = Visibility.Hidden;

            if (IsServer == false)
                JoinServer.sendMessage($"QT:{str}:{EstimatedNumber}");

            else
                CreateServer.AddAnswer(str, Int32.Parse(EstimatedNumber));

        }

        private void resetBtnVisibility(string numberYes, bool done)
        {
            var conductor = this.Parent as IConductor;

            if (done == false)
            {
                ValidationsViewModel validationsViewModel = new ValidationsViewModel();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    BtnVisibility = Visibility.Visible;
                    validationsViewModel.InitReport(numberYes);
                    conductor.ActivateItemAsync(validationsViewModel);
                });
            }

            else
            {
                ResultsViewModel resultsViewModel = new ResultsViewModel();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    BtnVisibility = Visibility.Visible;
                    resultsViewModel.loadData();
                    conductor.ActivateItemAsync(resultsViewModel);
                });
            }
        }

    }//
}
