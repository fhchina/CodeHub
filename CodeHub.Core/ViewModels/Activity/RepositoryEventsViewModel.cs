using System.Collections.Generic;
using CodeHub.Core.Services;
using GitHubSharp;
using GitHubSharp.Models;

namespace CodeHub.Core.ViewModels.Activity
{
    public class RepositoryEventsViewModel : BaseEventsViewModel
    {
        public string RepositoryName { get; set; }

        public string RepositoryOwner { get; set; }

        public RepositoryEventsViewModel(IApplicationService applicationService)
            : base(applicationService)
        {
        }

        protected override GitHubRequest<List<EventModel>> CreateRequest()
        {
			return ApplicationService.Client.Users[RepositoryOwner].Repositories[RepositoryName].GetEvents();
        }
    }
}