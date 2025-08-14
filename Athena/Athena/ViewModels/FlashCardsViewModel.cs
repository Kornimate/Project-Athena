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
using System.Threading.Tasks;

namespace Athena.ViewModels
{
    public class FlashCardsViewModel : ObservableObject
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

        public IAsyncRelayCommand LeftTapCommand { get; private set; }
        public IAsyncRelayCommand RightTapCommand { get; private set; }

        private WordsStateMachine _stateMachine;
        private List<WordModel> _words = [];
        private IDataLoader _dataLoader;
        private int counter = 0;

        public FlashCardsViewModel(IDataLoader dataLoader)
        {
            LeftTapCommand = new AsyncRelayCommand(GoBack);
            RightTapCommand = new AsyncRelayCommand(GoFurther);

            _stateMachine = new();

            _dataLoader = dataLoader;

            _ = SetUpWords();
        }

        private async Task SetUpWords()
        {
            if (_words.Count == 0)
                _words = await _dataLoader.LoadDataFromJson("example.json");

            _words.Shuffle(); //shuffles in place

            _stateMachine.SetWord(_words[0]);

            SetWords();

            Debug.WriteLine(counter);
        }

        private async Task GoFurther()
        {
            _stateMachine.Next();

            if (_stateMachine.IsFinishedRound)
            {

                if (++counter >= _words.Count)
                {
                    counter = _words.Count - 1;
                    _stateMachine.SetState(1);
                    SetWords();
                    Debug.WriteLine(counter);
                    return;
                }

                Debug.WriteLine(counter);
            }

            if (_stateMachine.IsAtRoundStart)
            {
                _stateMachine.SetWord(_words[counter]);
            }

            SetWords();
        }

        private async Task GoBack()
        {
            _stateMachine.Previous();

            if (_stateMachine.IsAtRoundStart)
            {
                if (--counter <= 0)
                {
                    counter = 0;
                    _stateMachine.SetState(0);
                    SetWords();
                    Debug.WriteLine(counter);
                    return;
                }

                Debug.WriteLine(counter);
            }

            if (_stateMachine.IsFinishedRound)
            {
                _stateMachine.SetWord(_words[counter]);
            }


            SetWords();
        }

        private void SetWords()
        {
            GreekInGreek = _stateMachine.GetGreekInGreek();
            GreekInLatin = _stateMachine.GetGreekInLatin();
            EnglishTranslation = _stateMachine.GetEnglishTranslation();
        }
    }
}
