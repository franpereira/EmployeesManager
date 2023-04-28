using System;
using TMPro;
using UnityEngine;

namespace Employees.UI.Unity.Seniorities
{
    public class SeniorityRow : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI positionNameText;
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI employeesCountText;
        [SerializeField] TextMeshProUGUI baseSalaryText;
        [SerializeField] TextMeshProUGUI percentagePerIncrementText;
        [SerializeField] TextMeshProUGUI currentIncrementsText;
        [SerializeField] TextMeshProUGUI salaryText;

        public event Action<int> EmployeesButtonClicked;
        
        public int Id { get; set; }
        
        public string PositionName
        {
            get => positionNameText.text;
            set => positionNameText.text = value;
        }

        public string Name
        {
            get => nameText.text;
            set => nameText.text = value;
        }

        public int EmployeesCount
        {
            get => int.Parse(employeesCountText.text);
            set => employeesCountText.text = value.ToString();
        }

        public double BaseSalary
        {
            get => double.Parse(baseSalaryText.text);
            set => baseSalaryText.text = value.ToString("C");
        }

        public double PercentagePerIncrement
        {
            get => double.Parse(percentagePerIncrementText.text);
            set => percentagePerIncrementText.text = value.ToString("P");
        }

        public int CurrentIncrements
        {
            get => int.Parse(currentIncrementsText.text);
            set => currentIncrementsText.text = value.ToString();
        }

        public double Salary
        {
            get => double.Parse(salaryText.text);
            set => salaryText.text = value.ToString("C");
        }
        
        public void OnEmployeesButtonClicked() => EmployeesButtonClicked?.Invoke(Id);
    }
}