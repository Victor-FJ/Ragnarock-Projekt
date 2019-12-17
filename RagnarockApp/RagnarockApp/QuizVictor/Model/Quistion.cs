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
                    throw new ValueEmptyException("Spørgsmålet skal inkludere noget tekst");
                if (!value.Contains('?'))
                    throw new IsNotQuistionException("Spørgsmålet skal indeholde et '?'");
                _theQuistion = value;
            }
        }

        public string Hint
        {
            get { return _hint; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ValueEmptyException("Hintet skal inkludere noget tekst");
                _hint = value;
            }
        }


        public string[] AnswerOptions
        {
            get { return _answerOptions; }
            set
            {
                if (value == null)
                    throw  new ValueEmptyException("AnswerOptionsInput kan ikke blive sat til null");
                if (value.Length != 4)
                    throw new IncorrectSizeException("Der skal være præcis 4 svars mugligheder");
                for (int i = 0; i < value.Length; i++)
                {
                    if (String.IsNullOrWhiteSpace(value[i]))
                        throw new ValueEmptyException($"{i + 1}. svarsmuglighed skal inkludere noget tekst");
                    for (int j = 0; j < i; j++)
                        if (value[i].ToLower() == value[j].ToLower())
                            throw new ValueAlreadyExistException($"{i + 1}. svarsmuglighed kan ikke være det samme som {j + 1}.");
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
                    throw new IndexOutOfRangeException("Svaret skal være 1 af de 4 svarsmugligheder");
                _answer = value;
            }
        }

        public int[] AnswerStats { get; set; }

        public int TotalStats
        {
            get
            {
                int total = 0;
                foreach (int answerStat in AnswerStats)
                    total += answerStat;
                if (total == 0)
                    return 1;
                return total;
            }
        }

        public Quistion()
        {
            _theQuistion = "Is this the default quistion?";
            _answerOptions = new string[] {"A: ...", "B: ...", "C: ...", "D: ..."};
            _hint = "A hint";
            AnswerStats = new int[4];
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