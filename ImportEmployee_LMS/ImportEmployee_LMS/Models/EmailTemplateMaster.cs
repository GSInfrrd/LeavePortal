//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImportEmployee_LMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmailTemplateMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmailTemplateMaster()
        {
            this.EmailTemplateMappings = new HashSet<EmailTemplateMapping>();
        }
    
        public int Id { get; set; }
        public string Template { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmailTemplateMapping> EmailTemplateMappings { get; set; }
    }
}
