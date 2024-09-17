namespace Application.Dtos
{
    /// <summary>
    /// Data Transfer Object (DTO) used for user login requests.
    /// Contains the necessary fields to authenticate a user.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// The username of the user attempting to log in.
        /// </summary>
        public required string Username { get; set; }

        /// <summary>
        /// The password of the user attempting to log in.
        /// </summary>
        public required string Password { get; set; }
    }

    /// <summary>
    /// Data Transfer Object (DTO) used for user registration requests.
    /// Contains the necessary fields to register a new user.
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// The desired username for the new user.
        /// </summary>
        public required string Username { get; set; }

        /// <summary>
        /// The desired password for the new user.
        /// </summary>
        public required string Password { get; set; }
    }

}