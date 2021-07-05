using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContratoSeguro.Api.DocuSign
{
    public class DocuSignTemplate
    {
        public DocuSignTemplate(string templateId, IList<string> templateRoleNames)
        {
            this.TemplateId = templateId;
            this.TemplateRoleNames = templateRoleNames;
        }

        public IList<string> TemplateRoleNames { get; set; }

        public string TemplateId { get; set; }
    }
}
