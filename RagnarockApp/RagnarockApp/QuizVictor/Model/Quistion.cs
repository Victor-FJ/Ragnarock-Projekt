using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RagnarockApp.QuizVictor.Exceptions;

namespace RagnarockApp.QuizVictor.Model
{
    public class Quistion
    {
        private string _theQuistion;
        private string _hint;
        private string[] _answerOptions;
        private int _answer;

        public string TheQuistion
        {
            get { return _theQuistion; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ValueEmptyException("The quistion has to include some text");
                if (!value.Contains('?'))
                    throw new IsNotQuistionException("The quistion has to include a '?'");
                _theQuistion = value;
            }
        }

        public string Hint
        {
            get { return _hint; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ValueEmptyException("The hint has to include some text");
                _hint = value;
            }
        }


        public string[] AnswerOptions
        {
            get { return _answerOptions; }
            set
            {
                if (value == null)
                    throw  new ValueEmptyException("The AnswerOptionsInput cannot be set to null");
                if (value.Length != 4)
                    throw new IncorrectSizeException("There has to be exactly 4 answers options");
                for (int i = 0; i < value.Length; i++)
                {
                    if (String.IsNullOrWhiteSpace(value[i]))
                        throw new ValueEmptyException($"The {i + 1}. answer option has to include some text");
                    for (int j = 0; j < i; j++)
                        if (value[i] == value[j])
                            throw new ValueAlreadyExistException($"The {i + 1}. answer option cannot be the same as the {j + 1}.");
                }
                _answerOptions = value;
            }
        }

        public int Answer
        {
            get { return _answer; }
            set
            {
                if (value < 0 || value > _answerOptions.Length)
                    throw new IndexOutOfRangeException("The answer has to be between one of the 4 answer options");
                _answer = value;
            }
        }

        public Quistion()
        {
            _theQuistion = "Is this the default quistion?";
            _answerOptions = new string[] {"A: ...", "B: ...", "C: ...", "D: ..."};
            _hint = "A hint";
        }

        public Quistion(string theQuistion, string hint, string[] answerOptions, int answer)
            : this()
        {
            TheQuistion = theQuistion;
            Hint = hint;
            AnswerOptions = answerOptions;
            Answer = answer;
        }
    }
}