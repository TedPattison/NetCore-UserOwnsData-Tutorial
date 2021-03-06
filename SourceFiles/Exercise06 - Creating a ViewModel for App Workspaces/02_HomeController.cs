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

    public async Task<IActionResult> Embed(string workspaceId) {
      var viewModel = await powerBiServiceApi.GetEmbeddedViewModel(workspaceId);
      // cast string value to object type in order to pass string value as  MVC view model 
      return View(viewModel as object);
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  
  }
}