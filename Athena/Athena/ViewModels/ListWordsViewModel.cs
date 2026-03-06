using Athena.Interfaces;
using Athena.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Athena.ViewModels
{
    public class ListWordsViewModel : ObservableObject
    {
        private List<WordModel> _words = [];
        private Locale? _greekLocale;
        private Locale? _englishLocale;
        private SpeechOptions? _greekSettings;
        private SpeechOptions? _englishSettings;

        private bool _setupDone = false; 

        private CancellationTokenSource? _ctsExit;
        public ObservableCollection<WordModel> Words { get; set; }

        public WordModel SelectedWord
        {

            get => null!;
            set
            {
                _ = WordSelected(value);
            }
        }

        private readonly IWordCacheService _wordCacheService;

        public IRelayCommand<WordModel> RemoveWordCommand { get; }
        public IRelayCommand<WordModel> SelectWordCommand { get; }

        public ListWordsViewModel(IWordCacheService wordCacheService)
        {
            Words = new ObservableCollection<WordModel>();
            _wordCacheService = wordCacheService;

            RemoveWordCommand = new RelayCommand<WordModel>(async (word) => await RemoveWord(word!));
            SelectWordCommand = new RelayCommand<WordModel>((word) => SelectedWord = word! );

            _wordCacheService.DataChanged += WordsChanged;
        }

        private void WordsChanged(object? sender, EventArgs e)
        {
            Words.Clear();

            foreach (var word in _wordCacheService.CachedWords)
            {
                Words.Add(word);
            }
        }

        public async Task LoadAsync()
        {
            if (Words.Any())
                return;

            foreach (var word in _wordCacheService.CachedWords)
            {
                Words.Add(word);
            }
        }

        public void StopReading()
        {
            _ctsExit?.Cancel();
        }

        private async Task WordSelected(WordModel? word)
        {
            _ctsExit = new CancellationTokenSource();

            try
            {
                await ReadWord(word, _ctsExit.Token);
            }
            catch (OperationCanceledException) { }
        }

        private async Task SetUpVoices()
        {
            if(_setupDone)
                return;

            var locales = await TextToSpeech.Default.GetLocalesAsync();

            _greekLocale = locales.FirstOrDefault(e => e.Language == "el")!;
            _englishLocale = locales.LastOrDefault(e => e.Language == "en")!;

            _greekSettings = new SpeechOptions()
            {
                Locale = _greekLocale,
                Pitch = 1.0f,
                Volume = 1.0f
            };

            _englishSettings = new SpeechOptions()
            {
                Locale = _englishLocale,
                Pitch = 1.0f,
                Volume = 1.0f
            };

            _setupDone = true;
        }

        private async Task ReadWord(WordModel? wordModel, CancellationToken token)
        {
            await SetUpVoices();

            if (wordModel == null)
                return;

            var word = Words.FirstOrDefault(x => x.GreekInGreek == wordModel.GreekInGreek)!;

            token.ThrowIfCancellationRequested();

            await TextToSpeech.Default.SpeakAsync(word.GreekInGreek!, _greekSettings, token);
            await TextToSpeech.Default.SpeakAsync(word.EnglishTranslation!, _englishSettings, token);
        }

        private async Task RemoveWord(WordModel word)
        {
            bool confirm = await App.Current!.Windows[0].Page!.DisplayAlertAsync(
                "Confirm Removal",
                $"Are you sure you want to remove the word '{word.GreekInGreek}'?",
                "Yes",
                "No"
            );

            if (confirm)
            {
                _wordCacheService.RemoveWord(word);
            }
        }
    }
}
