using FestivalApp.BL.Repositories;
using FestivalApp.GUI.Factory;
using FestivalApp.GUI.Messages;
using FestivalApp.GUI.Messages.ArtistMessages;
using FestivalApp.GUI.Messages.FestivalMessages;
using FestivalApp.GUI.Messages.ShowMessages;
using FestivalApp.GUI.Messages.StageMessages;
using FestivalApp.GUI.Messages.TicketMessages;
using FestivalApp.GUI.Services;
using System.Windows;

namespace FestivalApp.GUI.ViewModels
{
    public class ViewModelLocator : ViewModelBase
    {
        private ViewModelBase _currentViewModel;

        public NavBarViewModel NavBarViewModel { get; }

        public FestivalAddViewModel FestivalAddViewModel { get; set; }
        public StageAddViewModel StageAddViewModel { get; set; }
        public ArtistAddViewModel ArtistAddViewModel { get; set; }
        public ShowAddViewModel ShowAddViewModel { get; set; }
        public TicketListFestivalViewModel TicketListFestivalViewModel { get; set; }

        public FestivalListViewModel FestivalListViewModel { get; set; }
        public ArtistListViewModel ArtistListViewModel { get; set; }
        public ShowListViewModel ShowListViewModel { get; set; }
        public StageListViewModel StageListViewModel { get; set; }
        public TicketListViewModel TicketListViewModel { get; set; }

        public FestivalDetailViewModel FestivalDetailViewModel { get; set; }
        public ArtistDetailViewModel ArtistDetailViewModel { get; set; }
        public ShowDetailViewModel ShowDetailViewModel { get; set; }
        public StageDetailViewModel StageDetailViewModel { get; set; }
        public UserDetailViewModel UserDetailViewModel { get; set; }
        public TicketDetailViewModel TicketDetailViewModel { get; set; }

        public ViewModelLocator()
        {
            // Database
            string connerctionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Festival;Integrated Security=True";

            // Registering messages 
            Mediator.Instance.Register<SelectedFestivalMessage>(GoToFestivalDetailPage);
            Mediator.Instance.Register<SelectedArtistMessage>(GoToArtistDetialPage);
            Mediator.Instance.Register<SelectedStageMessage>(GoToStageDetailPage);
            Mediator.Instance.Register<SelectedShowMessage>(GoToShowDetailPage);
            Mediator.Instance.Register<NavBarMessage>(GoToSelectedPage);
            Mediator.Instance.Register<AddedStageMessage>(ShowAddStagePage);
            Mediator.Instance.Register<AddedFestivalMessage>(ShowAddFestivalPage);
            Mediator.Instance.Register<AddedArtistMessage>(ShowAddArtistPage);
            Mediator.Instance.Register<AddedShowMessage>(ShowAddShowPage);
            Mediator.Instance.Register<TicketAddPageMessage>(GoToAddTicketPage);
            Mediator.Instance.Register<TicketBuyMessage>(GoToBuyTicketPage);

            // Repositories
            FestivalRepository festivalRepository = new FestivalRepository(new DefaultDbContextFactory(connerctionString));
            StageRepository stageRepository = new StageRepository(new DefaultDbContextFactory(connerctionString));
            ArtistRepository artistRepository = new ArtistRepository(new DefaultDbContextFactory(connerctionString));
            UserRepository userRepository = new UserRepository(new DefaultDbContextFactory(connerctionString));
            ShowRepository showRepository = new ShowRepository(new DefaultDbContextFactory(connerctionString));
            TicketRepository ticketRepository = new TicketRepository(new DefaultDbContextFactory(connerctionString));

            // ViewModels
            FestivalListViewModel = new FestivalListViewModel(festivalRepository);
            ArtistListViewModel = new ArtistListViewModel(artistRepository);
            ShowListViewModel = new ShowListViewModel(showRepository);
            StageListViewModel = new StageListViewModel(stageRepository);
            TicketListViewModel = new TicketListViewModel(ticketRepository);

            FestivalDetailViewModel = new FestivalDetailViewModel(festivalRepository, userRepository, stageRepository);
            ArtistDetailViewModel = new ArtistDetailViewModel(artistRepository, showRepository);
            ShowDetailViewModel = new ShowDetailViewModel(showRepository, stageRepository, artistRepository);
            StageDetailViewModel = new StageDetailViewModel(stageRepository, showRepository, festivalRepository);
            UserDetailViewModel = new UserDetailViewModel(userRepository, ticketRepository, festivalRepository);
            TicketDetailViewModel = new TicketDetailViewModel(ticketRepository);

            NavBarViewModel = new NavBarViewModel();

            FestivalAddViewModel = new FestivalAddViewModel(festivalRepository);
            StageAddViewModel = new StageAddViewModel(stageRepository);
            ArtistAddViewModel = new ArtistAddViewModel(artistRepository);
            ShowAddViewModel = new ShowAddViewModel(showRepository);
            TicketListFestivalViewModel = new TicketListFestivalViewModel(ticketRepository);

            CurrenViewModel = FestivalListViewModel;
        }

