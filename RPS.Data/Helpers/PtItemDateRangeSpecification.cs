using RPS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RPS.Data.Helpers
{
    public class PtItemDateRangeSpecification : Specification<PtItem>
    {
        private readonly DateTime _start;
        private readonly DateTime _end;

        public PtItemDateRangeSpecification(DateTime start, DateTime end)
        {
            _start = start;
            _end = end;
        }

        public override Expression<Func<PtItem, bool>> ToExpression()
        {
            return item => item.DateCreated >= _start && item.DateCreated <= _end;
        }

    }
}
