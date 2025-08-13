using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.ViewModels
{
    public class ReadWordsViewModel : ObservableObject
    {
        private string? _greekInGreek;
        public string? GreekInGreek
        {
            get => _greekInGreek;
            set
            {
                if (_greekInGreek != value)
                {
                    _greekInGreek = value;
                    OnPropertyChanged(nameof(GreekInGreek));
                }
            }
        }

        private string? _greekInLatin;
        public string? GreekInLatin
        {
            get => _greekInLatin;
            set
            {
                if (_greekInLatin != value)
                {
                    _greekInLatin = value;
                    OnPropertyChanged(nameof(GreekInLatin));
                }
            }
        }

        private string? _englishTranslation;
        public string? EnglishTranslation
        {
            get => _englishTranslation;
            set
            {
                if (_englishTranslation != value)
                {
                    _englishTranslation = value;
                    OnPropertyChanged(nameof(EnglishTranslation));
                }
            }
        }

        public IAsyncRelayCommand NavigateToMainMenu { get; private set; }

        public ReadWordsViewModel()
        {
            GreekInGreek = "Hello";
            GreekInLatin = "Bello";
            EnglishTranslation = "Mello";

            NavigateToMainMenu = new AsyncRelayCommand(GoToMainMenu);
        }

        private async Task GoToMainMenu()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
