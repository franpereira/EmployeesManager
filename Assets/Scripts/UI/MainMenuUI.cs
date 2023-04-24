using System;
using UnityEngine;

namespace Employees.UI
{
    public class MainMenuUI : MonoBehaviour, IMainMenuUI
    {
        public event Action PositionsSelected;
        public event Action SenioritiesSelected;

        public void OnPositionsSelected() => PositionsSelected?.Invoke();
        public void OnSenioritiesSelected() => SenioritiesSelected?.Invoke();
    }
}