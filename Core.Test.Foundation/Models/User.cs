
using System.Xml;

namespace Core.Test.Foundation.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void Load(XmlElement ele)
        {
            this.UserName = ele.GetAttribute("UserName");
            this.Password = ele.GetAttribute("Password");
        }
    }
}
