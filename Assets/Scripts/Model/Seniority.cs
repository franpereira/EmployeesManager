namespace Employees.Model
{
    public class Seniority
    {
        public int Id { get; set; }
        public Position Position { get; set; }
        public string Name { get; set; }
        public int Ordinal { get; set; }
        public double BaseSalary { get; set; }
        public double PercentagePerIncrement { get; set; }
        public int CurrentIncrements { get; set; }
        
        public double Salary => BaseSalary + (BaseSalary * PercentagePerIncrement / 100) * CurrentIncrements;

    }
}