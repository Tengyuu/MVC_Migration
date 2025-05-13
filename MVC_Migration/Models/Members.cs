using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVC_Migration.Models
{
    public class Members
    {
        //[Required] int,float,decimal,datetime已繼承Required，無須再加Required
        [Display(Name= "編號")]
        public int Id { get; set; }
        [Required(ErrorMessage = "請輸入名字")]
        public string Name { get; set; }
        //[Required]
        public int Age { get; set; }
        //[DataType(DataType.EmailAddress)]

    }
}
