using System.Collections.Generic;
using Employees.Model;

namespace Employees.UI.Seniorities
{
    public interface ISenioritiesUI
    {
        public void LoadSeniorities(IEnumerable<Seniority> seniorities);
        public void ShowUI();
        public void HideUI();
    }
}