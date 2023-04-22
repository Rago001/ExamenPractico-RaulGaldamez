using System.ComponentModel.DataAnnotations;

namespace ExamenPractico_RaulGaldamez.Models {

    public class Answer {

        [Key]
        public int idAnswer { get; set; }
        public string answer { get; set; }
        public int idField { get; set; }

    }
}
