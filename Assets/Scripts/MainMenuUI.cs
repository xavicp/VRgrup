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

    [Header("Game Manager")]
    public ControladorJuego controladorJuego;

    [Header("Controlador Dianas")]
    public ControladorDianas controladorDianas;

    private bool isPaused = false;
    private bool inGameplay = false;

    private bool buttonPressedLastFrame = false;

    void Update()
    {
        if (!inGameplay)
            return;

        InputDevice rightHand =
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        bool aButtonPressed = false;

        if (rightHand.TryGetFeatureValue(
            CommonUsages.primaryButton,
            out aButtonPressed))
        {
            if (aButtonPressed && !buttonPressedLastFrame)
            {
                if (!isPaused)
                    OpenPause();
                else
                    ClosePause();
            }

            buttonPressedLastFrame = aButtonPressed;
        }
    }

    // =========================
    // MENU PRINCIPAL
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
        recordsPanel.SetActive(false);
        warningPanel.SetActive(false);

        panel.SetActive(true);
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

        if (controladorJuego != null)
        {
            controladorJuego.EmpezarJuego();
        }

        if (controladorDianas != null)
        {
            controladorDianas.EmpezarDianas();
        }
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

    public void ContinueGameplay()
    {
        ClosePause();
    }

    // =========================
    // MENU PRINCIPAL
    // =========================

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        isPaused = false;
        inGameplay = false;

        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        warningPanel.SetActive(false);
        instructionsPanel.SetActive(false);
        recordsPanel.SetActive(false);
        gameplayHUD.SetActive(false);

        if (controladorDianas != null)
        {
            controladorDianas.DetenerDianas();
        }

        panel.SetActive(true);
        mainMenuPanel.SetActive(true);
    }

    // =========================
    // REINICIAR PARTIDA
    // =========================

    public void RestartGame()
    {
        Time.timeScale = 1f;

        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        panel.SetActive(false);

        gameplayHUD.SetActive(true);

        isPaused = false;
        inGameplay = true;

        if (controladorJuego != null)
        {
            controladorJuego.ReiniciarJuego();
        }

        if (controladorDianas != null)
        {
            controladorDianas.EmpezarDianas();
        }
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

        if (controladorDianas != null)
        {
            controladorDianas.DetenerDianas();
        }

        Time.timeScale = 0f;
    }
}