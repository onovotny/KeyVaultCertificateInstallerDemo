using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KeyVaultCertificateInstaller.Models
{
    public class CreateCsrRequest
    {
        [Required]
        public string VaultName { get; set; }

        [Required]
        public string CertificateName { get; set; }
    }
}
