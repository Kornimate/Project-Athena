using Athena.ViewModels;
using Microsoft.Maui.Controls;

namespace Athena.Views
{
    public partial class AddWordPage : ContentPage
    {
        public AddWordPage(AddWordViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}