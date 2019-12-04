﻿using System;
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
        private string[] _answerOptions;
        private int _answer;

        public string TheQuistion
        {
            get { return _theQuistion; }
            set
            {
                if (value == null || value == "")
                    throw new QuistionPropEmptyException("The quistion has to include some text");
                if (!value.Contains('?'))
                    throw new IsNotQuistionException("The quistion has to include a '?'");
                _theQuistion = value;
            }
        }
        
        public string[] AnswerOptions
        {
            get { return _answerOptions; }
            set
            {
                if (value == null)
                    throw  new QuistionPropEmptyException("The AnswerOptions cannot be set to null");
                if (value.Length != 4)
                    throw new AnsOptIncorrectSizeException("There has to be exactly 4 answers options");
                for (int i = 0; i < value.Length; i++)
                    if (value[i] == null || value[i] == "")
                        throw new QuistionPropEmptyException($"The {i}. answer option has to include some text");
                _answerOptions = value;
            }
        }

        public int Answer
        {
            get { return _answer; }
            set
            {
                if (value < 0 || value > _answerOptions.Length)
                    throw new IndexOutOfRangeException("The answer has to be between 0 and 3 and be the index for the correct answer in AnswerOptions");
                _answer = value;
            }
        }
    }
}