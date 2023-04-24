namespace Employees.Model.Sql
{
    public static class DbIdentifiers
    {
        // Position
        public const string POSITION_TABLE = "Position";
        public const string POSITION_ID = "id";
        public const string POSITION_NAME = "name";

        // Seniority
        public const string SENIORITY_TABLE = "Seniority";
        public const string SENIORITY_ID = "id";
        public const string SENIORITY_POSITION_ID = "position_id";
        public const string SENIORITY_NAME = "name";
        public const string SENIORITY_ORDINAL = "ordinal";
        public const string SENIORITY_BASE_SALARY = "base_salary";
        public const string SENIORITY_PERCENTAGE_PER_INCREMENT = "percentage_per_increment";
        public const string SENIORITY_CURRENT_INCREMENTS = "current_increments";

        // Employee
        public const string EMPLOYEE_TABLE = "Employee";
        public const string EMPLOYEE_ID = "id";
        public const string EMPLOYEE_SENIORITY_ID = "seniority_id";
        public const string EMPLOYEE_FIRST_NAME = "first_name";
        public const string EMPLOYEE_LAST_NAME = "last_name";
    }
}