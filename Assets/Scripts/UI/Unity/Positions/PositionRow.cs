using System;
using TMPro;
using UnityEngine;

namespace Employees.UI.Unity.Positions
{
    public class PositionRow : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI seniorityCountText;
        [SerializeField] TextMeshProUGUI employeesCountText;

        int _positionId;
        
        public event Action<int> EditButtonClicked;
        public event Action<int> SenioritiesButtonClicked; 
        public event Action<int> EmployeesButtonClicked;
        
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
        public void OnSenioritiesButtonClicked() => SenioritiesButtonClicked?.Invoke(Id);
        public void OnEmployeesButtonClicked() => EmployeesButtonClicked?.Invoke(Id);
    }
}