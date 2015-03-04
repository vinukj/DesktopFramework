

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
//using Product.Desktop.Test.UI.Framework.UIMap;
using Core.Test.Foundation.UI.Contracts;
using Core.Test.Foundation.UI.Desktop.Muia;
using Core.Test.Foundation.UI.Desktop.Pages;
using Microsoft.VisualStudio.TestTools.UITesting;
using Core.Test.Foundation.Logger;
using Core.Test.Foundation.UI.Desktop;



namespace Application.Desktop.Test.UI.Framework.Utilities
{
    public class Utils : WpfBasePage
    {

        public static IControl getListItemByName(string paneAutomationid, string name)
        {
            AutomationElement root = AutomationElement.RootElement;
            PropertyCondition PropApplicaionRoot = new PropertyCondition(AutomationElement.AutomationIdProperty, "MainForm");
            AutomationElement ApplicationRoot = root.FindFirst(TreeScope.Subtree, PropApplicaionRoot);
            PropertyCondition PropSideMenuRoot = new PropertyCondition(AutomationElement.AutomationIdProperty, paneAutomationid);
            AutomationElement SideMenuRoot = ApplicationRoot.FindFirst(TreeScope.Subtree, PropSideMenuRoot);
            // List Item
            PropertyCondition PropSideMenuRootList = new PropertyCondition(AutomationElement.LocalizedControlTypeProperty, "list item");
            AutomationElement SideMenuRootList = ApplicationRoot.FindFirst(TreeScope.Subtree, PropSideMenuRootList);

            PropertyCondition propEntitiesAndFields = new PropertyCondition(AutomationElement.NameProperty, name);
            AutomationElement Boolean_136736ListItem = SideMenuRoot.FindFirst(TreeScope.Subtree, propEntitiesAndFields);
            // Converting
            MuiaControl muiabutton = new MuiaControl(Boolean_136736ListItem, "Boolean_136736 List Item");
            return muiabutton;
           
        }

        public static IControl getThirdLevelChild(string fistchild, string secondchild, string thirdchild)
        {
            AutomationElement pomRoot = AutomationElement.RootElement;
            PropertyCondition pcMainWindow = new PropertyCondition(AutomationElement.AutomationIdProperty, "MainForm");
            AutomationElement mainWindow = pomRoot.FindFirst(TreeScope.Subtree, pcMainWindow);
            // tree veiw
            PropertyCondition pcSecurityTree = new PropertyCondition(AutomationElement.AutomationIdProperty, fistchild);
            AutomationElement securityProfTree = mainWindow.FindFirst(TreeScope.Subtree, pcSecurityTree);


            PropertyCondition pcSecurityProf = new PropertyCondition(AutomationElement.NameProperty, secondchild);
            AutomationElement securityProf = securityProfTree.FindFirst(TreeScope.Subtree, pcSecurityProf);
            PropertyCondition pcSueprUsers = new PropertyCondition(AutomationElement.NameProperty, thirdchild);
            AutomationElement btn = securityProf.FindFirst(TreeScope.Subtree, pcSueprUsers);
            // COnverting
            MuiaControl btn1 = new MuiaControl(btn, "btn");
            return btn1;
        }


        public static IControl getSecondLevelChild(string fistchild, string secondchild)
        {
            AutomationElement pomRoot = AutomationElement.RootElement;
            PropertyCondition pcMainWindow = new PropertyCondition(AutomationElement.AutomationIdProperty, "MainForm");
            AutomationElement mainWindow = pomRoot.FindFirst(TreeScope.Subtree, pcMainWindow);
            // tree veiw
            PropertyCondition pcSecurityTree = new PropertyCondition(AutomationElement.AutomationIdProperty, fistchild);
            AutomationElement securityProfTree = mainWindow.FindFirst(TreeScope.Subtree, pcSecurityTree);


            PropertyCondition pcSecurityProf = new PropertyCondition(AutomationElement.NameProperty, secondchild);
            AutomationElement securityProf = securityProfTree.FindFirst(TreeScope.Subtree, pcSecurityProf);
           
            // COnverting
            MuiaControl btn1 = new MuiaControl(securityProf, "btn");
            return btn1;
        }

