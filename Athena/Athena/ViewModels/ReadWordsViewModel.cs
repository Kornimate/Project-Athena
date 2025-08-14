using Athena.Extensions;
using Athena.Interfaces;
using Athena.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;
using static System.Net.Mime.MediaTypeNames;

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
        public IAsyncRelayCommand StartSession { get; private set; }

        private List<WordModel> _words = [];
        private Locale _greekLocale;
        private Locale _englishLocale;
        private SpeechOptions _greekSettings;
        private SpeechOptions _englishSettings;

        private IDataLoader _dataLoader;
        private CancellationTokenSource _ctsExit;

        public ReadWordsViewModel(IDataLoader dataLoader)
        {
            NavigateToMainMenu = new AsyncRelayCommand(GoToMainMenu);
            StartSession = new AsyncRelayCommand(StartReading);

            _dataLoader = dataLoader;
        }

        private async Task StartReading()
        {
            _ctsExit = new CancellationTokenSource();

            await SetUpWords();

            await SetUpVoices();

            try
            {
                await ReadWords(_ctsExit.Token);
            }
            catch (OperationCanceledException) { }
        }

        private async Task SetUpWords()
        {
            if (_words.Count == 0)
                _words = await _dataLoader.LoadDataFromJson("example.json");

            _words.Shuffle(); //shuffles in place
        }

        private async Task SetUpVoices()
        {
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
        }

        private async Task ReadWords(CancellationToken token)
        {
            foreach(var word in _words)
            {
                token.ThrowIfCancellationRequested();

                GreekInGreek = word.GreekInGreek;
                GreekInLatin = word.GreekInLatin;
                EnglishTranslation = word.EnglishTranslation;

                await TextToSpeech.Default.SpeakAsync(word.GreekInGreek!, _greekSettings, token);
                await TextToSpeech.Default.SpeakAsync(word.EnglishTranslation!, _englishSettings, token);

                await Task.Delay(2000, token);
            }
        }

        private async Task GoToMainMenu()
        {
            _ctsExit?.Cancel();
            await Shell.Current.GoToAsync("..");
        }

        public void ClearText()
        {
            GreekInGreek = "";
            GreekInLatin = "";
            EnglishTranslation = "";
        }
    }
}
