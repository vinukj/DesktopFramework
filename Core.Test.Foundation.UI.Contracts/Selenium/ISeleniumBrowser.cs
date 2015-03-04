//----------------------------------------------------------------------- // 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts.Selenium
{
    public interface ISeleniumBrowser:IBrowser
    {
        void UploadFileManually(IControl browseButton, String fileName) ;
        void DismissAlert(IControl alertActionElement, AlertOption option);
        string ValidateAlertMessage();
    }
}
