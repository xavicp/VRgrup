using UnityEngine;
using UnityEngine.XR;

public class MainMenuUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject warningPanel;
    public GameObject gameplayHUD;
    public GameObject instructionsPanel;
    public GameObject recordsPanel;
    public GameObject gameOverPanel;

    [Header("Pause")]
    public GameObject pausePanel;

    [Header("Background")]
    public GameObject panel;

    // CONTROL
    private bool isPaused = false;
    private bool inGameplay = false;

    // CONTROL BOTÓN A
    private bool buttonPressedLastFrame = false;

    void Update()
    {
        // SOLO FUNCIONA EN GAMEPLAY
        if (!inGameplay)
            return;

        // MANDO DERECHO QUEST
        InputDevice rightHand =
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        bool aButtonPressed = false;

        if (rightHand.TryGetFeatureValue(
            CommonUsages.primaryButton,
            out aButtonPressed))
        {
            // DETECTAR SOLO UNA PULSACIÓN
            if (aButtonPressed && !buttonPressedLastFrame)
            {
                if (!isPaused)
                {
                    OpenPause();
                }
                else
                {
                    ClosePause();
                }
            }

            buttonPressedLastFrame = aButtonPressed;
        }
    }

    // =========================
    // MENÚ PRINCIPAL
    // =========================

    public void PlayGame()
    {
        mainMenuPanel.SetActive(false);
        warningPanel.SetActive(true);
    }

    public void OpenInstructions()
    {
        mainMenuPanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        instructionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void OpenRecords()
    {
        mainMenuPanel.SetActive(false);
        recordsPanel.SetActive(true);
    }

    public void BackFromRecords()
    {
        recordsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // =========================
    // GAMEPLAY
    // =========================

    public void StartGameplay()
    {
        inGameplay = true;

        warningPanel.SetActive(false);
        panel.SetActive(false);

        gameplayHUD.SetActive(true);

        Time.timeScale = 1f;
    }

    // =========================
    // PAUSA
    // =========================

    public void OpenPause()
    {
        isPaused = true;

        panel.SetActive(true);
        pausePanel.SetActive(true);

        gameplayHUD.SetActive(false);

        Time.timeScale = 0f;
    }

    public void ClosePause()
    {
        isPaused = false;

        pausePanel.SetActive(false);
        panel.SetActive(false);

        gameplayHUD.SetActive(true);

        Time.timeScale = 1f;
    }

    // BOTÓN CONTINUAR
    public void ContinueGameplay()
    {
        ClosePause();
    }

    // BOTÓN MENÚ PRINCIPAL
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        isPaused = false;
        inGameplay = false;

        // OCULTAR TODO
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        warningPanel.SetActive(false);
        instructionsPanel.SetActive(false);
        recordsPanel.SetActive(false);
        gameplayHUD.SetActive(false);

        // MOSTRAR MENÚ
        panel.SetActive(true);
        mainMenuPanel.SetActive(true);
    }

    // =========================
    // GAME OVER
    // =========================

    public void GameOver()
    {
        inGameplay = false;

        panel.SetActive(true);

        gameplayHUD.SetActive(false);

        pausePanel.SetActive(false);

        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }
}