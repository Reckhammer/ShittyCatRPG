using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MapEntryMenu : MonoBehaviour
{
    public static MapEntryMenu instance;
    public GameObject menu;
    public TextMeshProUGUI text;
    public Button confirmButton;
    public Button cancelButton;

    private string sceneToLoad = "";

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void OpenMenu(string sceneName, bool isEncounter, string niceName = "")
    {
        if (isEncounter)
        {
            text.text = "You have been ambushed";
            cancelButton.gameObject.SetActive(false);
        }
        else
        {
            text.text = $"You are about to enter "+ niceName;
        }

        sceneToLoad = sceneName;

        menu.SetActive(true);
    }

    public void OnConfirmButtonClicked()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OnCancelButtonClicked()
    {
        menu.SetActive(false);
        sceneToLoad = "";
    }
}
