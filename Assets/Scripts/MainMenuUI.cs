using UnityEngine;
using UnityEngine.XR;
using TMPro;

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

    [Header("Game Over")]
    public TMP_Text textoPuntuacionFinal;

    [Header("Records")]
    public TMP_Text textoRecord;

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

    public void MostrarPuntuacionFinal(int puntuacion)
    {
        if (textoPuntuacionFinal != null)
        {
            textoPuntuacionFinal.text =
                "PUNTOS: " + puntuacion.ToString("0000");
        }
    }

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

        if (textoRecord != null)
        {
            textoRecord.text =
                PlayerPrefs.GetInt("Record", 0)
                .ToString("0000");
        }
    }

    public void BackFromRecords()
    {
        recordsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

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
    }

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

        panel.SetActive(true);
        mainMenuPanel.SetActive(true);
    }

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
    }

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