using RPS.Core.Models;
using System.Collections.Generic;

namespace RPS.Data
{
    public interface IPtUserRepository
    {
        IEnumerable<PtUser> GetAll();
    }
}
