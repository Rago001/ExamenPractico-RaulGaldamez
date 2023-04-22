using System.ComponentModel.DataAnnotations;

namespace ExamenPractico_RaulGaldamez.Models {

    public class Field {

        [Key]
        public int idField { get; set; }
        public string fieldName { get; set; }
        public string fieldTitle { get; set; }
        public bool isRequired { get; set; }
        public int fieldType { get; set; }
        public int idSurvey { get; set; }

    }
}
