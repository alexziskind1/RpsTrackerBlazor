using System;

namespace RPS.Core.Models
{
    public class PtTask:PtObjectBase
    {
        public bool Completed { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
