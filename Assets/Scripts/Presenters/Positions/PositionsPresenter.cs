using System.Linq;
using Employees.Model.DataAccess;
using Employees.Presenters.Employees;
using Employees.Presenters.Seniorities;
using Employees.UI.Interfaces.Positions;

namespace Employees.Presenters.Positions
{
    public class PositionsPresenter : INavigable
    {
        readonly IPositionsUI _ui;
        readonly PositionEditorPresenter _editorPresenter;
        readonly IDataRepository _repository;
        readonly SenioritiesPresenter _senioritiesPresenter;
        readonly EmployeesPresenter _employeesPresenter;

        public PositionsPresenter(IDataRepository repository, IPositionsUI ui, PositionEditorPresenter editorPresenter, SenioritiesPresenter senioritiesPresenter, EmployeesPresenter employeesPresenter)
        {
            _repository = repository;
            _ui = ui;
            _ui.BackRequested += ViewsNavigation.NavigateBack;
            _ui.EditPositionRequested += OnEditPositionRequested;
            _ui.SenioritiesRequested += OnSenioritiesRequested;
            _ui.EmployeesRequested += OnEmployeesRequested;
            _editorPresenter = editorPresenter;
            _editorPresenter.DataSaved += LoadAllPositions;
            _senioritiesPresenter = senioritiesPresenter;
            _employeesPresenter = employeesPresenter;
        }
        
        public void ShowUI() => _ui.ShowUI();
        public void HideUI() => _ui.HideUI();
        
        void OnEditPositionRequested(int id) => _editorPresenter.LoadPosition(id);
        
        void OnSenioritiesRequested(int id) => _senioritiesPresenter.LoadSenioritiesByPosition(id);

        void OnEmployeesRequested(int id) => _employeesPresenter.LoadEmployeesByPosition(id);

        public void LoadAllPositions()
        {
            _ui.Clear();
            var positions = _repository.Positions.GetAll();
            foreach (var position in positions)
            {
                int seniorityCount = _repository.Seniorities.GetByPosition(position).Count();
                int employeesCount = _repository.Employees.GetByPosition(position).Count();
                _ui.AddPosition(position.Id, position.Name, seniorityCount, employeesCount);
            }
            ViewsNavigation.NavigateTo(this);
        }
    }
}