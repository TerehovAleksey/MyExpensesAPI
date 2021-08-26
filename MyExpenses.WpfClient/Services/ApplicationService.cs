using MyExpenses.WpfClient.ViewModels;

namespace MyExpenses.WpfClient.Services
{
    public class ApplicationService : BaseViewModel, IApplicationService
    {
        private GeneralPageType previousPage = GeneralPageType.None;

        private GeneralPageType currentPage = GeneralPageType.Dashboard;
        public GeneralPageType CurrentPage { get => currentPage; private set => Set(ref currentPage, value); }

        public bool CanGoBack { get; private set; }

        public ApplicationService()
        {

        }

        public void GoToPage(GeneralPageType page)
        {
            previousPage = CurrentPage;
            if (page == GeneralPageType.Dashboard)
            {
                CurrentPage = GeneralPageType.Login;
            }
            else
            {
                CurrentPage = page;
            }
            CanGoBack = true;
        }

        public void GoBack()
        {
            if (previousPage == GeneralPageType.Dashboard)
            {
                CurrentPage = GeneralPageType.Login;
            }
            else
            {
                CurrentPage = previousPage;
            }
            previousPage = GeneralPageType.None;
            CanGoBack = false;
        }
    }
}
