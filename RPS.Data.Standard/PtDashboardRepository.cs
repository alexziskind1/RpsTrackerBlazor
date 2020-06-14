using System;
using System.Collections.Generic;
using System.Linq;
using RPS.Core.Models;
using RPS.Core.Models.Dto;
using RPS.Core.Models.Enums;
using RPS.Data.Helpers;

namespace RPS.Data
{
    public class PtDashboardRepository : IPtDashboardRepository
    {
        private PtInMemoryContext context;

        public PtDashboardRepository(PtInMemoryContext context)
        {
            this.context = context;
        }

        public PtDashboardFilteredIssues GetFilteredIssues(PtDashboardFilter filter)
        {
            var openItemSpec = new PtItemStatusSpecification(StatusEnum.Open);
            var closedItemSpec = new PtItemStatusSpecification(StatusEnum.Closed);

            var userIdSpec = new PtItemUserIdSpecification(filter.UserId);
            var dateRangeSpec = new PtItemDateRangeSpecification(filter.DateStart, filter.DateEnd);

            //var items = Find(userIdSpec.And(dateRangeSpec));

            var itemsForUserAndDates = context.PtItems
                                           .Where(userIdSpec.ToExpression().Compile())
                                           .Where(dateRangeSpec.ToExpression().Compile());

            var minDate = itemsForUserAndDates.Min(i => i.DateCreated);
            var maxDate = itemsForUserAndDates.Max(i => i.DateCreated);

            var categories = GetDates(minDate, maxDate);

            var itemsByMonth = categories.Select(c => {
                return itemsForUserAndDates.Where(i => {
                    var dc = i.DateCreated;
                    return dc.Month == c.Month && dc.Year == c.Year;
                });
            });


            var categorizedAndDivided = itemsByMonth.Select(c => {
                var openItemsForMonth = c.Where(openItemSpec.ToExpression().Compile()).ToList();
                var closedItemsForMonth = c.Where(closedItemSpec.ToExpression().Compile()).ToList();

                return new ItemsForMonth
                {
                    Open = openItemsForMonth,
                    Closed = closedItemsForMonth
                };
            });

            var issues = new PtDashboardFilteredIssues
            {
                Categories = categories,
                MonthItems = categorizedAndDivided.ToList()
            };

            return issues;
        }

        public PtDashboardStatusCounts GetStatusCounts(PtDashboardFilter filter)
        {
            var openItemSpec = new PtItemStatusSpecification(StatusEnum.Open);
            var closedItemSpec = new PtItemStatusSpecification(StatusEnum.Closed);
            var userIdSpec = new PtItemUserIdSpecification(filter.UserId);
            var dateRangeSpec = new PtItemDateRangeSpecification(filter.DateStart, filter.DateEnd);


            var itemsForUserAndDates = context.PtItems
                                           .Where(userIdSpec.ToExpression().Compile())
                                           .Where(dateRangeSpec.ToExpression().Compile());

            //var openItems = Find(openItemSpec.And(userIdSpec).And(dateRangeSpec)).ToList();
            //var closedItems = Find(closedItemSpec.And(userIdSpec).And(dateRangeSpec)).ToList();
            var openItems = itemsForUserAndDates.Where(openItemSpec.ToExpression().Compile()).ToList();

            var closedItems = itemsForUserAndDates.Where(closedItemSpec.ToExpression().Compile()).ToList();

            return new PtDashboardStatusCounts
            {
                OpenItemsCount = openItems.Count,
                ClosedItemsCount = closedItems.Count
            };
        }


        private IReadOnlyList<PtItem> Find(Specification<PtItem> specification)
        {
            return context.PtItems.Where(specification.ToExpression().Compile()).ToList();
        }

        private List<DateTime> GetDates(DateTime min, DateTime max)
        {
            List<DateTime> months = new List<DateTime>();
            while (min <= max)
            {
                months.Add(new DateTime(min.Year, min.Month, 1));
                min = min.AddMonths(1);
            }
            return months;
        }
    }
}

