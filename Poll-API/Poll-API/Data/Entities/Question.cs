using Poll_API.DTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poll_API.Data.Entities
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public string SubText { get; set; }
        public int PollId { get; set; }
        [ForeignKey("PollId")] public Poll Poll { get; set; }
        [InverseProperty("Question")] public ICollection<Option> Options { get; set; }

        public Question()
        {
        }

        public Question(QuestionDTO question, List<Option> options)
        {
            Title = question.Title;
            SubText = question.SubText;
            Options = options;
        }
    }
}
