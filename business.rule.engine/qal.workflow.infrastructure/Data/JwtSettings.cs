using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qal.workflow.infrastructure.Data
{
    public class JwtSettings
    {
        public string? Issuer { get; set; }
        public string? AudienceId { get; set; }
        public string? Thumbprint { get; set; }
    }
}
