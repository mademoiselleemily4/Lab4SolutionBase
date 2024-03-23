namespace Lab4Web.Services.Lambda
{
    public interface ILambdaService
    {
        Tuple<int, int, int> Test1(int value);

        bool Test2(string value);

        Task<string> Test3Async(string value);
    }
}
