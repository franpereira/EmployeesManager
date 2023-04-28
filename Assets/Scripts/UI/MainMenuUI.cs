using System;
using UnityEngine;

namespace Employees.UI
{
    public class MainMenuUI : MonoBehaviour, IMainMenuUI
    {
        public event Action PositionsSelected;
        public event Action SenioritiesSelected;
        public event Action EmployeesSelected;

        public void OnPositionsSelected() => PositionsSelected?.Invoke();
        public void OnSenioritiesSelected() => SenioritiesSelected?.Invoke();
        public void OnEmployeesSelected() => EmployeesSelected?.Invoke();
    }
}