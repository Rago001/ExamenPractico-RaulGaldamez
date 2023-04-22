using AutoMapper;
using ExamenPractico_RaulGaldamez.DTOs;
using ExamenPractico_RaulGaldamez.Models;

namespace ExamenPractico_RaulGaldamez.Utilities {

    public class AutoMapperProfiles: Profile {

        public AutoMapperProfiles() {

            CreateMap<SurveyCreationDTO, Survey>();
            CreateMap<FieldCreationDTO, Field>();
            CreateMap<SurveyEditionDTO, Survey>();
            CreateMap<Field, GetFieldDTO>();
            CreateMap<AnswerCreationDTO, Answer>();

        }

    }
}
