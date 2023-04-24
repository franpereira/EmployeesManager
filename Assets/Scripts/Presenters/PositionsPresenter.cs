using System.Collections.Generic;
using Employees.Model;
using Employees.Model.DataAccess;
using Employees.UI;
using UnityEngine;

namespace Employees.Presenters
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
            _editorPresenter.DataSaved += LoadPositions;
            InitializeUI();
        }
        
        void OnEditPositionRequested(int id)
        {
            _editorPresenter.LoadPosition(id);
        }

        void InitializeUI()
        {
            Debug.Log("Positions UI initialized");
            LoadPositions();
        }
        
        public void LoadPositions() => _ui.LoadPositions(_repository.Positions.GetAll());
    }
}