using System.ComponentModel.DataAnnotations;

namespace Store.Models.Dtos
{
    public class DtoNewUser
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string email { get; set; }
        public string? phoneNumber { get; set; }

    }
}
