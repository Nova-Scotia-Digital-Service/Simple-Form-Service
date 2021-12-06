﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFormsService.API.Entities
{
    public class Form_Submission
    {
        [Key]
        public Guid Submission_Id { get; set; }
        public Guid Template_Id { get; set; }

        [Column(TypeName = "jsonb")]
        public string Json_Data { get; set; }

        public Form_Template Form_Template { get; set; }
    }
}