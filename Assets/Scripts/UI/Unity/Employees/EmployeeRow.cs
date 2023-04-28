using TMPro;
using UnityEngine;

namespace Employees.UI.Unity.Employees
{
    public class EmployeeRow : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI firstNameText;
        [SerializeField] TextMeshProUGUI lastNameText;
        [SerializeField] TextMeshProUGUI seniorityText;
        [SerializeField] TextMeshProUGUI positionText;
        [SerializeField] TextMeshProUGUI salaryText;
        
        public string FirstName
        {
            get => firstNameText.text;
            set => firstNameText.text = value;
        }
        
        public string LastName
        {
            get => lastNameText.text;
            set => lastNameText.text = value;
        }
        
        public string Seniority
        {
            get => seniorityText.text;
            set => seniorityText.text = value;
        }
        
        public string Position
        {
            get => positionText.text;
            set => positionText.text = value;
        }
        
        public double Salary
        {
            get => double.Parse(salaryText.text);
            set => salaryText.text = value.ToString("C");
        }
    }
}