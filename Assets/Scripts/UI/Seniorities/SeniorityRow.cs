using TMPro;
using UnityEngine;

namespace Employees.UI.Seniorities
{
    public class SeniorityRow : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI positionNameText;
        [SerializeField] TextMeshProUGUI nameText;
        
        public string PositionName
        {
            get => positionNameText.text;
            set => positionNameText.text = value;
        }
        
        public string Name
        {
            get => nameText.text;
            set => nameText.text = value;
        }
    }
}