namespace ExamenPractico_RaulGaldamez.DTOs {

    public class GetFieldDTO {

        public int idField { get; set; }
        public string fieldName { get; set; }
        public string fieldTitle { get; set; }
        public bool isRequired { get; set; }
        public int fieldType { get; set; }
        public int idSurvey { get; set; }

    }
}
