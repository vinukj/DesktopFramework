
namespace Core.Test.Foundation.Reports
{
    public interface ITestRunReport
    {
        string GetName();
        bool Generate();
    }
}
