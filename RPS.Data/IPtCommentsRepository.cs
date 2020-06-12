using RPS.Core.Models;
using RPS.Core.Models.Dto;
using System.Collections.Generic;

namespace RPS.Data
{
    public interface IPtCommentsRepository
    {
        IEnumerable<PtComment> GetAllForItem(int itemId);
        PtComment AddNewComment(PtNewComment newComment);
    }
}
