namespace Employees.UI.Seniorities
{
    public interface ISenioritiesUI
    {
        public void AddSeniority(string seniorityName, string positionName, int employeesCount, double baseSalary,
            double percentagePerIncrement, int currentIncrements, double salary);
        
        public void Clear();
        
        public void ShowUI();
        public void HideUI();
    }
}