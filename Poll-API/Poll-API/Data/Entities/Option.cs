using Poll_API.DTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poll_API.Data.Entities
{
    public class Option
    {
        public int OptionId { get; set; }
        public int VoteCount { get; set; }
        public string OptionText { get; set; }
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")] public Question Question { get; set; }

        public Option()
        {
        }

        public Option(OptionDTO option)
        {
            OptionText = option.OptionText;
        }
    }
}
