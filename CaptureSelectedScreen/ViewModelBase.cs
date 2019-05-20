using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CaptureSelectedScreen
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected bool IsWorked = false;


        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


        protected UIElement view = null;
        protected RelayCommand<UIElement> DoLoad = null;
        public ICommand OnLoaded
        {
            get
            {
                if (DoLoad == null)
                {
                    DoLoad = new RelayCommand<UIElement>((view) =>
                    {
                        this.view = view;
                    });
                }
                return DoLoad;
            }
            protected set { DoLoad = value as RelayCommand<UIElement>; OnPropertyChanged(); }
        }

        protected RelayCommand<UIElement> DoActivated = null;
        public ICommand OnActivated
        {
            get
            {
                if (DoActivated == null)
                {
                    DoActivated = new RelayCommand<UIElement>((view) =>
                    {

                    });
                }
                return DoActivated;
            }
            protected set { DoActivated = value as RelayCommand<UIElement>; OnPropertyChanged(); }
        }

        protected RelayCommand<UIElement> DoDeactivated = null;
        public ICommand OnDeactivated
        {
            get
            {
                if (DoDeactivated == null)
                {
                    DoDeactivated = new RelayCommand<UIElement>((view) =>
                    {

                    });
                }
                return DoDeactivated;
            }
            protected set { DoDeactivated = value as RelayCommand<UIElement>; OnPropertyChanged(); }
        }

        protected RelayCommand<UIElement> DoUnload = null;
        public ICommand OnUnloaded
        {
            get
            {
                if (DoUnload == null)
                {
                    DoUnload = new RelayCommand<UIElement>((view) =>
                    {

                    });
                }
                return DoUnload;
            }
            protected set { DoUnload = value as RelayCommand<UIElement>; OnPropertyChanged(); }
        }
        protected RelayCommand<UIElement> DoClosed = null;
        public ICommand OnClosed
        {
            get
            {
                if (DoClosed == null)
                {
                    DoClosed = new RelayCommand<UIElement>((view) =>
                    {
                        if (Application.Current.MainWindow != null && view != Application.Current.MainWindow)
                        {
#if false
                            Task.Run(() =>
                                                {
                                                    GC.Collect();
                                                }); 
#endif
                        }
                    });
                }
                return DoClosed;
            }
            protected set { DoClosed = value as RelayCommand<UIElement>; OnPropertyChanged(); }
        }

#if ACS_FX45
        public virtual void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            return;
        }
#endif

        bool IsLoaded = false;
        protected bool GuardLoaded()
        {
            if (IsLoaded == false) { IsLoaded = true; return true; }

            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()) == true) { return false; }

            return false;
        }
        bool IsUnloaded = false;
        protected bool GuardUnLoaded()
        {
            if (IsUnloaded == false) { IsUnloaded = true; return true; }

            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()) == true) { return false; }

            return false;
        }
        bool IsClosing = false;
        protected bool GuardClosing()
        {
            if (IsClosing == false) { IsClosing = true; return true; }
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()) == true) { return false; }
            return false;
        }
        bool IsClosed = false;
        protected bool GuardClosed()
        {
            if (IsClosed == false) { IsClosed = true; return true; }
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()) == true) { return false; }
            return false;
        }


#if DEBUG
        private bool isTest = true;
#else
        private bool isTest = false; 
#endif

        public bool IsTest
        {
            get { return isTest; }
            set
            {
#if false
                isTest = value; 
#endif 
                OnPropertyChanged();
            }
        }


        public Window OwnerWindow { get; set; } = null;
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class NotifyPropertyChangedInvocatorAttribute : System.Attribute
    {
        public NotifyPropertyChangedInvocatorAttribute() { }
        public NotifyPropertyChangedInvocatorAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }

        public string ParameterName { get; private set; }
    }
}
