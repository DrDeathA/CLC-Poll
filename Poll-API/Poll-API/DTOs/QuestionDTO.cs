using Poll_API.Data.Entities;

namespace Poll_API.DTOs
{
    public class QuestionDTO
    {
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public string SubText { get; set; }
        public List<OptionDTO> Options { get; set; }
        public int? AnswerOptionId { get; set; }

        public QuestionDTO()
        {
        }

        public QuestionDTO(Question quesiton)
        {
            QuestionId = quesiton.QuestionId;
            Title = quesiton.Title;
            SubText = quesiton.SubText;
            Options = new List<OptionDTO>();
            foreach (var option in quesiton.Options)
            {
                Options.Add(new OptionDTO(option));
            }
        }
    }
}
