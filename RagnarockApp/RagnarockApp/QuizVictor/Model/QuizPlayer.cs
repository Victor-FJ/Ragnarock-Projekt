using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RagnarockApp.QuizVictor.Exceptions;

namespace RagnarockApp.QuizVictor.Model
{
    public class QuizPlayer
    {
        private static QuizPlayer _instance = new QuizPlayer();
        public static QuizPlayer Instance
        { get { return _instance; } }

        public List<Quiz> Quizzes { get; set; }

        private QuizPlayer()
        {
            Quizzes = new List<Quiz>();
            Quizzes.Add(new Quiz("A test quiz"));
            Quizzes.Add(new Quiz("Another quiz"));
            Quizzes[0].Quistions.Add(new Quistion());
            Quizzes[0].Quistions.Add(new Quistion());
        }

        /// <summary>
        /// Creates a new quiz in the QuizPlayer
        /// </summary>
        /// <param name="quizName">The quiz name for the quiz to be created. Must not be empty, whitespaced or used by another quiz</param>
        public void CreateQuiz(string quizName)
        {
            if (CheckName(quizName) != -1)
                throw new NameAlreadyExistException("This name is already used by another quiz");
            Quizzes.Add(new Quiz(quizName));
        }

        /// <summary>
        /// Changes the name of an existing quiz in the QuizPlayer
        /// </summary>
        /// <param name="oldQuizName">The old quiz name of the quiz to be changed. Must be the name of an existing quiz</param>
        /// <param name="newQuizName">The new quiz name for the quiz. Must not be empty, whitespaced or used by another quiz</param>
        public void ModifyQuizName(string oldQuizName, string newQuizName)
        {
            int index = CheckName(oldQuizName);
            if (index == -1)
                throw new DoesNotExistException("This quiz does not exist");
            if (CheckName(newQuizName) != -1)
                throw new NameAlreadyExistException("This name is already used by another quiz");
            Quizzes[index].QuizName = newQuizName;
        }

        /// <summary>
        /// Checks the name of a quiz to determine if its valid and if it already used by a quiz in the QuizPlayer
        /// </summary>
        /// <param name="quizName">The quiz name to test</param>
        /// <returns></returns>
        private int CheckName(string quizName)
        {
            if (String.IsNullOrWhiteSpace(quizName))
                throw new ValueEmptyException("The name has to include some text");
            for (int i = 0; i < Quizzes.Count; i++)
                if (Quizzes[i].QuizName == quizName)
                    return i;
            return -1;
        }
    }
}