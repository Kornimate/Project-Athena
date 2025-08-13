using Athena.ViewModels;

namespace Athena.Views;

public partial class ReadWordsPage : ContentPage
{
	private ReadWordsViewModel _viewModel;

	public ReadWordsPage(ReadWordsViewModel viewModel)
	{
		InitializeComponent();

		_viewModel = viewModel;

		BindingContext = _viewModel;
	}
}