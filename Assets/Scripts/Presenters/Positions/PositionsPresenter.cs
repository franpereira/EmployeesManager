using System.Linq;
using Employees.Model.DataAccess;
using Employees.UI.Positions;

namespace Employees.Presenters.Positions
{
    public class PositionsPresenter
    {
        readonly IPositionsUI _ui;
        readonly PositionEditorPresenter _editorPresenter;
        readonly IDataRepository _repository;

        public PositionsPresenter(IDataRepository repository, IPositionsUI ui, PositionEditorPresenter editorPresenter)
        {
            _repository = repository;
            _ui = ui;
            _ui.EditPositionRequested += OnEditPositionRequested;
            _editorPresenter = editorPresenter;
            _editorPresenter.DataSaved += LoadAllPositions;
            LoadAllPositions();
        }
        
        void OnEditPositionRequested(int id)
        {
            _editorPresenter.LoadPosition(id);
        }
        
        public void LoadAllPositions()
        {
            _ui.Clear();
            var positions = _repository.Positions.GetAll();
            foreach (var position in positions)
            {
                int seniorityCount = _repository.Seniorities.GetByPosition(position).Count();
                int employeesCount = _repository.Employees.GetByPosition(position).Count();
                _ui.AddPosition(position.Name, seniorityCount, employeesCount);
            }
        }
    }
}