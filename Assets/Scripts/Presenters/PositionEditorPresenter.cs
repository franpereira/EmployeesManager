using System;
using Employees.Model;
using Employees.Model.DataAccess;
using Employees.UI;
using UnityEngine;

namespace Employees.Presenters
{
    public class PositionEditorPresenter
    {
        public event Action DataSaved;
        
        readonly IPositionEditorUI _ui;
        readonly IDataRepository _repository;

        Position _position;

        public PositionEditorPresenter(IPositionEditorUI ui, IDataRepository repository)
        {
            _ui = ui;
            _ui.SavePositionSelected += SavePosition;
            _repository = repository;
        }

        public void LoadPosition(int id)
        {
            _ui.ShowUI();
            if (id == 0)
                _position = new Position();
            else
            {
                _position = _repository.Positions.Get(id);
                _ui.Name = _position.Name; 
            }
        }

        public void SavePosition()
        {
            _position.Name = _ui.Name;
            
            if (_position.Id == 0)
                _repository.Positions.Add(_position);
            else
                _repository.Positions.Update(_position);
            
            DataSaved?.Invoke();
            _ui.HideUI();
        }
    }
}