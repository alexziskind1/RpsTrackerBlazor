using System;

namespace RPS.Core.Models.Dto
{
    public class PtDashboardFilter
    {
        public int UserId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }

}
