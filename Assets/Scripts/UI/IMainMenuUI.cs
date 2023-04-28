using System;

namespace Employees.UI
{
    public interface IMainMenuUI
    {
        event Action PositionsSelected;
        event Action SenioritiesSelected;
        event Action EmployeesSelected;
    }
}