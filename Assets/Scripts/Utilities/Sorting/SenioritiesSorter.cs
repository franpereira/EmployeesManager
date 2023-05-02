using System;
using System.Collections.Generic;
using Employees.Model;

namespace Employees.Utilities.Sorting
{
    public class SenioritiesSorter
    {
        public IEnumerable<Seniority> SortSeniorities(IEnumerable<Seniority> seniorities, SenioritiesSortType type,
            bool ascending = true)
        {
            List<Seniority> results = new(seniorities);
            int sortOrder = ascending ? 1 : -1;

            switch (type)
            {
                case SenioritiesSortType.Default:
                    results.Sort((a, b) => a.Id.CompareTo(b.Id) * sortOrder);
                    break;
                case SenioritiesSortType.Position:
                    results.Sort((a, b) => string.Compare(a.Position.Name, b.Position.Name, StringComparison.Ordinal) * sortOrder);
                    break;
                case SenioritiesSortType.Name:
                    results.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal) * sortOrder);
                    break;
                case SenioritiesSortType.Ordinal:
                    results.Sort((a, b) => a.Ordinal.CompareTo(b.Ordinal) * sortOrder);
                    break;
                case SenioritiesSortType.BaseSalary:
                    results.Sort((a, b) => a.BaseSalary.CompareTo(b.BaseSalary) * sortOrder);
                    break;
                case SenioritiesSortType.PercentagePerIncrement:
                    results.Sort((a, b) => a.PercentagePerIncrement.CompareTo(b.PercentagePerIncrement) * sortOrder);
                    break;
                case SenioritiesSortType.CurrentIncrements:
                    results.Sort((a, b) => a.CurrentIncrements.CompareTo(b.CurrentIncrements) * sortOrder);
                    break;
                case SenioritiesSortType.Salary:
                    results.Sort((a, b) => a.Salary.CompareTo(b.Salary) * sortOrder);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "SenioritiesSortType not implemented");
            }

            return results;
        }
    }
}