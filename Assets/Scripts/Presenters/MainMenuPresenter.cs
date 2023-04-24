using Employees.UI;

namespace Employees.Presenters
{
    public class MainMenuPresenter
    {
        readonly IMainMenuUI _ui;
        IPositionsUI _positionsUI;

        public MainMenuPresenter(IMainMenuUI ui, IPositionsUI positionsUI)
        {
            _ui = ui;
            _positionsUI = positionsUI;
            _ui.PositionsSelected += OnPositionsSelected;
        }

        void OnPositionsSelected() => _positionsUI.ShowUI();
    }
}