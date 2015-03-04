//----------------------------------------------------------------------- // 

//<copyright file="Config.cs" company="Aptean"> // 
//Copyright (c) Aptean. All rights reserved. // 
//<author> Vinay Jagtap K </author> // <date>10/11/2014 11:02:08 AM</date> // </copyright> 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Aptean.Test.Foundation.UI.Web.JQuery;
using Aptean.Test.Foundation.UI.Web.Selenium;
using Aptean.Test.Foundation.UI.Contracts;
using Aptean.Test.Foundation.UI.Contracts.Selenium;

namespace Aptean.Test.Foundation.UI
{
    public class HtmlControlFactory
    {
        Dictionary<String, ControlSearchInfo> controls;
        Dictionary<String, IFrameSearchInfo> frames;
        public String LastError { get; set; }

        public HtmlControlFactory()
        {
            controls = new Dictionary<String, ControlSearchInfo>();
            frames = new Dictionary<String, IFrameSearchInfo>();
        }

        public bool Init(string xml)
        {
            try
            {
                XmlDocument dcoument = new XmlDocument();
                dcoument.LoadXml(xml);
                XmlElement docEle = dcoument.DocumentElement;

                XmlNodeList controls = docEle.GetElementsByTagName("Control");

                for (int i = 0; i < controls.Count; i++)
                {
                    ControlSearchInfo searchInfo = new ControlSearchInfo();
                    XmlElement controlEl = (XmlElement)controls[i];
                    searchInfo.Name = controlEl.GetAttribute("name");

                    if (GetAttribute(controlEl, "pathType").Trim().Length > 0)
                    {
                        searchInfo.PathType = (ControlPathType)Enum.Parse(typeof(ControlPathType), controlEl.GetAttribute("pathType"));
                    }

                    XmlNode pathNode = controlEl.ChildNodes[0];

                    searchInfo.Path = pathNode.InnerText;

                    if (!this.controls.ContainsKey(searchInfo.Name))
                    {
                        this.controls.Add(searchInfo.Name, searchInfo);
                    }
                }

                controls = docEle.GetElementsByTagName("IFrame");
                for (int i = 0; i < controls.Count; i++)
                {
                    XmlElement controlEl = (XmlElement)controls[i];
                    IFrameSearchInfo frame = new IFrameSearchInfo();
                    frame.Name = controlEl.GetAttribute("name");
                    frame.Path = controlEl.GetAttribute("path");
                    if(controlEl.ChildNodes.Count >0){
                        XmlElement childFrame = (XmlElement)controlEl.ChildNodes[0];
                        if (childFrame != null)
                        {
                            AppendChildFrame(frame, childFrame);
                        }
                    }
                    if (!frames.ContainsKey(frame.Name))
                    {
                        frames.Add(frame.Name, frame);
                    }
                }
                return true;
            }
            catch (Exception exe)
            {
                LastError = exe.Message;
            }

            return false;
        }

        private void AppendChildFrame(IFrameSearchInfo frame, XmlElement controlEl)
        {
            if (controlEl == null)
            {
                return;
            }
            IFrameSearchInfo nextFrame = new IFrameSearchInfo();
            nextFrame.Name = controlEl.GetAttribute("name");
            nextFrame.Path = controlEl.GetAttribute("path");
            frame.NextFrame = nextFrame;
            XmlElement childFrame= null;
            if (controlEl.ChildNodes.Count > 0) { 
                childFrame = (XmlElement)controlEl.ChildNodes[0];
            }
            AppendChildFrame(nextFrame, childFrame);
        }

        private String GetAttribute(XmlElement controlEl, String attr)
        {
            String value = "";
            try
            {
                value = controlEl.GetAttribute(attr);
            }
            catch (Exception exe)
            {

            }
            return value;
        }

        public ControlSearchInfo GetControl(String name)
        {
            if (!controls.ContainsKey(name))
            {
                throw new Exception(string.Format("Could not find control='{0}' in XML", name));
            }
            return controls[name];
        }

        public IFrameSearchInfo GetFrame(String name)
        {
            if (!frames.ContainsKey(name))
            {
                throw new Exception(string.Format("Could not find Frame='{0}' in XML", name));
            }
            return frames[name];
        }

        public T CreateJQ<T>(String controlName) where T : JQControl
        {
            JQControl control = (JQControl)Activator.CreateInstance<T>();
            ControlSearchInfo info = GetControl(controlName);
            control.ControlSearchInfo = info;
            return control as T;
        }

        public T CreateSelenium<T>(String controlName) where T : SeleniumControl
        {
            SeleniumControl control = (SeleniumControl)Activator.CreateInstance<T>();
            ControlSearchInfo info = GetControl(controlName);
            control.ControlSearchInfo = info;
            return control as T;
        }

        public ISeleniumIFrame CreateFrame(String frameName)
        {
            ISeleniumIFrame control = new SeleniumIFrame();
            IFrameSearchInfo info = GetFrame(frameName);
            control.FrameSearchInfo = info;
            return control;
        }
    }
}
