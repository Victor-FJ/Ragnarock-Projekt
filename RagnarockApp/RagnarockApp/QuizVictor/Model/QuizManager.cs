﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RagnarockApp.QuizVictor.Exceptions;

namespace RagnarockApp.QuizVictor.Model
{
    public class QuizManager
    {
        private static QuizManager _instance = new QuizManager();
        public static QuizManager Instance
        { get { return _instance; } }


        public Quiz MarkedQuiz { get; set; }

        public List<Quiz> Quizzes { get; set; }

        private QuizManager()
        {
            Quizzes = new List<Quiz>();
        }

        #region QuizHandler

        /// <summary>
        /// Creates a new quiz in the QuizManagerInstance
        /// </summary>
        /// <param name="quizName">The quiz name for the quiz to be created. Must not be empty, whitespaced or used by another quiz</param>
        public void CreateQuiz(string quizName)
        {
            CheckName(quizName);
            Quizzes.Add(new Quiz(quizName));
            MarkedQuiz = FindQuiz(quizName);
        }

        /// <summary>
        /// Changes the name of an existing quiz in the QuizManagerInstance
        /// </summary>
        /// <param name="oldQuizName">The old quiz name of the quiz to be changed. Must be the name of an existing quiz</param>
        /// <param name="newQuizName">The new quiz name for the quiz. Must not be empty, whitespaced or used by another quiz</param>
        /// <param name="doChange">Desides if the name should be changed or if it should just do a check for wether it can</param>
        public void ModifyQuizName(string oldQuizName, string newQuizName, bool doChange)
        {
            CheckName(newQuizName);
            if (doChange)
                FindQuiz(oldQuizName).QuizName = newQuizName;
        }

        /// <summary>
        /// Deletes a quiz in the quizplayer
        /// </summary>
        /// <param name="quizName">The quiz name of the quiz to be deleted. Must be the name of an existing quiz</param>
        /// <param name="doChange">Desides if the name should be changed or if it should just do a check for wether it can</param>
        public void DeleteQuiz(string quizName, bool doChange)
        {
            if (doChange)
                Quizzes.Remove(FindQuiz(quizName));
        }

        #endregion

        private void CheckName(string quizName)
        {
            if (String.IsNullOrWhiteSpace(quizName))
                throw new ValueEmptyException("Navnet skal inkludere noget tekst");
            for (int i = 0; i < Quizzes.Count; i++)
                if (Quizzes[i].QuizName.ToLower() == quizName.ToLower())
                    throw new ValueAlreadyExistException("Dette navn er allerede brugt af en anden quiz");
        }

        private Quiz FindQuiz(string quizName)
        {
            for (int i = 0; i < Quizzes.Count; i++)
                if (Quizzes[i].QuizName == quizName)
                    return Quizzes[i];
            throw new DoesNotExistException("Denne quiz eksistere ikke");
        }
    }
}