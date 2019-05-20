using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CaptureSelectedScreen
{
    internal class CaptureSelectVM : ViewModelBase
    {
        CaptureSelect captureWnd = null;
        public CaptureSelectVM()
        {
            DoLoad = new RelayCommand<System.Windows.UIElement>(_ =>
            {
                if (this.view != null) { return; }
                this.view = _;
                captureWnd = _ as CaptureSelect;
            });

            DoUnload = new RelayCommand<System.Windows.UIElement>(_ =>
            {
                if (this.view == null) { return; }
                try
                {

                }
                finally
                {
                    this.view = null;
                    captureWnd = null;
                }
            });
        }

        #region Properties
        private double mouseX = 0.0;
        public double MouseX
        {
            get { return mouseX; }
            set { mouseX = value; OnPropertyChanged(); }
        }

        private double mouseY = 0.0;
        public double MouseY
        {
            get { return mouseY; }
            set { mouseY = value; OnPropertyChanged(); }
        }

        private double mouseX2 = 0.0;
        public double MouseX2
        {
            get { return mouseX2; }
            set { mouseX2 = value; OnPropertyChanged(); }
        }

        private double mouseY2 = 0.0;
        public double MouseY2
        {
            get { return mouseY2; }
            set { mouseY2 = value; OnPropertyChanged(); }
        }

        private double captureTop = 0.0;
        public double CaptureTop
        {
            get { return captureTop; }
            set { captureTop = value; OnPropertyChanged(); }
        }

        private double captureLeft = 0.0;
        public double CaptureLeft
        {
            get { return captureLeft; }
            set { captureLeft = value; OnPropertyChanged(); }
        }

        private double captureWidth = 0.0;
        public double CaptureWidth
        {
            get { return captureWidth; }
            set { captureWidth = value; OnPropertyChanged(); }
        }

        private double captureHeight = 0.0;
        public double CaptureHeight
        {
            get { return captureHeight; }
            set { captureHeight = value; OnPropertyChanged(); }
        }

        private bool captured = false;
        public bool Captured
        {
            get { return captured; }
            set { captured = value; OnPropertyChanged(); }
        }

        private bool capture = false;
        public bool Capture
        {
            get { return capture; }
            set { capture = value; OnPropertyChanged(); }
        }

        private Rect captureRect;
        public Rect CaptureRect
        {
            get { return captureRect; }
            set { captureRect = value; }
        }
        #endregion

        #region Command
        private RelayCommand DoCapture;
        public ICommand OnCapture
        {
            get
            {
                if (DoCapture == null)
                {
                    DoCapture = new RelayCommand(() =>
                    {
                        Capture = true;
                        captureWnd?.Close();
                    });
                }
                return DoCapture;
            }
            set { DoCapture = value as RelayCommand; OnPropertyChanged(); }
        }

        private RelayCommand DoCaptureCancel;
        public ICommand OnCaptureCancel
        {
            get
            {
                if (DoCaptureCancel == null)
                {
                    DoCaptureCancel = new RelayCommand(() =>
                    {
                        Capture = false;
                        captureWnd?.Close();
                    });
                }
                return DoCaptureCancel;
            }
            set { DoCaptureCancel = value as RelayCommand; OnPropertyChanged(); }
        }
        #endregion
    }
}
