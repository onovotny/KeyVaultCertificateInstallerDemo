using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyVaultCertificateInstaller.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Rest;

namespace KeyVaultCertificateInstaller.Services
{
    public class KeyVaultService 
    {
        private readonly IHttpContextAccessor contextAccessor;

        public KeyVaultService(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        async Task<KeyVaultClient> CreateClient()
        {
            var token = await contextAccessor.HttpContext.GetTokenAsync("access_token");
            return new KeyVaultClient(new TokenCredentials(token));
        }

        public async Task<CreateCsrResponse> CreateCSR(string vaultName, string certName)
        {
            var client = await CreateClient();

            //client.GetPendingCertificateSigningRequestAsync
            var policy = new CertificatePolicy
            {
                KeyProperties = new KeyProperties
                {
                    KeyType = "RSA-HSM"
                },
                IssuerParameters = new IssuerParameters
                {
                    Name = "Unknown"
                },
                X509CertificateProperties = new X509CertificateProperties
                {
                    ValidityInMonths = 36,
                    Subject = "CN=foo" // TODO: I don't think this can be blank
                }
            };

            try
            {
                var res = await client.CreateCertificateAsync($"https://{vaultName}.vault.azure.net/", certName, policy);
                var csr = Convert.ToBase64String(res.Csr);

                return new CreateCsrResponse
                {
                    Csr = csr,
                    RequestId = res.RequestId,
                    CertId = res.Id,
                    Status = res.Status,
                    StatusDetails = res.StatusDetails,
                    Error = res.Error?.Message
                };
            }
            catch (KeyVaultErrorException e)
            {
                return new CreateCsrResponse
                {
                    Error = e.Message,
                };
            }
        }
    }
}