        public static ICheckBox getCheckBoxByAutomationId(string automationId)
        {
            AutomationElement root = AutomationElement.RootElement;
            PropertyCondition PropApplicaionRoot = new PropertyCondition(AutomationElement.AutomationIdProperty, "MainForm");
            AutomationElement ApplicationRoot = root.FindFirst(TreeScope.Subtree, PropApplicaionRoot);
            //PropertyCondition propMainMenu = new PropertyCondition(AutomationElement.AutomationIdProperty, "FieldEditForm");
            //AutomationElement DataBaseOptions = ApplicationRoot.FindFirst(TreeScope.Subtree, propMainMenu);
            PropertyCondition propEntitiesAndFields = new PropertyCondition(AutomationElement.AutomationIdProperty, automationId);
            AutomationElement MandatoryCheckBox = ApplicationRoot.FindFirst(TreeScope.Subtree, propEntitiesAndFields);
            // COnverting
            MuiaCheckBox btn1 = new MuiaCheckBox(MandatoryCheckBox, "btn");
            return btn1;
        }

        /// <summary>
        /// Takes the given number of parent and tries to identify the required field on the screen
        /// </summary>
        /// <param name="list">list of parents in the descending hierarchy</param>
        /// <returns>Required Control</returns>
        public static IControl getRqdCtrl(params string[] list)
        {
            AutomationElement root = AutomationElement.RootElement;
            PropertyCondition pcParent = null;
            AutomationElement parent = null;

            for (int i = 0; i < list.Length; i++)
            {
                if (i == 0)
                {
                    pcParent = new PropertyCondition(AutomationElement.AutomationIdProperty, list[i]);
                    parent = root.FindFirst(TreeScope.Subtree, pcParent);
                }
                else
                {
                    pcParent = new PropertyCondition(AutomationElement.AutomationIdProperty, list[i]);
                    parent = parent.FindFirst(TreeScope.Subtree, pcParent);
                }
            }

            MuiaControl rqdCtrl = new MuiaControl(parent, "Required Control");
            return rqdCtrl;
        }

        /// <summary>
        /// Determines whether [is alert dialog exist] [the specified retry].
        /// </summary>
        /// <param name="retry">The retry.</param>
        /// <returns><c>true</c> if [is alert dialog exist] [the specified retry]; otherwise, <c>false</c>.</returns>
        public static bool IsAlertDialogExist(ControlRetry retry = ControlRetry.Excellent)
        {
            var handle = User32.GetForegroundWindow();

            var window = UITestControlFactory.FromWindowHandle(handle);

            var control = DesktopControlFactory.CreateMuia<MuiaControl>(window, MuiaElementProperty.AutomationId, "positiveButton", "Foreground dialog");

            return control.IsVisible(retry);
        }

        /// <summary>
        /// Dispatches the dialog.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        public static void DispatchDialog(MuiaElementProperty property, string value)
        {
            var handle = User32.GetForegroundWindow();

            var window = UITestControlFactory.FromWindowHandle(handle);

            var control = DesktopControlFactory.CreateMuia<MuiaControl>(window, property, value, "Foreground dialog");

            if (control.IsVisible(ControlRetry.Super))
            {
                control.Click();
            }
        }

        // Convert the Automation element in the UITESTCONTORL
        public static UITestControl ConvertElementToControl(AutomationElement automationElement)
        {
            Playback.Wait(500);
            // Changing the Element to Control
            UITestControl Ctrl = UITestControlFactory.FromNativeElement(automationElement, "UIA");
            return Ctrl;
        }

        //function to wait Until the Screen next Screen or Control is Not getting Appear On the Screen
        public static void WaitforNextControltoAppear(string ControlAutomationId = "", string ControlName = "")
        {
            AutomationElement root = AutomationElement.RootElement;
            //Getting the Main Window
            PropertyCondition windowCondition = new PropertyCondition(AutomationElement.AutomationIdProperty, "Default");
            AutomationElement window = root.FindFirst(TreeScope.Subtree, windowCondition);
            if (ControlAutomationId != "")
            {
                AutomationElement element = window.FindFirst(TreeScope.Subtree | TreeScope.Children, new PropertyCondition(AutomationElement.AutomationIdProperty, ControlAutomationId));


                while (element == null)
                {
                    Playback.Wait(500);
                    element = window.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.AutomationIdProperty, ControlAutomationId));
                    //ctrl = UIAClassLibrary.ConvertElementToControl(element);
                }
            }
            if (ControlName != "")
            {
                AutomationElement element = window.FindFirst(TreeScope.Subtree | TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, ControlName));

