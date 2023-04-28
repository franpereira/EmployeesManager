using System;
using Employees.UI.Interfaces;
using UnityEngine;

namespace Employees.UI.Unity
{
    public abstract class ViewUI : MonoBehaviour, IViewUI
    {
        public event Action BackRequested;
        public void RequestBack() => BackRequested?.Invoke();
        public void ShowUI() => gameObject.SetActive(true);
        public void HideUI() => gameObject.SetActive(false);
    }
}