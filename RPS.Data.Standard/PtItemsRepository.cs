using RPS.Core.Models;
using RPS.Core.Models.Enums;
using System.Collections.Generic;
using System.Linq;
using RPS.Core.Models.Dto;
using System;

namespace RPS.Data
{
    public class PtItemsRepository : IPtItemsRepository
    {
        private PtInMemoryContext context;

        public PtItemsRepository(PtInMemoryContext context)
        {
            this.context = context;
        }

        public IEnumerable<PtItem> GetAll()
        {
            return context.PtItems;
        }

        public IEnumerable<PtItem> GetClosedItems()
        {
            return context.PtItems.Where(i => i.Status == StatusEnum.Closed && 
            i.DateDeleted == null);
        }

        public PtItem GetItemById(int itemId)
        {
            var item = context.PtItems.SingleOrDefault(i => i.Id == itemId);

            item.Tasks = item.Tasks.Where(t => !t.DateDeleted.HasValue).ToList();
            return item;
        }

        public IEnumerable<PtItem> GetOpenItems()
        {
            return context.PtItems.Where(i => (i.Status == StatusEnum.Open ||
                                      i.Status == StatusEnum.ReOpened) &&
                                        i.DateDeleted == null);
        }

        public IEnumerable<PtItem> GetUserItems(int userId)
        {
            return context.PtItems.Where(i => i.Assignee.Id == userId &&
                                    i.DateDeleted == null);
        }

        public PtItem AddNewItem(PtNewItem newItem)
        {
            var item1 = new PtItem
            {
                Id = context.PtItems.Max(i=>i.Id) + 1,
                Title = newItem.Title,
                Description = newItem.Description,
                Type = newItem.TypeStr,
                Assignee = context.PtUsers.Find(u=>u.Id == newItem.UserId),
                Estimate = 0,
                Priority = PriorityEnum.Medium,
                Status = StatusEnum.Open,
                Tasks = new List<PtTask>(),
                Comments = new List<PtComment>(),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            context.PtItems.Insert(0, item1);

            return item1;
        }

        public PtItem UpdateItem(PtUpdateItem updateItem)
        {
            var idx = context.PtItems.FindIndex(i => i.Id == updateItem.Id);
            var oldItem = context.PtItems.Find(i => i.Id == updateItem.Id);

            var uItem = new PtItem
            {
                Id = updateItem.Id,
                Title = updateItem.Title,
                Description = updateItem.Description,
                Type = updateItem.Type,
                Assignee = context.PtUsers.Find(u => u.Id == updateItem.AssigneeId),
                Estimate = updateItem.Estimate,
                Priority = updateItem.Priority,
                Status = updateItem.Status,
                Tasks = oldItem.Tasks,
                Comments = oldItem.Comments,
                DateCreated = oldItem.DateCreated,
                DateModified = DateTime.Now
            };

            context.PtItems[idx] = uItem;
            return uItem;
        }
    }
}
