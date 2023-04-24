using System;
using System.Collections.Generic;
using Employees.Model;

namespace Employees.UI.Positions
{
    public interface IPositionsUI
    {
        public event Action<int> EditPositionRequested;

        public void LoadPositions(IEnumerable<Position> positions);
        
        public void ShowUI();
        public void HideUI();
    }
}