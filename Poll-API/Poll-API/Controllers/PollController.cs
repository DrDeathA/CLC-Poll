using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poll_API.Data;
using Poll_API.Data.Entities;
using Poll_API.DTOs;

namespace Poll_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {
        public PollController()
        {
        }

        [HttpPost("Create/{userId}")]
        public async Task<IActionResult> CreatePoll(Guid userId, PollDTO poll)
        {
            using var db = new DataContext();

            var questions = new List<Question>();

            foreach (var question in poll.Questions)
            {
                var options = new List<Option>();
                foreach (var option in question.Options)
                {
                    options.Add(new Option(option));
                }
                questions.Add(new Question(question, options));
            }

            var newPoll = new Poll(userId, poll, questions);

            await db.Poll.AddAsync(newPoll);

            if (await db.SaveChangesAsync() < 1) return BadRequest("Failed Save Poll");

            return Ok("Poll Added successfully");
        }

        [HttpGet("GetPolls")]
        public async Task<IActionResult> GetPolls()
        {
            using var db = new DataContext();
            var dbPolls = await db.Poll
                .Include(p => p.Questions)
                .ThenInclude(q => q.Options)
                .ToListAsync();

            if (dbPolls == null) return NotFound("Poll Not Found");

            var polls = new List<PollDTO>();

            foreach (var poll in dbPolls)
            {
                polls.Add(new PollDTO(poll));
            }

            return Ok(polls);
        }

        [HttpGet("GetPoll/{pollId}")]
        public async Task<IActionResult> GetPoll(int pollId)
        {
            using var db = new DataContext();
            var dbPoll = await db.Poll
                .Include(p => p.Questions)
                .ThenInclude(q => q.Options)
                .SingleOrDefaultAsync(x => x.PollId == pollId);

            if (dbPoll == null) return NotFound("Poll Not Found");

            return Ok(new PollDTO(dbPoll));
        }

        [HttpPost("Answer")]
        public async Task<IActionResult> AnswerPoll(PollDTO poll)
        {
            using var db = new DataContext();
            var dbPoll = await db.Poll
                .Include(p => p.Questions)
                .ThenInclude(q => q.Options)
                .SingleOrDefaultAsync(x => x.PollId == poll.PollId);

            foreach (var question in poll.Questions)
            {
                var dbQuestion = dbPoll.Questions.SingleOrDefault(x => x.QuestionId == question.QuestionId);
                var dbOption = dbQuestion.Options.SingleOrDefault(o => o.OptionId == question.AnswerOptionId);
                dbOption.VoteCount++;
            }

            if (await db.SaveChangesAsync() < 1) return BadRequest("Failed Save Poll");

            return Ok("Poll Added successfully");
        }
    }
}
