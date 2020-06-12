using RPS.Core.Models.Enums;

namespace RPS.Core.Models.Dto
{
    public class PtNewItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ItemTypeEnum TypeStr { get; set; }
        public int UserId { get; set; }
    }
}
