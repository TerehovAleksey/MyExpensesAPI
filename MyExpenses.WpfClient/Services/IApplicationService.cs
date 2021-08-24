namespace MyExpenses.WpfClient.Services
{
    public interface IApplicationService
    {
        GeneralPageType CurrentPage { get; }

        bool CanGoBack { get; }

        void GoToPage(GeneralPageType page);

        void GoBack();
    }
}
