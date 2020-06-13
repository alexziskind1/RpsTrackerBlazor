using Microsoft.AspNetCore.Components;
using RPS.Core.Models;
using RPS.Data;
using RPS.Web.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPS.Web.Server.Components.Backlog
{
    public partial class Items : ComponentBase
    {
        private const int CURRENT_USER_ID = 21; //Fake user id for demo


        [Inject]
        IPtItemsRepository RpsItemsRepo { get; set; }

        [Parameter]
        public string PresetParam { get; set; }

        public PresetEnum Preset { get {
                if (string.IsNullOrEmpty(PresetParam))
                {
                    return PresetEnum.Open;
                } 
                else
                {
                    return Enum.Parse<PresetEnum>(PresetParam, true);
                }
            }
        }

        public List<PtItem> PtItems { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Refresh();
        }

        public void Refresh()
        {
            IEnumerable<PtItem> items = null;
            switch (Preset)
            {
                case PresetEnum.My:
                    items = RpsItemsRepo.GetUserItems(CURRENT_USER_ID);
                    break;
                case PresetEnum.Open:
                    items = RpsItemsRepo.GetOpenItems();
                    break;
                case PresetEnum.Closed:
                    items = RpsItemsRepo.GetClosedItems();
                    break;
                default:
                    items = RpsItemsRepo.GetOpenItems();
                    break;
            }
            PtItems = items.ToList();
        }
    }
}
