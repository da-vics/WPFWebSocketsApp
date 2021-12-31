using Caliburn.Micro;

namespace Cesibet.ViewModels
{
    class GameMenuViewModel : Screen
    {

        public GameMenuViewModel()
        {

        }

        public void CreateServerCommand()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItemAsync(new CreateServerViewModel());
        }

        public void JoinServerCommand()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItemAsync(new JoinServerViewModel());
        }

    }
}
