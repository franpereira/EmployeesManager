using Employees.Model.DataAccess;
using Employees.Presenters.Positions;
using Employees.UI.Seniorities;

namespace Employees.Presenters.Seniorities
{
    public class SenioritiesPresenter
    {
        readonly ISenioritiesUI _ui;
        readonly IDataRepository _repository;
        
        public SenioritiesPresenter(IDataRepository repository, ISenioritiesUI ui)
        {
            _repository = repository;
            _ui = ui;
        }
        
        public void LoadAllSeniorities()
        {
            _ui.LoadSeniorities(_repository.Seniorities.GetAll());
            _ui.ShowUI();
        }
    }
}