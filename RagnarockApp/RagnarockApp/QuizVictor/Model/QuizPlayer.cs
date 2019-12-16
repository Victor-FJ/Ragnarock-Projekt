using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarockApp.QuizVictor.Model
{
    public class QuizPlayer
    {
        private static QuizPlayer _instance = new QuizPlayer();
        public static QuizPlayer Instance
        { get { return _instance; } }


        public PlaySession CurrentPlaySession { get; set; }
        public int MarkedQuistionNo { get; set; }

        private QuizPlayer()
        {
            
        }
    }
}
