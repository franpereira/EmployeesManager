using Employees.Presenters.Seniorities;
using Employees.UI;
using Employees.UI.Positions;
using Employees.UI.Seniorities;

namespace Employees.Presenters
{
    public class MainMenuPresenter
    {
        readonly IMainMenuUI _ui;
        readonly IPositionsUI _positionsUI;
        readonly SenioritiesPresenter _senioritiesPresenter;

        public MainMenuPresenter(IMainMenuUI ui, IPositionsUI positionsUI, SenioritiesPresenter senioritiesPresenter)
        {
            _ui = ui;
            _ui.PositionsSelected += OnPositionsSelected;
            _ui.SenioritiesSelected += OnSenioritiesSelected;
            _positionsUI = positionsUI;
            _senioritiesPresenter = senioritiesPresenter;
        }

        void OnPositionsSelected() => _positionsUI.ShowUI();
        void OnSenioritiesSelected() => _senioritiesPresenter.LoadAllSeniorities();
    }
}