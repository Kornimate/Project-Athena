using Athena.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;

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

        private readonly IDispatcherTimer _timer;
        private int counter = 0;
        private List<WordModel> _words = [];

        public ReadWordsViewModel()
        {
            NavigateToMainMenu = new AsyncRelayCommand(GoToMainMenu);
            StartSession = new AsyncRelayCommand(StartReading);

            _timer = Dispatcher.GetForCurrentThread()!.CreateTimer();
            _timer.Interval = TimeSpan.FromSeconds(5);
            _timer.Tick += (s, e) =>
            {
                if(counter >= 150)
                {
                    _timer.Stop();
                    return;
                }

                GreekInGreek = _words![counter].GreekInGreek;
                GreekInLatin = _words![counter].GreekInLatin;
                EnglishTranslation = _words![counter].EnglishTranslation;

                counter++;
            };
        }

        private async Task StartReading()
        {
            if (_words.Count > 0)
                return;

            using var stream = await FileSystem.OpenAppPackageFileAsync("example.json");
            using var reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            _words = JsonSerializer.Deserialize<List<WordModel>>(json, options)!;

            _timer.Start();
        }

        private async Task GoToMainMenu()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
