using System;
using UnityEngine;

namespace Employees.UI
{
    public class MainMenuUI : MonoBehaviour, IMainMenuUI
    {
        public event Action PositionsSelected;

        public void OnPositionsSelected() => PositionsSelected?.Invoke();
    }
}