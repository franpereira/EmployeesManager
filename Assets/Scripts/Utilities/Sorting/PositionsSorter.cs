using System;
using System.Collections.Generic;
using Employees.Model;

namespace Employees.Utilities.Sorting
{
    public class PositionsSorter
    {
        public IEnumerable<Position> SortPositions(IEnumerable<Position> positions, PositionsSortType type,
            bool ascending = true)
        {
            List<Position> results = new(positions);
            int sortOrder = ascending ? 1 : -1;

            switch (type)
            {
                case PositionsSortType.Default:
                    results.Sort((a, b) => a.Id.CompareTo(b.Id) * sortOrder);
                    break;
                case PositionsSortType.Name:
                    results.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal) * sortOrder);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "PositionsSortType not implemented");
            }

            return results;
        }
    }
}