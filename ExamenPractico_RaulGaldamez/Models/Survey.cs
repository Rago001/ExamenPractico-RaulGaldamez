using System.ComponentModel.DataAnnotations;

namespace ExamenPractico_RaulGaldamez.Models {

    public class Survey {

        [Key]
        public int idSurvey { get; set; }
        public string surveyName { get; set; }
        public string surveyDescription { get; set; }
        public string userName { get; set; }

    }
}
