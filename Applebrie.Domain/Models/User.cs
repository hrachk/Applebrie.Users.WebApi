using System.ComponentModel.DataAnnotations;

namespace Applebrie.Domain
{
    public class User : EntityBase
    {
       
        /// <summary>
        /// FirstName
        /// </summary>
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        /// <summary>
        /// BirthDate
        /// </summary>
        [Display(Name = "BirthDate")]
        public DateTime BirthDate { get; set; }
         
    }
}
