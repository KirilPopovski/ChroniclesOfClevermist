using ChroniclesOfClevermist.Data.Common.Models;
using System;

namespace ChroniclesOfClevermist.Data.Models
{
    public class Answer : BaseDeletableModel<string>
    {
        public Answer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Text { get; set; }

        public string QuestionId { get; set; }

        public Question Question { get; set; }
    }
}
