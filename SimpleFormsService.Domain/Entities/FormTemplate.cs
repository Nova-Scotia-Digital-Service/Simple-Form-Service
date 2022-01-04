using SimpleFormsService.Domain.Entities.Base;

namespace SimpleFormsService.Domain.Entities
{
    public class FormTemplate : EntityBase
    {
        public FormTemplate(Guid id) : base(id)
        {}

        /*

        /// <summary>
        /// Represents a list of Authorized Admin users who will have access to view the contents of the submitted form. 
        /// The list will simply be the email address of the end user.  Email address will be provided by Azure AD via OpenID Connect authentication. 
        /// </summary>
        public List<string> AuthorizedAdminUsers { get; set; }

        public List<string> Email { get; set; }

        */
    }
}