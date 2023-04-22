using AutoMapper;
using ExamenPractico_RaulGaldamez.DTOs;
using ExamenPractico_RaulGaldamez.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpPut("{idSurvey:int}/editSurvey")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> EditSurvey(SurveyEditionDTO editionDTO, int idSurvey) {

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> DeleteSurvey(int idSurvey) {

            var exists = await context.Survey.AnyAsync( x => x.idSurvey == idSurvey );

            if (!exists) {
                return NotFound();
            }

            context.Remove(new Survey() { idSurvey = idSurvey });
            await context.SaveChangesAsync();

            return Ok();
        
        }

        [HttpGet("{idSurvey:int}")]
        public async Task<ActionResult<GetSurveyDTO>> AnswerSurvey(int idSurvey) {

            var selectedSurvey = await context.Survey.FirstOrDefaultAsync(x => x.idSurvey == idSurvey);

            if (selectedSurvey == null) { 
                return NotFound(); 
            }

            var selectedSurveyFields = await context.Field.Where(x => x.idSurvey == idSurvey).ToListAsync();

            var surveyDTO = new GetSurveyDTO();

            surveyDTO.idSurvey = selectedSurvey.idSurvey;
            surveyDTO.surveyName = selectedSurvey.surveyName;
            surveyDTO.surveyDescription = selectedSurvey.surveyDescription;
            surveyDTO.userName = selectedSurvey.userName;
            surveyDTO.fieldsDTOs = new List<GetFieldDTO>();

            foreach (var field in selectedSurveyFields) {

                var fieldDTO = mapper.Map<GetFieldDTO>(field);
                surveyDTO.fieldsDTOs.Add(fieldDTO);

            }

            return surveyDTO;

        }

        [HttpPost("{idSurvey:int}")]
        public async Task<ActionResult> AnswerSurvey(int idSurvey, AnswerSurveyDTO answerSurveyDTO) {

            var selectedSurvey = await context.Survey.FirstOrDefaultAsync(x => x.idSurvey == idSurvey);

            if (selectedSurvey == null) {
                return NotFound();
            }

            foreach (var answerDTO in answerSurveyDTO.answersDTOList) { 
            
                var newAnswer = mapper.Map<Answer>(answerDTO);

                context.Add(newAnswer);
                await context.SaveChangesAsync();
            
            }

            return Ok();

        }

        [HttpGet("{idSurvey:int}/answers")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<GetSurveyAnswersDTO>> GetSurveyAnswers(int idSurvey) {

            var selectedSurvey = await context.Survey.FirstOrDefaultAsync(x => x.idSurvey == idSurvey);

            if (selectedSurvey == null)
            {
                return NotFound();
            }

            var selectedSurveyFields = await context.Field.Where(x => x.idSurvey == idSurvey).ToListAsync();

            var surveyAnswersDTO = new GetSurveyAnswersDTO();

            surveyAnswersDTO.idSurvey = selectedSurvey.idSurvey;
            surveyAnswersDTO.surveyName = selectedSurvey.surveyName;
            surveyAnswersDTO.surveyDescription = selectedSurvey.surveyDescription;
            surveyAnswersDTO.userName = selectedSurvey.userName;

            surveyAnswersDTO.fieldsAnswerDTOs = new List<GetSurveyFieldsAnswerDTO>();

            foreach (var field in selectedSurveyFields) {

                var selectedFieldAnwers = await context.Answer.Where(x => x.idField == field.idField).ToListAsync();

                var fieldAnswersDTO = new GetSurveyFieldsAnswerDTO();

                fieldAnswersDTO.idField = field.idField;
                fieldAnswersDTO.fieldName = field.fieldName;
                fieldAnswersDTO.fieldTitle = field.fieldTitle;
                fieldAnswersDTO.isRequired = field.isRequired;
                fieldAnswersDTO.fieldType = field.fieldType;
                fieldAnswersDTO.idSurvey = field.idSurvey;

                fieldAnswersDTO.answersDTOs = new List<GetAnswersDTO>();

                foreach (var answer in selectedFieldAnwers) {
                    
                    var answersDTO = new GetAnswersDTO();

                    answersDTO.idAnswer = answer.idAnswer;
                    answersDTO.answer = answer.answer;
                    answersDTO.idField = answer.idField;

                    fieldAnswersDTO.answersDTOs.Add(answersDTO);

                }

                surveyAnswersDTO.fieldsAnswerDTOs.Add(fieldAnswersDTO);

            }

            return surveyAnswersDTO;

        }

    }
}
