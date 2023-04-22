namespace ExamenPractico_RaulGaldamez.DTOs {

    public class SurveyCreationDTO {

        public string surveyName { get; set; }
        public string surveyDescription { get; set; }
        public string userName { get; set; }

        public List<FieldCreationDTO> fieldDTOList { get; set; }

    }
}
