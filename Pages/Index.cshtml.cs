using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DotNetCoreRazor_MSGraph.Graph;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Identity.Web;

namespace DotNetCoreRazor_MSGraph.Pages
{
    [AuthorizeForScopes(ScopeKeySection = "DownstreamApi:Scopes")]
    public class IndexModel : PageModel
    {
        private readonly GraphProfileClient _graphProfileClient;
        public string UserDisplayName { get; private set; } = "";
        public string UserPhoto { get; private set; }
        readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, GraphProfileClient graphProfileClient)
        {
            _logger = logger;
            _graphProfileClient = graphProfileClient;            
        }

        public async Task OnGetAsync()
        {
            var user = await _graphProfileClient.GetUserProfile(); 
            UserDisplayName = user.DisplayName.Split(' ')[0];
            UserPhoto = await _graphProfileClient.GetUserProfileImage();            
        }
    }
}
