namespace ExamenPractico_RaulGaldamez.DTOs {

    public class GetSurveyDTO {

        public int idSurvey { get; set; }
        public string surveyName { get; set; }
        public string surveyDescription { get; set; }
        public string userName { get; set; }

        public List<GetFieldDTO> fieldsDTOs { get; set; }

    }
}
