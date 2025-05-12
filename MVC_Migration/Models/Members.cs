using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVC_Migration.Models
{
    public class Members
    {
        [Required]
        [Display(Name= "編號")]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

    }
}
