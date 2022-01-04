using System;
using System.Collections.Generic;
using System.Text;

namespace Cesibet.Models
{
    class QuestionsModel
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }

    class Answers
    {
        public string QuestionAnswer { get; set; }
        public int Estimate { get; set; }
    }

}
