using System.ComponentModel.DataAnnotations;

namespace Talabat.API.Dtos
{
    public class AddressDto
    {
        // AddressDto use in Endp UpdateUserAdd,CreateOrder 'AddDto deal with Add of user & Ord but diff in map in MapProf'
        [Required] 
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }


    }
}
