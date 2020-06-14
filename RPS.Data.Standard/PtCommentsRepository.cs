using System;
using System.Collections.Generic;
using System.Linq;
using RPS.Core.Models;
using RPS.Core.Models.Dto;

namespace RPS.Data
{
    public class PtCommentsRepository : IPtCommentsRepository
    {
        private PtInMemoryContext context;

        public PtCommentsRepository(PtInMemoryContext context)
        {
            this.context = context;
        }

        public PtComment AddNewComment(PtNewComment newComment)
        {
            var item = context.PtItems.Single(i => i.Id == newComment.ItemId);

            var newCommentId = item.Comments.Count > 0 ? item.Comments.Max(t => t.Id) + 1 : 1;

            PtComment comment = new PtComment
            {
                Id = newCommentId,
                Title = newComment.Title,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                User = context.PtUsers.Find(u => u.Id == newComment.UserId)
            };

            item.Comments.Insert(0, comment);
            return comment;
        }

        public IEnumerable<PtComment> GetAllForItem(int itemId)
        {
            var item = context.PtItems.Single(i => i.Id == itemId);
            return item.Comments.Where(t => !t.DateDeleted.HasValue);
        }
    }
}
