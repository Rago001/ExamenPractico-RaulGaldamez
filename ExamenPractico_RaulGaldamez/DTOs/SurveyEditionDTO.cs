namespace ExamenPractico_RaulGaldamez.DTOs {

    public class SurveyEditionDTO {

        public string surveyName { get; set; }
        public string surveyDescription { get; set; }

        public List<FieldCreationDTO> fieldDTOList { get; set; }

    }
}
