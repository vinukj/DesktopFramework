using Microsoft.VisualStudio.TestTools.UITesting;

namespace Ross.UITest.Framework.TCL
{
    public class Functions
    {
        public string GetMessage()
        {
        //    AutomationElement root = AutomationElement.RootElement;
        //    PropertyCondition windowCondition = new PropertyCondition(AutomationElement.AutomationIdProperty, "Default");
        //    AutomationElement window = root.FindFirst(TreeScope.Subtree, windowCondition);
        //    PropertyCondition modifySupCondition = new PropertyCondition(AutomationElement.AutomationIdProperty, "DockSite");
        //    AutomationElement modifySup = window.FindFirst(TreeScope.Subtree, modifySupCondition);

        //    PropertyCondition messagetextCondition = new PropertyCondition(AutomationElement.NameProperty, "MessageNumberIndicator");
        //    AutomationElement messagetext = window.FindFirst(TreeScope.Subtree, messagetextCondition);

        //    UITestControl sCtrl = UITestControlFactory.FromNativeElement(messagetext, "UIA");
        //    string value = sCtrl.GetProperty("Name").ToString();
        //    return value;
            UITestControl Control = new UITestControl();
            Control.SearchProperties.Add("AutomationId", "Default");
            UITestControl lblObj = new UITestControl(Control);
            lblObj.SearchProperties.Add("Name", "%P_02497", PropertyExpressionOperator.Contains);
            //lblObj.SearchProperties;
            string Name = lblObj.GetProperty("Name").ToString();
            return Name;
        }
    }
}
