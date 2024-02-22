using FestivalApp.GUI.Commands;
using FestivalApp.GUI.Messages;
using FestivalApp.GUI.Services;
using System.Windows.Input;

namespace FestivalApp.GUI.ViewModels
{
    public class NavBarViewModel
    {
        public ICommand PageCommand { get; }

        public NavBarViewModel()
        {
            PageCommand = new RelayCommand<string>(GoToPage);
        }

        private void GoToPage(string page)
        {
            Mediator.Instance.Send(new NavBarMessage(page));
        }
    }
}
