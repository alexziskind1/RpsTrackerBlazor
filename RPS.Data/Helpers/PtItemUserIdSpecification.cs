using RPS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RPS.Data.Helpers
{
    public class PtItemUserIdSpecification : Specification<PtItem>
    {
        private readonly int _userId;

        public PtItemUserIdSpecification(int userId)
        {
            _userId = userId;
        }

        public override Expression<Func<PtItem, bool>> ToExpression()
        {
            if (_userId == 0)
            {
                return item => true;
            }
            else
            {
                return item => item.Assignee.Id == _userId;
            }
        }

    }
}
