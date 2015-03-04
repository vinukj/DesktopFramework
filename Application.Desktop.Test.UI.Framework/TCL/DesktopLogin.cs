using Microsoft.VisualStudio.TestTools.UITesting;
//using Product.Desktop.Test.UI.Framework.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Test.Foundation;
using Core.Test.Foundation.UI.Contracts;
using Core.Test.Foundation.UI.Desktop;
using Core.Test.Foundation.UI.Desktop.Muia;

namespace Application.Desktop.Test.UI.Framework.TCL
{
   public class DesktopLogin
    {
       //public static void LogOn(string Database="")
       //{
       //    WpfApplication.DesktopStart();
       //    LoginPage login = new LoginPage(); // Remove the Dalay
       //    login.UserNameTextBox.SetText(TestSession.Config.GetDesktopActiveEnvironment().Users[0].UserName);
       //    login.PasswordTextBox.SetText(TestSession.Config.GetDesktopActiveEnvironment().Users[0].Password);
       //    login.DatabaseDropDown.ClickByMouse();
       //    if (Database != "")
       //    {
       //        try
       //        {
       //            login.DatabaseDropDown.SelectItemByIndex(1);
       //        }
       //        catch (Exception e)
       //        {
       //            DesktopLogin desktoplogin = new DesktopLogin();
       //            login.DatabaseDropDown.SelectItemByText(login.DatabaseDropDown, Database);
       //           // desktoplogin.SelectItemByText(Database);
       //        }
       //    }
       //    login.LoginButton.Click();
       //}

       // public static void Exit()
       // {
       //     LoginPage loginpage = new LoginPage();
       //     loginpage.FileMainMenu.ClickByMouse();
       //     loginpage.MainMenuFile_Exit.ClickByMouse();
            
       // }

       // public void SelectItemByText(string VisibleText)
       // {
       //      LoginPage login = new LoginPage();
       //      string items = login.DatabaseDropDown.GetAllItems();
       //     string[] ItemList = items.Split(',');
           
       //     for (int i = 1; i < ItemList.Count(); i++)
       //     {
       //         if (VisibleText.Equals(login.DatabaseDropDown.GetSelectedItem().TrimEnd()))
       //             break;
       //         Keyboard.SendKeys("{UP}");
       //     }
       //     for (int q = 0; q < ItemList.Count(); q++)
       //     {
       //         if (VisibleText.Equals(login.DatabaseDropDown.GetSelectedItem().TrimEnd()))
       //             break;
       //         Keyboard.SendKeys("{DOWN}");
       //     }
       // }

       ////private void  ItemByVisibleText(string VisibleText)
       ////{
       ////    LoginPage login = new LoginPage();
          
       ////    string i = login.DatabaseDropDown.GetAllItems();
       ////    for (int Num = 0; Num < 18; Num++)
       //    {

       //    }
       //   // string selected = login.DatabaseDropDown.GetSelectedItem();
       //}

    }
}
