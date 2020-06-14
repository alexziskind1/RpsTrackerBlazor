using Microsoft.AspNetCore.Components;
using RPS.Core.Models.Dto;
using RPS.Core.Models.Enums;
using RPS.Data;
using RPS.Web.Server.Models.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPS.Web.Server.Pages
{
    public partial class Create : ComponentBase
    {
        private const int CURRENT_USER_ID = 21; //Fake user id for demo

        public CreateFormModel FormModel = new CreateFormModel();

        [Inject]
        private IPtItemsRepository RpsItemsRepo { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private void HandleValidSubmit()
        {
            var newItem = ToPtNewItem();
            RpsItemsRepo.AddNewItem(newItem);
            NavigationManager.NavigateTo("/backlog");
        }

        private PtNewItem ToPtNewItem()
        {
            return new PtNewItem
            {
                Title = FormModel.Title,
                Description = FormModel.Description,
                TypeStr = FormModel.ItemType,
                UserId = CURRENT_USER_ID
            };
        }
    }
}
