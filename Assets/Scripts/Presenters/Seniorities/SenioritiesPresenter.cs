using System.Linq;
using Employees.Model.DataAccess;
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
            _ui.Clear();
            var seniorities = _repository.Seniorities.GetAll();
            foreach (var seniority in seniorities)
            {
                int employeesCount = _repository.Employees.GetBySeniority(seniority).Count(); // Having a CountBySeniority() could be better?
                _ui.AddSeniority(seniority.Name, seniority.Position.Name, employeesCount, seniority.BaseSalary, seniority.PercentagePerIncrement,
                    seniority.CurrentIncrements, seniority.Salary);
            }
                
            _ui.ShowUI();
        }
    }
}