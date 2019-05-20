using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Input;

namespace CaptureSelectedScreen
{
    public class MainWindowVM : ViewModelBase
    {
        #region Constants
        private readonly string SavePath = AppDomain.CurrentDomain.BaseDirectory;
        #endregion

        #region Properties
        public string Watermark { get; private set; } = "This is Water Mark";
        #endregion

        #region Command
        private RelayCommand DoCapture = null;
        public ICommand OnCapture
        {
            get
            {
                if (DoCapture == null)
                {
                    DoCapture = new RelayCommand(() =>
                    {
                        CaptureScreen();
                    });
                }
                return DoCapture;
            }

            set { DoCapture = value as RelayCommand; OnPropertyChanged(); }
        }
        #endregion

        #region Method
        public void CaptureScreen()
        {
            var cwnd = new CaptureSelect
            {
                Left = SystemParameters.VirtualScreenLeft,
                Top = SystemParameters.VirtualScreenTop,
                Width = SystemParameters.VirtualScreenWidth,
                Height = SystemParameters.VirtualScreenHeight,
            };
            cwnd.Closed += (s, e) =>
            {
                var dc = cwnd.DataContext as CaptureSelectVM;
                if (dc != null && dc.Capture == true)
                {
                    Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, ((Action)(() =>
                    {
                        if (dc.CaptureRect == null || dc.CaptureRect.Width <= 0 || dc.CaptureRect.Height <= 0) { return; }
                        string path = string.Empty;
                        try
                        {
                            using (var bmp = new Bitmap((int)dc.CaptureRect.Width, (int)dc.CaptureRect.Height))
                            {
                                int screenWidth = Convert.ToInt32(SystemParameters.VirtualScreenWidth);
                                int screenHeight = Convert.ToInt32(SystemParameters.VirtualScreenHeight);
                                int screenLeft = Convert.ToInt32(SystemParameters.VirtualScreenLeft);
                                int screenTop = Convert.ToInt32(SystemParameters.VirtualScreenTop);
                                using (var g = Graphics.FromImage(bmp))
                                {
                                    g.CopyFromScreen(screenLeft + (int)dc.CaptureRect.Left, screenTop + (int)dc.CaptureRect.Top, 0, 0, bmp.Size);
                                    Font font = new Font("Malgun Gothic", 10.0f, System.Drawing.FontStyle.Bold);
                                    SizeF size = g.MeasureString(Watermark, font);
                                    int max = Math.Max(size.ToSize().Width, size.ToSize().Height);

                                    int textwidth = size.ToSize().Width;
                                    int textheight = size.ToSize().Height;

                                    int y_offset = (int)(textwidth * Math.Sin(45 * Math.PI / 180.0));

                                    bool white = false;
                                    // Drawing Watermark 
                                    if (y_offset >= 0)
                                    {
                                        for (int x = 0; x < bmp.Width; x += textwidth)
                                        {
                                            for (int y = 0; y < bmp.Height; y += textwidth)
                                            {
                                                //move to this position
                                                g.TranslateTransform(x, y);

                                                //draw text rotated around its center
                                                g.TranslateTransform(textwidth, textheight);
                                                g.RotateTransform(-45);
                                                g.TranslateTransform(-textwidth, -textheight);
                                                if (white)
                                                {
                                                    g.DrawString(Watermark, font, new SolidBrush(System.Drawing.Color.FromArgb(0x22, 0xFF, 0xFF, 0xFF)), 0, 0);
                                                    white = false;
                                                }
                                                else
                                                {
                                                    g.DrawString(Watermark, font, new SolidBrush(System.Drawing.Color.FromArgb(0x22, 0x00, 0x00, 0x00)), 0, 0);
                                                    white = true;
                                                }
                                                //reset
                                                g.ResetTransform();
                                            }
                                            white = !white;
                                        }
                                    }
                                }
                                path = System.IO.Path.Combine(SavePath, "CaptureScreen_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg");
                                bmp.Save(path, ImageFormat.Jpeg);
                            }
                        }
                        catch (Exception /*ex*/)
                        {
                            //TODO: log
                            //Message 캡쳐하지 못했습니다.
                        }

                        if (string.IsNullOrEmpty(path)) { return; }

                    })));

                }
            };
            cwnd.Show();
        }
        #endregion
    }
}
