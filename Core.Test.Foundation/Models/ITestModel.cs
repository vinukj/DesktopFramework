


namespace Core.Test.Foundation.Models
{
    public interface ITestModel
    {
       // void Load(string xml);
        void LoadWeb(string xml);   // Added
        void LoadDesktop(string xml);  // Added
    }
}
