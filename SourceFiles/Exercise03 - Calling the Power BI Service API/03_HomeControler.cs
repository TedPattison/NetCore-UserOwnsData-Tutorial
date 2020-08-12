using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserOwnsData.Models;
using UserOwnsData.Services;

namespace UserOwnsData.Controllers {

  [Authorize]  
  public class HomeController : Controller {

    private PowerBiServiceApi powerBiServiceApi;

    public HomeController(PowerBiServiceApi powerBiServiceApi) {
      this.powerBiServiceApi = powerBiServiceApi;
    }


    [AllowAnonymous]
    public IActionResult Index() {
      return View();
    }

    public async Task<IActionResult> Embed() {

      Guid workspaceId = new Guid("14d56daf-65e7-49ac-9f5f-8ac649d82402");
      Guid reportId = new Guid("63f0d281-e7d4-4eec-afec-08a0bd03b1b2");

      var viewModel = await powerBiServiceApi.GetReport(workspaceId, reportId);
      return View(viewModel);
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  
  }
}