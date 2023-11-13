using Poll_API.Data.Entities;

namespace Poll_API.DTOs
{
    public class PollDTO
    {
        public int PollId { get; set; }
        public string Topic { get; set; }
        public List<QuestionDTO> Questions { get; set; }

        public PollDTO()
        {
        }

        public PollDTO(Poll poll)
        {
            PollId = poll.PollId;
            Topic = poll.Topic;
            Questions = new List<QuestionDTO>();
            foreach (var question in poll.Questions)
            {
                Questions.Add(new QuestionDTO(question));
            }
        }
    }
}
