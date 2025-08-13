using Athena.ViewModels;

namespace Athena
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel _viewModel;
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            
            _viewModel = viewModel;

            BindingContext = _viewModel;
        }
    }

}
