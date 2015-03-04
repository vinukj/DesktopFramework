

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts.Selenium
{
    public class IFrameSearchInfo
    {
        public String Path { get; set; }
        public String Name { get; set; }
        public IFrameSearchInfo NextFrame { get; set; }
        public IFrameSearchInfo Clone()
        {
            IFrameSearchInfo info = new IFrameSearchInfo();
            info.Name = this.Name;
            info.Path = this.Path;
            info.NextFrame = this.NextFrame;
            return info;
        }

        public string GetFullName()
        {
            IFrameSearchInfo nextFrame = this.Clone();
            string name = "";
            while (nextFrame != null)
            {
                name += nextFrame.Name + " -|";
                nextFrame = nextFrame.NextFrame;
                if (nextFrame != null)
                {
                    nextFrame = nextFrame.Clone();
                }
            }

            return name.Substring(0, name.Length - 3);
        }
    }
}
