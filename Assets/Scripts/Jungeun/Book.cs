using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    public Button bookButton; 
    public GameObject bookPanel;

    void Start()
    {
        bookButton.onClick.AddListener(TogglePanel);
        bookPanel.SetActive(false); 
    }

    void TogglePanel()
    {
        bookPanel.SetActive(!bookPanel.activeSelf);
    }
}
