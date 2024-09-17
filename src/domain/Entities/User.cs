using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Represents a user in the system. 
    /// Contains the username and the hashed password for authentication purposes.
    /// </summary>
    public class User
    {
        /// <summary>
        /// The unique identifier for the user.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The username of the user. This field is required.
        /// </summary>
        public required string Username { get; set; }

        /// <summary>
        /// The hashed password of the user. This field is required.
        /// </summary>
        public required string PasswordHash { get; set; }
    }
}
