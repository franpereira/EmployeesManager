using Employees.Presenters.Employees;
using Employees.Presenters.Positions;
using Employees.Presenters.Seniorities;
using Employees.UI;
using Employees.UI.Positions;

namespace Employees.Presenters
{
    public class MainMenuPresenter
    {
        readonly IMainMenuUI _ui;
        readonly PositionsPresenter _positionsPresenter;
        readonly SenioritiesPresenter _senioritiesPresenter;
        readonly EmployeesPresenter _employeesPresenter;

        public MainMenuPresenter(IMainMenuUI ui, PositionsPresenter positionsPresenter, SenioritiesPresenter senioritiesPresenter, EmployeesPresenter employeesPresenter)
        {
            _ui = ui;
            _ui.PositionsSelected += OnPositionsSelected;
            _ui.SenioritiesSelected += OnSenioritiesSelected;
            _ui.EmployeesSelected += OnEmployeesSelected;
            _positionsPresenter = positionsPresenter;
            _senioritiesPresenter = senioritiesPresenter;
            _employeesPresenter = employeesPresenter;
        }

        void OnPositionsSelected() => _positionsPresenter.LoadAllPositions();
        void OnSenioritiesSelected() => _senioritiesPresenter.LoadAllSeniorities();
        void OnEmployeesSelected() => _employeesPresenter.LoadAllEmployees();
    }
}