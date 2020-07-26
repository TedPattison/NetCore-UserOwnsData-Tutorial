using System;
using System.Threading.Tasks;
using Microsoft.Identity.Web;
using Microsoft.Rest;
using Microsoft.PowerBI.Api;

namespace UserOwnsData.Services {

	public class EmbeddedReportViewModel {
		public string Id;
		public string Name;
		public string EmbedUrl;
		public string Token;
	}

	public class PowerBiServiceApi {

		readonly ITokenAcquisition tokenAcquisition;

		public PowerBiServiceApi(ITokenAcquisition tokenAcquisition) {
			this.tokenAcquisition = tokenAcquisition;
		}

		const string urlPowerBiServiceApiRoot = "https://api.powerbi.com/";

		public static readonly string[] RequiredScopes =
		  new string[] {
			"https://analysis.windows.net/powerbi/api/Group.Read.All",
			"https://analysis.windows.net/powerbi/api/Report.ReadWrite.All",
			"https://analysis.windows.net/powerbi/api/Dataset.ReadWrite.All",
			"https://analysis.windows.net/powerbi/api/Content.Create",
            "https://analysis.windows.net/powerbi/api/Workspace.ReadWrite.All"
		};


		public string GetAccessToken() {
			return this.tokenAcquisition.GetAccessTokenForUserAsync(RequiredScopes).Result;
		}

		public PowerBIClient GetPowerBiClient()	{
			var tokenCredentials = new TokenCredentials(GetAccessToken(), "Bearer");
			return new PowerBIClient(new Uri(urlPowerBiServiceApiRoot), tokenCredentials);
		}

		public async Task<EmbeddedReportViewModel> GetReport(Guid WorkspaceId, Guid ReportId) {
			PowerBIClient pbiClient = GetPowerBiClient();
			// call to Power BI Service API to get embedding data
			var report = await pbiClient.Reports.GetReportInGroupAsync(WorkspaceId, ReportId);
			// return report embedding data to caller
			return new EmbeddedReportViewModel {
				Id = report.Id.ToString(),
				EmbedUrl = report.EmbedUrl,
				Name = report.Name,
				Token = GetAccessToken()
			};
		}
	
	}

}