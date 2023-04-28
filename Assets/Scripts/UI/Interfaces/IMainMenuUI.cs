using System;

namespace Employees.UI.Interfaces
{
    public interface IMainMenuUI : IViewUI
    {
        event Action PositionsSelected;
        event Action SenioritiesSelected;
        event Action EmployeesSelected;
    }
}