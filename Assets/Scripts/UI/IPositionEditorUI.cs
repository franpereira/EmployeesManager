using System;

namespace Employees.UI
{
    public interface IPositionEditorUI
    {
        public event Action SavePositionSelected;
        
        public string Name { get; set; }

        void ShowUI();
        void HideUI();
    }
}