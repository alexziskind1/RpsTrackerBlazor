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
    public partial class Tasks : ComponentBase
    {

        [Parameter]
        public PtItem Item { get; set; }

        public List<PtTask> TaskItems = new List<PtTask>();

        public string NewTaskTitle = string.Empty;

        [Inject]
        private IPtTasksRepository RpsTasksRepo { get; set; }

        protected override void OnInitialized()
        {
            TaskItems = Item.Tasks;
            base.OnInitialized();
        }

        public void AddTaskHandler()
        {
            if (!string.IsNullOrWhiteSpace(NewTaskTitle))
            {
                SaveTask();
                NewTaskTitle = string.Empty;
            }
        }

        private void SaveTask()
        {
            PtNewTask taskNew = new PtNewTask
            {
                ItemId = Item.Id,
                Title = NewTaskTitle
            };

             RpsTasksRepo.AddNewTask(taskNew);
        }

        public void DeleteTaskHandler(PtTask t)
        {
            RpsTasksRepo.DeleteTask(t.Id, Item.Id);
            TaskItems.Remove(t);
        }



    }
}
