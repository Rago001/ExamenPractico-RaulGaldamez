using ExamenPractico_RaulGaldamez.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamenPractico_RaulGaldamez.Controllers {

    [ApiController]
    [Route("api/survey")]
    public class SurveyController: ControllerBase {

        private readonly AppDbContext context;

        public SurveyController(AppDbContext context) {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Survey>>> Get() {

            return await context.Survey.ToListAsync();
        
        }

        [HttpPost]
        public async Task<ActionResult> Post(Survey survey) { 
        
            context.Add(survey);

            await context.SaveChangesAsync();

            return Ok();
        
        }

    }
}
