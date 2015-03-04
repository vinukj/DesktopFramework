//----------------------------------------------------------------------- // 


using Microsoft.VisualStudio.TestTools.UITesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Test.Foundation.UI.Contracts;
using Core.Test.Foundation.UI.Contracts.Muia;

namespace Core.Test.Foundation.UI.Desktop
{
    public class TestMouse
    {
        public static void Click(IControl control, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            throw new NotImplementedException("Mouse Click in not implemented");
        }

        public static void ClickConnectorArrow(IMuiaControl Control)
        {
            Rectangle SourceControlRectangle = Control.GetBoundingRectangel();
            Point ConnectorPont = new Point();
            Rectangle TargetControlRectangle = Control.GetBoundingRectangel();
            ConnectorPont.X = (int)(TargetControlRectangle.X + 1 + TargetControlRectangle.Width / 2);
            ConnectorPont.Y = (int)(TargetControlRectangle.Y - 19)-2;
            Mouse.DoubleClick(ConnectorPont);
        }

        public static void DragAndDropLocation(IMuiaControl DraggedControl)
        {
            Rectangle SourceControlRectangle = DraggedControl.GetBoundingRectangel();
            Point StartdraggingPont = new Point();
            StartdraggingPont.X = SourceControlRectangle.X;
            StartdraggingPont.Y = SourceControlRectangle.Y;
            Mouse.Hover(StartdraggingPont);
            Mouse.StartDragging();
            Point StopdraggingPont = new Point();
            StopdraggingPont.X = SourceControlRectangle.X+40;
            StopdraggingPont.Y = SourceControlRectangle.Y+200;

            Mouse.StopDragging(StopdraggingPont);
        }

        public static void DragAndDropLocation(IMuiaControl DraggedControl, int X, int Y)
        {
            Rectangle SourceControlRectangle = DraggedControl.GetBoundingRectangel();
            Point StartdraggingPont = new Point();
            StartdraggingPont.X = SourceControlRectangle.X;
            StartdraggingPont.Y = SourceControlRectangle.Y;
            Mouse.Hover(StartdraggingPont);
            Mouse.StartDragging();
            Point StopdraggingPont = new Point();
            StopdraggingPont.X = SourceControlRectangle.X + X;
            StopdraggingPont.Y = SourceControlRectangle.Y + Y;

            Mouse.StopDragging(StopdraggingPont);
        }
        public static void DargAndDrop(IMuiaControl SourceControl, IMuiaControl TargetControl)
        {
             Rectangle SourceControlRectangle = SourceControl.GetBoundingRectangel();
            //Get the Clickable Point
            Point StartPoint =new Point();
          //  Rectangle SourceControlRectangle = SourceControl.GetBoundingRectangel();
            StartPoint.X = (int)(SourceControlRectangle.X + 1 + SourceControlRectangle.Width / 2);
            StartPoint.Y = (int)(SourceControlRectangle.Y + 34);
            
            Mouse.Hover(StartPoint);
            Mouse.StartDragging();

            Point EndPoint = new Point();
            Rectangle TargetControlRectangle = TargetControl.GetBoundingRectangel();
            EndPoint.X = (int)(TargetControlRectangle.X + 1 + TargetControlRectangle.Width / 2);
            EndPoint.Y = (int)(TargetControlRectangle.Y - 19);
            Mouse.StopDragging(EndPoint);
       }
        public static void DargAndDrop2(IMuiaControl SourceControl, IMuiaControl TargetControl)
        {
            Rectangle SourceControlRectangle = SourceControl.GetBoundingRectangel();
            //Get the Clickable Point
            Point StartPoint = new Point();
            //  Rectangle SourceControlRectangle = SourceControl.GetBoundingRectangel();
            StartPoint.X = (int)(SourceControlRectangle.X + 2 + SourceControlRectangle.Width / 2);
            StartPoint.Y = (int)(SourceControlRectangle.Y + 34);

            Mouse.Hover(StartPoint);
            Mouse.StartDragging();

            Point EndPoint = new Point();
            Rectangle TargetControlRectangle = TargetControl.GetBoundingRectangel();
            EndPoint.X = (int)(TargetControlRectangle.X + 1 + TargetControlRectangle.Width / 2);
            EndPoint.Y = (int)(TargetControlRectangle.Y - 19);
            Mouse.StopDragging(EndPoint);
        }    
   }
}
   

