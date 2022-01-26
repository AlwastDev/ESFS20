using ESFS20.Core;
using System.Windows;

namespace ESFS20.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public RelayCommandAsync HomeViewCommand { get; set; }
        public RelayCommandAsync QuizViewCommand { get; set; }
        public RelayCommandAsync RegistrationViewCommand { get; set; }
        public RelayCommandAsync AuthorizationViewCommand { get; set; }
        public RelayCommandAsync TheoryViewCommand { get; set; }
        public RelayCommandAsync SupportViewCommand { get; set; }
        public RelayCommandAsync EditTheoryViewCommand { get; set; }
        public RelayCommandAsync EditQuizViewCommand { get; set; }

        public RelayCommandAsync ExitCommand { get; set; }
        public RelayCommandAsync MinimizedCommand { get; set; }
        public RelayCommandAsync ExitAccountCommand { get; set; }

        private HomeViewModel HomeVm { get; set; }
        private QuizViewModel QuizVm { get; set; }
        private RegistrationViewModel RegistrationVm { get; set; }
        private AuthorizationViewModel AuthorizationVm { get; set; }
        private TheoryViewModel TheoryVm { get; set; }
        private SupportViewModel SupportVm { get; set; }
        private EditTheoryViewModel EditTheoryVm { get; set; }
        private EditQuizViewModel EditQuizVm { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            private set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVm = new HomeViewModel();
            QuizVm = new QuizViewModel();
            RegistrationVm = new RegistrationViewModel();
            AuthorizationVm = new AuthorizationViewModel();
            TheoryVm = new TheoryViewModel();
            SupportVm = new SupportViewModel();
            EditTheoryVm = new EditTheoryViewModel();
            EditQuizVm = new EditQuizViewModel();

            CurrentView = HomeVm;

            HomeViewCommand = new RelayCommandAsync(_ => { CurrentView = HomeVm; });

            QuizViewCommand = new RelayCommandAsync(_ => { CurrentView = QuizVm; });

            TheoryViewCommand = new RelayCommandAsync(_ => { CurrentView = TheoryVm; });

            SupportViewCommand = new RelayCommandAsync(_ => { CurrentView = SupportVm; });

            RegistrationViewCommand = new RelayCommandAsync(_ => { CurrentView = RegistrationVm; });

            AuthorizationViewCommand = new RelayCommandAsync(_ => { CurrentView = AuthorizationVm; });

            EditTheoryViewCommand = new RelayCommandAsync(_ => { CurrentView = EditTheoryVm; });

            EditQuizViewCommand = new RelayCommandAsync(_ => { CurrentView = EditQuizVm; });
            ExitCommand = new RelayCommandAsync(_ => { Application.Current.Shutdown(); });

            MinimizedCommand = new RelayCommandAsync(_ =>
            {
                Application.Current.Windows[0]!.WindowState = WindowState.Minimized;
            });

            ExitAccountCommand = new RelayCommandAsync(_ =>
            {
                SplashScreen splash = new SplashScreen();
                Application.Current.Windows[0]?.Close();
                splash.IsAuth = false;
                splash.Show();
            });
        }
    }
}