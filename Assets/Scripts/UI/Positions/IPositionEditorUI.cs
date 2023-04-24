using System;

namespace Employees.UI.Positions
{
    public interface IPositionEditorUI
    {
        public event Action SavePositionSelected;
        
        public string Name { get; set; }
        
        public void ShowUI();
        public void HideUI();
    }
}