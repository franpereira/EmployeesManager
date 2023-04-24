using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Employees.UI.Positions
{
    public class PositionEditorUI : MonoBehaviour, IPositionEditorUI
    {
        [SerializeField] TMP_InputField nameInputField;
        [SerializeField] Button saveButton;

        public event Action SavePositionSelected;

        public string Name
        {
            get => nameInputField.text;
            set => nameInputField.text = value;
        }

        void Start() => saveButton.onClick.AddListener(OnSavePositionSelected);

        public void ShowUI() => gameObject.SetActive(true);
        public void HideUI() => gameObject.SetActive(false);
        
        public void OnSavePositionSelected() => SavePositionSelected?.Invoke();
    }
}