using Athena.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public IAsyncRelayCommand NavigateToReadWords { get; private set; }

        public IAsyncRelayCommand NavigateToFlashCards { get; private set; }

        public MainViewModel()
        {
            NavigateToReadWords = new AsyncRelayCommand(GoToReadWordsPage);
            NavigateToFlashCards = new AsyncRelayCommand(GoToFlashCardsPage);
        }

        private async Task GoToFlashCardsPage()
        {
            await Shell.Current.GoToAsync(nameof(FlashCardsPage));
        }

        private async Task GoToReadWordsPage()
        {
            await Shell.Current.GoToAsync(nameof(ReadWordsPage));
        }
    }
}
