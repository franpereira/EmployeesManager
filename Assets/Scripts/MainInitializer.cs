using Employees.Model.DataAccess;
using Employees.Model.Sql.DataAccess;
using Employees.Presenters;
using Employees.Services.Database;
using Employees.UI;
using UnityEngine;

namespace Employees
{
    public class MainInitializer : MonoBehaviour
    {
        [SerializeField] string databaseFilePath = "AcmeCorporation.sqlite";
        [SerializeField] bool forceNewDatabase = false;
        
        [SerializeField] MainMenuUI mainMenuUI;
        [SerializeField] PositionsUI positionsUI;
        [SerializeField] PositionEditorUI positionEditorUI;

        void Awake()
        {
            SqliteCreator sqliteCreator = new();
            SqliteDatabase db = sqliteCreator.Create(databaseFilePath, forceNewDatabase);
            IDataRepository repository = new SqlDataRepository(db.NewConnection);
            
            MainMenuPresenter mainMenuPresenter = new(mainMenuUI, positionsUI);
            PositionEditorPresenter positionEditorPresenter = new(positionEditorUI, repository);
            PositionsPresenter positionsPresenter = new(repository, positionsUI, positionEditorPresenter);
        }
    }
}