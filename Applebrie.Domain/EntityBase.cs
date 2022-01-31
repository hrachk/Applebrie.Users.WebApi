using System.ComponentModel.DataAnnotations; 

namespace Applebrie.Domain
{
    public abstract class EntityBase : IEntityBase
    {
        /// <summary>
        /// Entity ID
        /// </summary>
        [Display(Name = "Id")]
        [Required]
        public Guid Id { get; set; }

    }
}
