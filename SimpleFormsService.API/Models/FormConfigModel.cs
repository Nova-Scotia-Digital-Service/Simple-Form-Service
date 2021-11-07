using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SimpleFormsService.API.Model
{
    public class FormConfigModel
    {

        [JsonPropertyName("formId")]
        public string FormId { get; set; }


        /// <summary>
        /// Represents a list of Authorized Admin users who will have access to view the contents of the submitted form. 
        /// The list will simply be the email address of the end user.  Email address will be provided by Azure AD via OpenID Connect authentication. 
        /// </summary>
        public List<string> AuthorizedAdminUsers { get; set; }



            
    }
}
