namespace MyExpenses.ClientCore.Helpers
{
    public interface IHttpInitData
    {
        string BaseAddress { get; }
        string Token { get; }
    }
}
