using Employees.Model.DataAccess;
using Employees.Model.Sql.DataAccess;
using Employees.Presenters;
using Employees.Presenters.Positions;
using Employees.Presenters.Seniorities;
using Employees.Services.Database;
using Employees.UI;
using Employees.UI.Positions;
using Employees.UI.Seniorities;
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
        
        [SerializeField] SenioritiesUI senioritiesUI;

        void Awake()
        {
            SqliteCreator sqliteCreator = new();
            SqliteDatabase db = sqliteCreator.Create(databaseFilePath, forceNewDatabase);
            IDataRepository repository = new SqlDataRepository(db.NewConnection);
            
            SenioritiesPresenter senioritiesPresenter = new(repository, senioritiesUI);
            MainMenuPresenter mainMenuPresenter = new(mainMenuUI, positionsUI, senioritiesPresenter);
            
            PositionEditorPresenter positionEditorPresenter = new(positionEditorUI, repository);
            PositionsPresenter positionsPresenter = new(repository, positionsUI, positionEditorPresenter);
            
            
        }
    }
}