using Poll_API.DTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poll_API.Data.Entities
{
    public class Poll
    {
        public int PollId { get; set; }
        public string Topic { get; set; }
        [InverseProperty("Poll")] public ICollection<Question> Questions { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")] public User User { get; set; }

        public Poll()
        {
        }

        public Poll(Guid userId, PollDTO poll, List<Question> questions)
        {
            UserId = userId;
            Topic = poll.Topic;
            Questions = questions;
        }
    }
}
