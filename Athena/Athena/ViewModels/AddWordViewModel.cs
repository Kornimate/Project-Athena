using Athena.Interfaces;
using Athena.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Athena.ViewModels
{
    public class AddWordViewModel : ObservableObject
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

        public IAsyncRelayCommand SaveCommand { get; }
        public IAsyncRelayCommand CancelCommand { get; }

        private IWordCacheService _wordsService;

        public AddWordViewModel(IWordCacheService wordsService)
        {
            SaveCommand = new AsyncRelayCommand(SaveAsync);
            CancelCommand = new AsyncRelayCommand(CancelAsync);
            _wordsService = wordsService;
        }

        private async Task SaveAsync()
        {
            // Basic validation (ensure at least greek text and english provided)
            if (string.IsNullOrWhiteSpace(GreekInGreek) &&
                string.IsNullOrWhiteSpace(GreekInLatin) &&
                string.IsNullOrWhiteSpace(EnglishTranslation))
            {
                // Nothing to save
                return;
            }

            var newWord = new WordModel
            {
                GreekInGreek = GreekInGreek,
                GreekInLatin = GreekInLatin,
                EnglishTranslation = EnglishTranslation
            };

            await _wordsService.UpdateWords((words, word) => words.Add(word), newWord);

            // Navigate back to previous page
            await Shell.Current.GoToAsync("..");
        }

        private async Task CancelAsync()
        {
            // Just go back
            await Shell.Current.GoToAsync("..");
        }
    }
}
