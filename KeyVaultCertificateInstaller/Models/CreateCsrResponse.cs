using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KeyVaultCertificateInstaller.Models
{
    public class CreateCsrResponse
    {
        public string RequestId { get; set; }
        public string CertId { get; set; }
        public string Csr { get; set; }
        public string Status { get; set; }
        public string StatusDetails { get; set; }
        public string Error { get; set; }
    }
}