        public ViewModelBase CurrenViewModel
        {
            get { return _currentViewModel; }
            set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    OnPropertyChanged(nameof(_currentViewModel));
                }
            }
        }

        public void ShowAddShowPage(AddedShowMessage message)
        {
            CurrenViewModel = ShowAddViewModel;
            OnPropertyChanged(nameof(CurrenViewModel));
        }

        public void ShowAddArtistPage(AddedArtistMessage message)
        {
            CurrenViewModel = ArtistAddViewModel;
            OnPropertyChanged(nameof(CurrenViewModel));
        }

        public void ShowAddStagePage(AddedStageMessage message)
        {
            CurrenViewModel = StageAddViewModel;
            OnPropertyChanged(nameof(CurrenViewModel));
        }

        public void ShowAddFestivalPage(AddedFestivalMessage message)
        {
            CurrenViewModel = FestivalAddViewModel;
            OnPropertyChanged(nameof(CurrenViewModel));
        }

        public void GoToBuyTicketPage(TicketBuyMessage message)
        {
            CurrenViewModel = TicketListFestivalViewModel;
            OnPropertyChanged(nameof(CurrenViewModel));
        }

        public void GoToAddTicketPage(TicketAddPageMessage message)
        {
            CurrenViewModel = TicketDetailViewModel;
            OnPropertyChanged(nameof(CurrenViewModel));
        }

        public void GoToFestivalDetailPage(SelectedFestivalMessage message)
        {
            CurrenViewModel = FestivalDetailViewModel;
            OnPropertyChanged(nameof(CurrenViewModel));
        }

        public void GoToShowDetailPage(SelectedShowMessage message)
        {
            CurrenViewModel = ShowDetailViewModel;
            OnPropertyChanged(nameof(CurrenViewModel));
        }

        public void GoToArtistDetialPage(SelectedArtistMessage message)
        {
            CurrenViewModel = ArtistDetailViewModel;
            OnPropertyChanged(nameof(CurrenViewModel));
        }

        public void GoToStageDetailPage(SelectedStageMessage message)
        {
            CurrenViewModel = StageDetailViewModel;
            OnPropertyChanged(nameof(CurrenViewModel));
        }

        public void GoToSelectedPage(NavBarMessage message)
        {
            switch (message.Page)
            {
                case "Home":
                    CurrenViewModel = FestivalListViewModel;
                    OnPropertyChanged(nameof(CurrenViewModel));
                    break;
                case "Artists":
                    CurrenViewModel = ArtistListViewModel;
                    OnPropertyChanged(nameof(CurrenViewModel));
                    break;
                case "Tickets":
                    CurrenViewModel = TicketListViewModel;
                    OnPropertyChanged(nameof(CurrenViewModel));
                    break;
                case "AddFestival":
                    CurrenViewModel = FestivalAddViewModel;
                    OnPropertyChanged(nameof(CurrenViewModel));
                    break;
                case "User":
                    CurrenViewModel = UserDetailViewModel;
                    OnPropertyChanged(nameof(CurrenViewModel));
                    break;
            }
        }
    }
}
