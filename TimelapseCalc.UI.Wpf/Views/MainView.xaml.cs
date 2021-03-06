using Cirrious.MvvmCross.Wpf.Views;
using TimelapseCalc.Core.ViewModels;

namespace TimelapseCalc.UI.Wpf.Views
{
    public partial class MainView : MvxWpfView
    {
        public new MainViewModel ViewModel
        {
            get { return (MainViewModel) base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public MainView()
        {
            InitializeComponent();
        }
    }
}
