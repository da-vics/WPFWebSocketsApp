using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cesibet.ViewModels
{
    class ShellViewModel : Conductor<object>.Collection.OneActive
    {
        public ShellViewModel()
        {
            ActivateItemAsync(new GameMenuViewModel());
        }

        public void BackHomeCommand()
        {
            if (Items.Count > 1)
            {
                ActivateItemAsync(new GameMenuViewModel());
                var homePage = Items[0];
                Items.Clear();
                ActivateItemAsync(homePage);
            }
        }
    }
}
