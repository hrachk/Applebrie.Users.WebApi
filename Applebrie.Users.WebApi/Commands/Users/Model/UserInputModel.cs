namespace Applebrie.Users.WebApi.Query.Model
{
    public class UserInputModel
    {
        /// <summary>
        /// НСИ Id
        /// </summary> 
        public Guid Id { get; set; } 

        /// <summary>
        /// FirstName
        /// </summary> 
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// LastName
        /// </summary> 
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// BirthDate
        /// </summary> 
        public DateTime BirthDate { get; set; }
    }
}
