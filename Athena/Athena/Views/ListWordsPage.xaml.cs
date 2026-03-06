using Athena.ViewModels;
using Athena.Models;

namespace Athena.Views;

public partial class ListWordsPage : ContentPage
{
    private ListWordsViewModel _viewModel;

    public ListWordsPage(ListWordsViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _ = _viewModel.LoadAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _viewModel.StopReading();
    }
}
