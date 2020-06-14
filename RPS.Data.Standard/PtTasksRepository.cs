using System;
using System.Collections.Generic;
using System.Linq;
using RPS.Core.Models;
using RPS.Core.Models.Dto;

namespace RPS.Data
{
    public class PtTasksRepository : IPtTasksRepository
    {
        private PtInMemoryContext context;

        public PtTasksRepository(PtInMemoryContext context)
        {
            this.context = context;
        }

        public PtTask AddNewTask(PtNewTask newTask)
        {
            var item = context.PtItems.Single(i => i.Id == newTask.ItemId);

            var newTaskId = item.Tasks.Count > 0 ? item.Tasks.Max(t => t.Id) + 1 : 1;

            PtTask task = new PtTask
            {
                Id = newTaskId,
                Title = newTask.Title,
                Completed = false,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            item.Tasks.Insert(0, task);
            return task;
        }

        public bool DeleteTask(int id, int itemId)
        {
            var item = context.PtItems.SingleOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                var task = item.Tasks.Single(t => t.Id == id);
                task.DateDeleted = DateTime.Now;
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<PtTask> GetAllForItem(int itemId)
        {
            var item = context.PtItems.Single(i => i.Id == itemId);
            return item.Tasks.Where(t => !t.DateDeleted.HasValue);
        }

        public PtTask UpdateTask(PtUpdateTask updateTask)
        {
            var item = context.PtItems.Single(i => i.Id == updateTask.ItemId);
            var oldTask = item.Tasks.Single(t => t.Id == updateTask.Id);
            var idx = item.Tasks.FindIndex(t => t.Id == updateTask.Id);

            PtTask uTask = new PtTask
            {
                Id = oldTask.Id,
                Title = updateTask.Title,
                Completed = updateTask.Completed,
                DateCreated = oldTask.DateCreated,
                DateModified = DateTime.Now,
                DateEnd = oldTask.DateEnd,
                DateStart = oldTask.DateStart
            };

            item.Tasks[idx] = uTask;

            return uTask;
        }
    }
}
