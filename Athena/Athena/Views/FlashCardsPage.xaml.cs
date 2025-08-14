using Athena.ViewModels;

namespace Athena.Views;

public partial class FlashCardsPage : ContentPage
{
	private FlashCardsViewModel _viweModel;
	public FlashCardsPage(FlashCardsViewModel viewModel)
	{
		InitializeComponent();

		_viweModel = viewModel;

        BindingContext = _viweModel;
    }
}