using System;
using Employees.UI.Interfaces;
using UnityEngine;

namespace Employees.UI.Unity
{
    public class MainMenuUI : ViewUI, IMainMenuUI
    {
        public event Action PositionsSelected;
        public event Action SenioritiesSelected;
        public event Action EmployeesSelected;

        public void OnPositionsSelected() => PositionsSelected?.Invoke();
        public void OnSenioritiesSelected() => SenioritiesSelected?.Invoke();
        public void OnEmployeesSelected() => EmployeesSelected?.Invoke();
    }
}