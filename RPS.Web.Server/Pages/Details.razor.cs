using Microsoft.AspNetCore.Components;
using RPS.Core.Models;
using RPS.Data;
using RPS.Web.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RPS.Web.Server.Pages
{
    public partial class Details : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public string ScreenParam { get; set; } = "Details";

            public DetailScreenEnum Screen { get {
                return Enum.Parse<DetailScreenEnum>(ScreenParam, true);
                    } }

        public PtItem Item { get; set; }

        [Inject]
        private IPtItemsRepository RpsItemsRepo { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Refresh();
        }

        private void Refresh()
        {
            Item = RpsItemsRepo.GetItemById(Id);
        }
    }
}
