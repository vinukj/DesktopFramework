

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Core.Test.Foundation.UI.Contracts.Wpf
{
    public interface IWpfExpander : IControl,IExpander
    {
        /// <summary>
        /// Expand the control
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        void Expand(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);

        /// <summary>
        /// Expand the control
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        void Collapse(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);

        /// <summary>
        /// Gets the value which indicates whether the expander control is expanded or not
        /// </summary>
        bool IsExpanded { get; }
    }
}
