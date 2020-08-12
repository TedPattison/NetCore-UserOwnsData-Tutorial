$(function(){

	// get DOM object div for report container
	var reportContainer = document.getElementById("embed-container");
  
	var reportId = window.viewModel.reportId;
	var embedUrl = window.viewModel.embedUrl;
	var token = window.viewModel.token
  
	var models = window['powerbi-client'].models;
	
	var config = {
	  type: 'report',
	  id: reportId,
	  embedUrl: embedUrl,
	  accessToken: token,
	  permissions: models.Permissions.All,
	  tokenType: models.TokenType.Aad,
	  viewMode: models.ViewMode.View,
	  settings: {
		panes: {
		  filters: { expanded: false, visible: true },
		  pageNavigation: { visible: false }
		}
	  }
	};
  
	// Embed the report and display it within the div container.
	var report = powerbi.embed(reportContainer, config);
  
	// add logic to resize embed container element on window rersize event
	var heightBuffer = 12;
	var newHeight = $(window).height() - ($("header").height() + heightBuffer);
	$("#embed-container").height(newHeight);
	$(window).resize(function () {
	  var newHeight = $(window).height() - ($("header").height() + heightBuffer);
	  $("#embed-container").height(newHeight);
	});
  
});