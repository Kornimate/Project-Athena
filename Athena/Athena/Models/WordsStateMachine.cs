using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Models
{
    public class WordsStateMachine
    {
        public bool IsFinishedRound
        {
            get => _state == 1;
        }

        public bool IsAtRoundStart
        {
            get => _state == 0;
        }

        private WordModel _wordModel;
        private int _state = 0;

        public void Next()
        {
            _state = (_state + 1) % 2;
        }

        public void Previous()
        {
            _state = _state == 0 ? 1 : _state - 1;
        }

        public void SetWord(WordModel word)
        {
            ArgumentNullException.ThrowIfNull(word);

            _wordModel = word;
        }
        public void SetState(int newState)
        {
            if (newState > 1 || newState < 0)
                throw new ArgumentOutOfRangeException(nameof(newState));

            _state = newState;
        }

        public string? GetEnglishTranslation()
        {
            return _state switch
            {
                0 => "",
                _ => _wordModel?.EnglishTranslation
            };
        }

        public string? GetGreekInGreek()
        {
            return _wordModel?.GreekInGreek;
        }

        public string? GetGreekInLatin()
        {
            return _state switch
            {
                0 => "",
                _ => _wordModel?.GreekInLatin
            };
        }
    }
}
