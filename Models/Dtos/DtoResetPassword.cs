using System.ComponentModel.DataAnnotations;

namespace Store.Models.Dtos
{
    public class DtoResetPassword
    {
        public string userName { get; set; }
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

    }
}
