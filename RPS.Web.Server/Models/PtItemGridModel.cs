using RPS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPS.Web.Server.Models
{
    public class PtItemGridModel
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public DateTime DateCreated { get; private set; }

        public int Estimate { get; private set; }
        public string Priority { get; private set; }
        public string Status { get; private set; }
        public string Type { get; private set; }
        public string AssigneeAvatar { get; private set; }
        public string AssigneeFullName { get; private set; }

        public PtItemGridModel(PtItem item)
        {
            Id = item.Id;
            Title = item.Title;
            DateCreated = item.DateCreated;

            Estimate = item.Estimate;
            Priority = item.Priority.ToString();
            Status = item.Status.ToString();
            Type = item.Type.ToString();
            AssigneeAvatar = item.Assignee.Avatar;
            AssigneeFullName = item.Assignee.FullName;
        }
    }

}
