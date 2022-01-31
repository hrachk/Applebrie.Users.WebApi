using Applebrie.Domain;
using AutoMapper;

namespace Applebrie.Users.WebApi.Queries.Models
{
    [AutoMap(typeof(User))]
    public class UserModel
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
