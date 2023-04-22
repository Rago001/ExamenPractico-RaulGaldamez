namespace ExamenPractico_RaulGaldamez.DTOs {

    public class GetSurveyFieldsAnswerDTO {

        public int idField { get; set; }
        public string fieldName { get; set; }
        public string fieldTitle { get; set; }
        public bool isRequired { get; set; }
        public int fieldType { get; set; }
        public int idSurvey { get; set; }

        public List<GetAnswersDTO> answersDTOs { get; set; }

    }
}
