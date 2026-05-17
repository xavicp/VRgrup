using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject warningPanel;
    public GameObject gameplayHUD;
    public GameObject instructionsPanel;
    public GameObject recordsPanel;

    // BOTÓN JUGAR
    public void PlayGame()
    {
        mainMenuPanel.SetActive(false);
        warningPanel.SetActive(true);
    }

    // BOTÓN "ESTOY LISTO"
    public void StartGameplay()
    {
        warningPanel.SetActive(false);
    }

    // BOTÓN INSTRUCCIONES
    public void OpenInstructions()
    {
        mainMenuPanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }

    // VOLVER DESDE INSTRUCCIONES
    public void BackToMainMenu()
    {
        instructionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // BOTÓN RECORDS
    public void OpenRecords()
    {
        mainMenuPanel.SetActive(false);
        recordsPanel.SetActive(true);
    }

    // VOLVER DESDE RECORDS
    public void BackFromRecords()
    {
        recordsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}