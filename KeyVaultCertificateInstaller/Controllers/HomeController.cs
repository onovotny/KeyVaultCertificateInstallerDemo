using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KeyVaultCertificateInstaller.Models;
using Microsoft.AspNetCore.Authentication;
using KeyVaultCertificateInstaller.Services;

namespace KeyVaultCertificateInstaller.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
          //  var accessToken = await HttpContext.GetTokenAsync("access_token");
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateCSR(CreateCsrRequest request, [FromServices] KeyVaultService service)
        {
            var res = await service.CreateCSR(request.VaultName, request.CertificateName);


            return View(res);
        }


        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
