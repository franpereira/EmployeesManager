using System;

namespace Employees.UI.Positions
{
    public interface IPositionsUI
    {
        public event Action<int> EditPositionRequested;

        public void ShowUI();
        public void HideUI();
        public void AddPosition(string positionName, int seniorityCount, int employeesCount);
        public void Clear();
    }
}