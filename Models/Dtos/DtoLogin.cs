using System.ComponentModel.DataAnnotations;

namespace Store.Models.Dtos
{
    public class DtoLogin
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
