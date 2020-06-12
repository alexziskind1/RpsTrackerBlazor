using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration.UserSecrets;
using RPS.Core.Models.Dto;
using RPS.Data;

namespace RPS.Web.Server.Pages
{
    public partial class Dashboard : ComponentBase
    {
        [Inject]
        IPtDashboardRepository RpsDashRepo { get; set; }



        [Parameter]
        public int? Months { get; set; }

        [Parameter]
        public int? UserId { get; set; }


        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        public int IssueCountOpen { get; set; }
        public int IssueCountClosed { get; set; }

        public int IssueCountActive { get { return IssueCountOpen + IssueCountClosed; } }
        public decimal IssueCloseRate { get { if (IssueCountActive == 0) return 0;  return Math.Round((decimal)IssueCountClosed / (decimal)IssueCountActive * 100m, 2); } }




        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Refresh();
        }


        private void Refresh()
        {
            DateTime start = Months.HasValue ? DateTime.Now.AddMonths(Months.Value * -1) : DateTime.Now.AddYears(-5);
            DateTime end = DateTime.Now;

            PtDashboardFilter filter = new PtDashboardFilter
            {
                DateStart = start,
                DateEnd = end,
                UserId = UserId.HasValue ? UserId.Value : 0
            };

            
            var statusCounts = RpsDashRepo.GetStatusCounts(filter);
            IssueCountOpen = statusCounts.OpenItemsCount;
            IssueCountClosed = statusCounts.ClosedItemsCount;

            if (Months.HasValue)
            {
                DateStart = filter.DateStart;
                DateEnd = filter.DateEnd;
            }
        }
    }
}