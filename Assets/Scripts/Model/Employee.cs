namespace Employees.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public Seniority Seniority { get; set; }
        public Position Position => Seniority.Position;
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}