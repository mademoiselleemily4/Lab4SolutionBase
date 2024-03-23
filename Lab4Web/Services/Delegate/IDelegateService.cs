namespace Lab4Web.Services.Delegate
{
    public interface IDelegateService
    {
        string Introduction(string value, Func<string, string, string> callback);

        string Hello(string firstname, string lastname);
    }
}
