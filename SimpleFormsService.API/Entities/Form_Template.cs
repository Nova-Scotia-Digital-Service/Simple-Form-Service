using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFormsService.API.Entities
{
    public class Form_Template
    {
        [Key]
        public Guid Template_Id { get; set; }

        [Column(TypeName = "jsonb")]
        public string Json_Config { get; set; }
    }
}
