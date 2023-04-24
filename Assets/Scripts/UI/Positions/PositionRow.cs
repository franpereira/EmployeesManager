using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Employees.UI.Positions
{
    public class PositionRow : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI seniorityCountText;
        [SerializeField] TextMeshProUGUI employeesCountText;
        [SerializeField] Button editButton;

        int _positionId;
        
        public event Action<int> EditButtonClicked;
        
        public int Id { get; set; }
        
        public string Name
        {
            get => nameText.text;
            set => nameText.text = value;
        }
        
        public int SeniorityCount
        {
            get => int.Parse(seniorityCountText.text);
            set => seniorityCountText.text = value.ToString();
        }
        
        public int EmployeesCount
        {
            get => int.Parse(employeesCountText.text);
            set => employeesCountText.text = value.ToString();
        }

        void Start() => editButton.onClick.AddListener(OnEditButtonClicked);

        void OnEditButtonClicked() => EditButtonClicked?.Invoke(Id);
    }
}