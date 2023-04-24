using System;
using System.Collections.Generic;
using Employees.Model;

namespace Employees.UI
{
    public interface IPositionsUI
    {
        public event Action<int> EditPositionRequested;
        
        public void ShowUI();

        public void LoadPositions(IEnumerable<Position> positions);
    }
}