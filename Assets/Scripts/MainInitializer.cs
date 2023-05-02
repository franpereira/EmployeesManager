using Employees.Model.DataAccess;
using Employees.Model.Sql.DataAccess;
using Employees.Presenters;
using Employees.Presenters.Employees;
using Employees.Presenters.Positions;
using Employees.Presenters.Seniorities;
using Employees.UI;
using Employees.UI.Unity;
using Employees.UI.Unity.Employees;
using Employees.UI.Unity.Positions;
using Employees.UI.Unity.Seniorities;
using Employees.Utilities.Database;
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

        [SerializeField] EmployeesUI employeesUI;

        void Awake()
        {
            SqliteCreator sqliteCreator = new();
            SqliteDatabase db = sqliteCreator.Create(databaseFilePath, forceNewDatabase);
            IDataRepository repository = new SqlDataRepository(db.NewConnection);

            EmployeesPresenter employeesPresenter = new(repository, employeesUI);
            SenioritiesPresenter senioritiesPresenter = new(repository, senioritiesUI, employeesPresenter);
            PositionEditorPresenter positionEditorPresenter = new(positionEditorUI, repository);
            PositionsPresenter positionsPresenter = new(repository, positionsUI, positionEditorPresenter, senioritiesPresenter, employeesPresenter);

            MainMenuPresenter mainMenuPresenter = new(mainMenuUI, positionsPresenter, senioritiesPresenter, employeesPresenter);
        }
    }
}