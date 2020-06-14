using RPS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPS.Web.Server.Models.Forms
{
    public class TasksFormModel
    {

        public int ItemId { get; set; }
        public string NewTaskTitle { get; set; }

        public List<PtTask> Tasks = new List<PtTask>();


    }
}
