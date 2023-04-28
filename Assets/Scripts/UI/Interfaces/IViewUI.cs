using System;

namespace Employees.UI.Interfaces
{
    public interface IViewUI
    {
        event Action BackRequested;
        void RequestBack();
        void ShowUI();
        void HideUI();
    }
}