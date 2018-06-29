using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p0llAnalyser.objects
{
    class Question
    {
        public int id { get; set; }
        public String title { get; set; }
        public String description { get; set; }
        public String answertype { get; set; }

        private List<Answer> mAnswer;
        public List<Answer> answers
        {
            get
            {
                if (mAnswer == null)
                {
                    mAnswer = new List<Answer>();
                }
                return mAnswer;
            }
        }
    }
}
