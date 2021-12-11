using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFormsService.API.Entities
{
    [NotMapped]
    public class Form_Submission
    {
        [Key]
        public Guid Submission_Id { get; set; }
        public Guid Template_Id { get; set; }

        [Column(TypeName = "jsonb")]
        public string Form_Data { get; set; } 
    }
}
