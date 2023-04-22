using AutoMapper;
using ExamenPractico_RaulGaldamez.DTOs;
using ExamenPractico_RaulGaldamez.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ExamenPractico_RaulGaldamez.Controllers {

    [ApiController]
    [Route("api/survey")]
    public class SurveyController: ControllerBase {

        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public SurveyController(AppDbContext context, IMapper mapper) {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost("createSurvey")]
        public async Task<ActionResult> CreateSurvey(SurveyCreationDTO surveyDTO) {

            var newSurvey = mapper.Map<Survey>(surveyDTO);

            context.Add(newSurvey);
            await context.SaveChangesAsync();

            foreach (var fieldDTO in surveyDTO.fieldDTOList) {

                var newField = mapper.Map<Field>(fieldDTO);
                newField.idSurvey = newSurvey.idSurvey;

                context.Add(newField);
                await context.SaveChangesAsync();

            }

            return Ok();
        
        }

        /*[HttpPost("{idSurvey:int}")]
        public async Task<ActionResult> AnswerSurvey(int idSurvey) { 
        
            
        
        }*/

        [HttpPut("{idSurvey:int}/editSurvey")]
        public async Task<ActionResult> Put(SurveyEditionDTO editionDTO, int idSurvey) {

            var selectedSurvey = await context.Survey.FirstOrDefaultAsync(x => x.idSurvey == idSurvey);

            if (selectedSurvey == null) {
                return NotFound();
            }

            selectedSurvey.surveyName = editionDTO.surveyName;
            selectedSurvey.surveyDescription = editionDTO.surveyDescription;

            context.Update(selectedSurvey);
            await context.SaveChangesAsync();

            await context.Database.ExecuteSqlAsync($"DELETE FROM Field WHERE idSurvey = {idSurvey}");

            foreach (var fieldDTO in editionDTO.fieldDTOList) {

                var newField = mapper.Map<Field>(fieldDTO);
                newField.idSurvey = selectedSurvey.idSurvey;

                context.Add(newField);
                await context.SaveChangesAsync();

            }

            return Ok();
        
        }

        [HttpDelete("{idSurvey:int}/deleteSurvey")]
        public async Task<ActionResult> Delete(int idSurvey) {

            var exists = await context.Survey.AnyAsync( x => x.idSurvey == idSurvey );

            if (!exists) {
                return NotFound();
            }

            context.Remove(new Survey() { idSurvey = idSurvey });
            await context.SaveChangesAsync();

            return Ok();
        
        }

    }
}
