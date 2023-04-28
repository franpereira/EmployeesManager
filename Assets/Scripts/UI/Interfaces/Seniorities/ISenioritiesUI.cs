using System;

namespace Employees.UI.Interfaces.Seniorities
{
    public interface ISenioritiesUI : IViewUI
    {
        public event Action<int> EmployeesRequested;
        
        public void AddSeniority(int id, string seniorityName, string positionName, int employeesCount, double baseSalary,
            double percentagePerIncrement, int currentIncrements, double salary);

        public void Clear();
    }
}