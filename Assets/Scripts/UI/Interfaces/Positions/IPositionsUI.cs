using System;

namespace Employees.UI.Interfaces.Positions
{
    public interface IPositionsUI : IViewUI
    {
        public event Action<int> EditPositionRequested;
        public event Action<int> SenioritiesRequested;
        public event Action<int> EmployeesRequested;
        public void AddPosition(int id, string positionName, int seniorityCount, int employeesCount);
        public void Clear();
    }
}