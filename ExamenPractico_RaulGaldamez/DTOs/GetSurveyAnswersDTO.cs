namespace ExamenPractico_RaulGaldamez.DTOs {

    public class GetSurveyAnswersDTO {

        public int idSurvey { get; set; }
        public string surveyName { get; set; }
        public string surveyDescription { get; set; }
        public string userName { get; set; }

        public List<GetSurveyFieldsAnswerDTO> fieldsAnswerDTOs { get; set; }

    }
}
