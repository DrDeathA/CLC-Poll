using Poll_API.Data.Entities;

namespace Poll_API.DTOs
{
    public class OptionDTO
    {
        public int OptionId { get; set; }
        public string OptionText { get; set; }
        public int VoteCount { get; set; }

        public OptionDTO()
        {
        }

        public OptionDTO(Option option)
        {
            OptionId = option.OptionId;
            OptionText = option.OptionText;
            VoteCount = option.VoteCount;
        }
    }
}
