using Caliburn.Micro;
using Cesibet.Models;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Cesibet.ViewModels
{
    class ResultsViewModel : Screen
    {
        public static string JsonString;

        private ObservableCollection<UserModel> _results { get; set; }
        public ObservableCollection<UserModel> Results { get => _results; set { _results = value; NotifyOfPropertyChange(); } }

        public ResultsViewModel()
        {
            Results = new ObservableCollection<UserModel>();
        }

        public void loadData()
        {
            var dataResult = JsonConvert.DeserializeObject<List<UserModel>>(JsonString);
            foreach (var data in dataResult)
            {
                Results.Add(data);
            }
        }

    }
}
