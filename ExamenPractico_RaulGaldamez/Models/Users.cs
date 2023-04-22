using System.ComponentModel.DataAnnotations;

namespace ExamenPractico_RaulGaldamez.Models {

    public class Users {

        [Key]
        public string userName { get; set; }
        public string userPassword { get; set; }

    }
}
