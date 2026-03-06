using Athena.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace Athena.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public IAsyncRelayCommand NavigateToReadWords { get; private set; }

        public IAsyncRelayCommand NavigateToFlashCards { get; private set; }

        public IAsyncRelayCommand NavigateToAddWord { get; private set; }

        public IAsyncRelayCommand NavigateToListWords { get; private set; }

        public MainViewModel()
        {
            NavigateToReadWords = new AsyncRelayCommand(GoToReadWordsPage);
            NavigateToFlashCards = new AsyncRelayCommand(GoToFlashCardsPage);
            NavigateToAddWord = new AsyncRelayCommand(GoToAddWordPage);
            NavigateToListWords = new AsyncRelayCommand(GoToListWordsPage);
        }

        private async Task GoToListWordsPage()
        {
            await Shell.Current.GoToAsync(nameof(ListWordsPage));
        }

        private async Task GoToFlashCardsPage()
        {
            await Shell.Current.GoToAsync(nameof(FlashCardsPage));
        }

        private async Task GoToReadWordsPage()
        {
            await Shell.Current.GoToAsync(nameof(ReadWordsPage));
        }

        private async Task GoToAddWordPage()
        {
            await Shell.Current.GoToAsync(nameof(AddWordPage));
        }
    }
}
