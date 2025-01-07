using UnityEngine;
using UnityEngine.UI;

public class WaypointUI : MonoBehaviour
{
    [SerializeField] public GameObject representation;
    [SerializeField] public Image colorImage;
    [SerializeField] public Text labelText;
    [SerializeField] public Button changeNameButton;
    [SerializeField] public Button changeColorButton;
    [SerializeField] public Button deleteButton;
    [SerializeField] public Button forceDeleteButton;
}
