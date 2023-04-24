using System.IO;
using Employees.Model;
using Employees.Model.Sql;
using Employees.Model.Sql.DataAccess;
using Mono.Data.Sqlite;
using UnityEngine;
using static Employees.Model.Sql.DbIdentifiers;

namespace Employees.Services.Database
{
    public class SqliteCreator
    {
        SqliteDatabase _db;
        SqlExecutor _executor;
        
        public SqliteDatabase Create(string filePath, bool forceNew = false)
        {
            if (forceNew is false && File.Exists(filePath))
            {
                Debug.Log("Using already existing database file");
                return new SqliteDatabase(filePath);
            }

            Debug.Log("Creating new database file...");
            SqliteConnection.CreateFile(filePath);
            _db = new SqliteDatabase(filePath);
            
            Debug.Log("Populating database with example data...");
            _executor = new SqlExecutor(_db.NewConnection);
            CreateTables();
            PopulateTables();
            Debug.Log("Database created successfully");
            return _db;
        }

        void CreateTables()
        {
            // Create position table:
            _executor.Execute(
                $"CREATE TABLE IF NOT EXISTS {POSITION_TABLE} (" +
                $"{POSITION_ID} INTEGER PRIMARY KEY AUTOINCREMENT, " +
                $"{POSITION_NAME} TEXT NOT NULL UNIQUE" +
                ")"
            );

            // Create seniority table:
            _executor.Execute(
                $"CREATE TABLE IF NOT EXISTS {SENIORITY_TABLE} (" +
                $"{SENIORITY_ID} INTEGER PRIMARY KEY AUTOINCREMENT, " +
                $"{SENIORITY_POSITION_ID} INTEGER NOT NULL, " +
                $"{SENIORITY_NAME} TEXT NOT NULL, " +
                $"{SENIORITY_ORDINAL} INTEGER NOT NULL, " +
                $"{SENIORITY_BASE_SALARY} REAL NOT NULL, " +
                $"{SENIORITY_PERCENTAGE_PER_INCREMENT} REAL NOT NULL, " +
                $"{SENIORITY_CURRENT_INCREMENTS} INTEGER NOT NULL, " +
                $"FOREIGN KEY ({SENIORITY_POSITION_ID}) REFERENCES {POSITION_TABLE} ({POSITION_ID})" +
                ")"
            );

            // Create employee table:
            _executor.Execute(
                $"CREATE TABLE IF NOT EXISTS {EMPLOYEE_TABLE} (" +
                $"{EMPLOYEE_ID} INTEGER PRIMARY KEY AUTOINCREMENT, " +
                $"{EMPLOYEE_SENIORITY_ID} INTEGER NOT NULL, " +
                $"{EMPLOYEE_FIRST_NAME} TEXT NOT NULL, " +
                $"{EMPLOYEE_LAST_NAME} TEXT NOT NULL, " +
                $"FOREIGN KEY ({EMPLOYEE_SENIORITY_ID}) REFERENCES {SENIORITY_TABLE} ({SENIORITY_ID})" +
                ")"
            );
        }

        void PopulateTables()
        {
            SqlPositionDA positionDA = new(_executor);
            SqlSeniorityDA seniorityDA = new(_executor, positionDA);
            SqlEmployeeDA employeeDA = new(_executor, seniorityDA);

            // Load positions from example csv:
            string positionsCsv = Resources.Load<TextAsset>("example_positions").text;
            string[] lines = positionsCsv.Split('\n');
            foreach (string line in lines)
            {
                string[] fields = line.Split(',');
                positionDA.Add(new Position
                {
                    Id = int.Parse(fields[0]),
                    Name = fields[1],
                });
            }
            
            // Seniorities:
            string senioritiesCsv = Resources.Load<TextAsset>("example_seniorities").text;
            lines = senioritiesCsv.Split('\n');
            foreach (string line in lines)
            {
                string[] fields = line.Split(',');
                seniorityDA.Add(new Seniority
                {
                    Id = int.Parse(fields[0]),
                    Position = positionDA.Get(int.Parse(fields[1])),
                    Name = fields[2],
                    Ordinal = int.Parse(fields[3]),
                    BaseSalary = float.Parse(fields[4]),
                    PercentagePerIncrement = float.Parse(fields[5]),
                    CurrentIncrements = int.Parse(fields[6]),
                });
            }

            // Employees:
            string employeesCsv = Resources.Load<TextAsset>("example_employees").text;
            lines = employeesCsv.Split('\n');
            foreach (string line in lines)
            {
                string[] fields = line.Split(',');
                employeeDA.Add(new Employee
                {
                    Id = int.Parse(fields[0]),
                    Seniority = seniorityDA.Get(int.Parse(fields[1])),
                    FirstName = fields[2],
                    LastName = fields[3],
                });
            }
        }
    }
}