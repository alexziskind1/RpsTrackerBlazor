using RPS.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RPS.Web.Server.Models.Forms
{
    public class CreateFormModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public ItemTypeEnum ItemType { get; set; }

        public readonly List<ItemTypeEnum> ItemTypes = new List<ItemTypeEnum> { ItemTypeEnum.Bug, ItemTypeEnum.Chore, ItemTypeEnum.Impediment, ItemTypeEnum.PBI };

    }
}
