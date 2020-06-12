using RPS.Core.Models.Enums;

namespace RPS.Core.Models.Dto
{
    public class PtUpdateItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Estimate { get; set; }
        public PriorityEnum Priority { get; set; }
        public StatusEnum Status { get; set; }
        public ItemTypeEnum Type { get; set; }
        public int AssigneeId { get; set; }
    }
}