                //ctrl.WaitForControlExist();
                while (element == null)
                {
                    Playback.Wait(500);
                    element = window.FindFirst(TreeScope.Subtree | TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, ControlName));
                    //ctrl = UIAClassLibrary.ConvertElementToControl(element);
                }

            }
        }

        // Get the Item by AutomationId 
        public static AutomationElement GetElementByAutomationId(string automationId)
        {
            AutomationElement root = AutomationElement.RootElement;
            //Getting the Main Window
            PropertyCondition windowCondition = new PropertyCondition(AutomationElement.AutomationIdProperty, "Default");
            AutomationElement window = root.FindFirst(TreeScope.Subtree, windowCondition);
            // getting the Parent element
            Playback.Wait(2000);
            AutomationElement eleRoot = window.FindFirst(TreeScope.Subtree | TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, "formView"));
            // Now getting the Required element 
            AutomationElement eleAutomationElement = eleRoot.FindFirst(TreeScope.Subtree | TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, automationId));
            // Returing the Element
            return eleAutomationElement;
        }

        // Get the Item by Name Property
        public static AutomationElement GetElementByName(string NameProp)
        {
            AutomationElement root = AutomationElement.RootElement;
            //Getting the Main Window
            PropertyCondition windowCondition = new PropertyCondition(AutomationElement.AutomationIdProperty, "Default");
            AutomationElement window = root.FindFirst(TreeScope.Subtree, windowCondition);
            // getting the Parent element
            Playback.Wait(2000);
            AutomationElement eleRoot = window.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.AutomationIdProperty, "formView"));
            // Now getting the Required element 
            AutomationElement eleAutomationElement = eleRoot.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.NameProperty, NameProp));
            // Returing the Element
            return eleAutomationElement;

        }
        //To get the message from the top bar
        public static bool MessageOnTopBar(string Message)
        {
            AutomationElement mainWindow = AutomationElement.RootElement;
            PropertyCondition MsgProp = new PropertyCondition(AutomationElement.NameProperty, Message);
            AutomationElement Parent = mainWindow.FindFirst(TreeScope.Subtree, MsgProp);
            UITestControl ctrl = new UITestControl();
            ctrl = UITestControlFactory.FromNativeElement(Parent, "UIA");
            string value = ctrl.GetProperty("Name").ToString();
            if (value.Equals(null))
                return false;
            return true;
        }
        // Get Value from Any TextBox/PartInput_field

        public static string GetValueFromPartTextBox(string PaneId, string CustomId, string EditBoxId)
        {
            AutomationElement mainWindow = AutomationElement.RootElement;
            PropertyCondition Paneprop = new PropertyCondition(AutomationElement.AutomationIdProperty, PaneId);
            AutomationElement elePane = mainWindow.FindFirst(TreeScope.Subtree, Paneprop);
            // 
            PropertyCondition Customprop = new PropertyCondition(AutomationElement.AutomationIdProperty, CustomId);
            AutomationElement elecustom = elePane.FindFirst(TreeScope.Subtree, Customprop);
            //
            PropertyCondition Txtboxprop = new PropertyCondition(AutomationElement.AutomationIdProperty, EditBoxId);
            AutomationElement eletxtBox = elecustom.FindFirst(TreeScope.Subtree, Txtboxprop);
            //
            //Convert the elemnet to control
            UITestControl Ctrl = new UITestControl();
            Ctrl = UITestControlFactory.FromNativeElement(eletxtBox, "UIA");
            // get The Value Of Text Box
            string TextValue = Ctrl.GetProperty("Text").ToString();
            // Return the Value
            return TextValue;
        }

        // function to generate the the Random String
        public static string GenerateRandomString(int numberOfchar)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, numberOfchar)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        public override void Init()
        {
            throw new NotImplementedException();
        }
    }

}
