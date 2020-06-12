using RPS.Core.Models;
using RPS.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RPS.Data.Helpers
{
    public class PtItemStatusSpecification : Specification<PtItem>
    {
        private readonly StatusEnum _status;

        public PtItemStatusSpecification(StatusEnum status)
        {
            _status = status;
        }

        public override Expression<Func<PtItem, bool>> ToExpression()
        {
            return item => item.Status == _status;
        }

    }
}
