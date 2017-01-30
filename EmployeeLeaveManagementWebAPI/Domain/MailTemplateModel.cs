using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_Domain
{
    public class MailTemplateModel
    {
        public int Id { get; set; }
        public string ActionName { get; set; }
        public int RefTemplateId { get; set; }

        public virtual EmailTemplateMaster EmailTemplateMaster { get; set; }
    }
}
