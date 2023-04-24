using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Employees.UI
{
    public class PositionRow : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI idText;
        [SerializeField] Button editButton;

        public event Action<int> EditButtonClicked;
        
        public string Name
        {
            get => nameText.text;
            set => nameText.text = value;
        }
        
        public int Id
        {
            get => int.Parse(idText.text);
            set => idText.text = value.ToString();
        }
        
        void Start() => editButton.onClick.AddListener(OnEditButtonClicked);

        void OnEditButtonClicked() => EditButtonClicked?.Invoke(Id);

        
    }
}