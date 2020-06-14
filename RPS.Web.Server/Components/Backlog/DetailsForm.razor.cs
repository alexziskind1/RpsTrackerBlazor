using Microsoft.AspNetCore.Components;
using RPS.Core.Models;
using RPS.Core.Models.Dto;
using RPS.Data;
using RPS.Web.Server.Models.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPS.Web.Server.Components.Backlog
{
    public partial class DetailsForm : ComponentBase
    {
        [Parameter]
        public PtItem Item { get; set; }

        public DetailsFormModel Model = new DetailsFormModel();

        [Inject]
        private IPtItemsRepository RpsItemsRepo { get; set; }

        [Inject]
        private IPtUserRepository RpsUsersRepo { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            
            PopulateDetailsFormModel();
            base.OnInitialized();
        }

        private void PopulateDetailsFormModel()
        {
            var users = RpsUsersRepo.GetAll();

            Model.Id = Item.Id;
            Model.Title = Item.Title;
            Model.Description = Item.Description;
            Model.Estimate = Item.Estimate;
            Model.SelectedItemType = Item.Type;
            Model.SelectedStatus = Item.Status;
            Model.SelectedPriority = Item.Priority;
            Model.SelectedAssigneeId = Item.Assignee.Id.ToString();
            Model.Users = users.ToList();
        }

        private void HandleValidSubmit()
        {
            var updateItem = ToPtUpdateItem();
            RpsItemsRepo.UpdateItem(updateItem);
             NavigationManager.NavigateTo("/backlog");
        }

        private PtUpdateItem ToPtUpdateItem()
        {
            return new PtUpdateItem
            {
                Id = Model.Id,
                Title = Model.Title,
                Description = Model.Description,
                Estimate = Model.Estimate,
                Priority = Model.SelectedPriority,
                Status = Model.SelectedStatus,
                Type = Model.SelectedItemType,
                AssigneeId = Int32.Parse(Model.SelectedAssigneeId)
            };
        }

    }
}
