using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CaptureSelectedScreen
{
    /// <summary>
    /// CaptureSelect.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CaptureSelect : Window
    {
        public CaptureSelect()
        {
            InitializeComponent();
        }

        #region Event Handlers
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            var dc = this.DataContext as CaptureSelectVM;
            if (dc != null)
            {
                if (!dc.Captured && e.LeftButton == MouseButtonState.Pressed)
                {
                    dc.Captured = true;
                }

                if (dc.Captured)
                {
                    dc.MouseX2 = e.GetPosition(this).X;
                    dc.MouseY2 = e.GetPosition(this).Y;

                    dc.CaptureLeft = dc.MouseX <= dc.MouseX2 ? dc.MouseX : dc.MouseX2;
                    dc.CaptureTop = dc.MouseY <= dc.MouseY2 ? dc.MouseY : dc.MouseY2;

                    dc.CaptureWidth = Math.Abs(dc.MouseX - dc.MouseX2);
                    dc.CaptureHeight = Math.Abs(dc.MouseY - dc.MouseY2);
                }
                else
                {
                    dc.MouseX = e.GetPosition(this).X;
                    dc.MouseY = e.GetPosition(this).Y;
                    dc.MouseX2 = e.GetPosition(this).X;
                    dc.MouseY2 = e.GetPosition(this).Y;
                }
            }
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dc = this.DataContext as CaptureSelectVM;
            if (dc != null)
            {
                dc.Captured = true;
                dc.MouseX = e.GetPosition(this).X;
                dc.MouseY = e.GetPosition(this).Y;
                dc.MouseX2 = e.GetPosition(this).X;
                dc.MouseY2 = e.GetPosition(this).Y;
                dc.CaptureLeft = dc.MouseX <= dc.MouseX2 ? dc.MouseX : dc.MouseX2;
                dc.CaptureTop = dc.MouseY <= dc.MouseY2 ? dc.MouseY : dc.MouseY2;
            }
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dc = this.DataContext as CaptureSelectVM;
            if (dc != null)
            {
                dc.CaptureRect = new Rect(dc.CaptureLeft, dc.CaptureTop, dc.CaptureWidth, dc.CaptureHeight);
                dc.MouseX = 0.0;
                dc.MouseY = 0.0;
                dc.MouseX2 = 0.0;
                dc.MouseY2 = 0.0;
                dc.Captured = false;
                dc.OnCapture.Execute(null);
            }
        }

        private void OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dc = this.DataContext as CaptureSelectVM;
            if (dc != null)
            {
                dc.OnCaptureCancel.Execute(null);
            }
        }
        #endregion
    }
}
