using System.Collections;
using UnityEngine;
using TMPro;

public class ControladorJuego : MonoBehaviour
{
    [Header("Configuracion de Lanzamiento")]
    public Transform[] puntosLanzamiento;
    public GameObject prefabEsfera;

    [Header("Movimiento de la pelota")]
    public float duracionTrayectoria = 1.5f;
    public float alturaParabola = 2f;
    public float tiempoEntreLanzamientos = 2f;

    [Header("Distancia recta de disparo")]
    public float distanciaDisparo = 10f;

    [Header("Estadisticas de Jugador")]
    public int vidaMaxima = 3;

    [Header("UI")]
    public MainMenuUI mainMenuUI;

    [Header("Dianas")]
    public ControladorDianas controladorDianas;

    [Header("HUD Puntuacion")]
    public TMP_Text textoPuntuacion;

    [Header("HUD Vidas")]
    public GameObject corazon1;
    public GameObject corazon2;
    public GameObject corazon3;

    private int vida;
    private int puntos;
    private int record;

    private float tiempoPartida;

    private bool juegoTerminado = false;

    void ActualizarHUD()
    {
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = puntos.ToString("0000");
        }
    }

    void ActualizarVidasHUD()
    {
        if (corazon1 != null)
            corazon1.SetActive(vida >= 1);

        if (corazon2 != null)
            corazon2.SetActive(vida >= 2);

        if (corazon3 != null)
            corazon3.SetActive(vida >= 3);
    }

    public int ObtenerPuntuacion()
    {
        return puntos;
    }

    public int ObtenerRecord()
    {
        return PlayerPrefs.GetInt("Record", 0);
    }

    public void EmpezarJuego()
    {
        StopAllCoroutines();

        vida = vidaMaxima;
        puntos = 0;
        tiempoPartida = 0f;

        record = PlayerPrefs.GetInt("Record", 0);

        juegoTerminado = false;

        ActualizarHUD();
        ActualizarVidasHUD();

        if (controladorDianas != null)
        {
            controladorDianas.EmpezarDianas();
        }

        StartCoroutine(RutinaLanzamiento());
    }

    public void ReiniciarJuego()
    {
        StopAllCoroutines();

        vida = vidaMaxima;
        puntos = 0;
        tiempoPartida = 0f;

        record = PlayerPrefs.GetInt("Record", 0);

        juegoTerminado = false;

        ActualizarHUD();
        ActualizarVidasHUD();

        if (controladorDianas != null)
        {
            controladorDianas.EmpezarDianas();
        }

        StartCoroutine(RutinaLanzamiento());
    }

    void Update()
    {
        if (!juegoTerminado)
        {
            tiempoPartida += Time.deltaTime;
        }
    }

    IEnumerator RutinaLanzamiento()
    {
        while (!juegoTerminado)
        {
            yield return new WaitForSeconds(tiempoEntreLanzamientos);

            if (juegoTerminado)
                yield break;

            int indicePlano = Random.Range(0, puntosLanzamiento.Length);

            Transform lanzadorElegido =
                puntosLanzamiento[indicePlano];

            GameObject objetoClonado = Instantiate(
                prefabEsfera,
                lanzadorElegido.position,
                Quaternion.identity
            );

            Vector3 direccion = lanzadorElegido.forward;

            Vector3 destino =
                lanzadorElegido.position +
                direccion * distanciaDisparo;

            StartCoroutine(
                MoverPelotaParabola(
                    objetoClonado,
                    lanzadorElegido.position,
                    destino
                )
            );

            Destroy(objetoClonado, 5f);
        }
    }

    IEnumerator MoverPelotaParabola(
        GameObject pelota,
        Vector3 inicio,
        Vector3 final)
    {
        float tiempo = 0f;

        while (tiempo < duracionTrayectoria)
        {
            if (pelota == null)
                yield break;

            float progreso =
                tiempo / duracionTrayectoria;

            Vector3 posicionLineal =
                Vector3.Lerp(inicio, final, progreso);

            float altura =
                4 * alturaParabola *
                progreso *
                (1 - progreso);

            posicionLineal.y += altura;

            pelota.transform.position =
                posicionLineal;

            tiempo += Time.deltaTime;

            yield return null;
        }

        if (pelota != null)
        {
            pelota.transform.position = final;
        }
    }

    public void SumarPuntos()
    {
        if (juegoTerminado)
            return;

        puntos += 10;

        ActualizarHUD();

        Debug.Log("Puntos: " + puntos);
    }

    public void RestarVida()
    {
        if (juegoTerminado)
            return;

        vida--;

        ActualizarVidasHUD();

        Debug.Log("Vidas restantes: " + vida);

        if (vida <= 0)
        {
            juegoTerminado = true;

            if (puntos > record)
            {
                record = puntos;

                PlayerPrefs.SetInt("Record", record);
                PlayerPrefs.Save();
            }

            if (controladorDianas != null)
            {
                controladorDianas.DetenerDianas();
            }

            Debug.Log("GAME OVER");
            Debug.Log("Puntos finales: " + puntos);
            Debug.Log("Tiempo sobrevivido: " + tiempoPartida);

            if (mainMenuUI != null)
            {
                mainMenuUI.MostrarPuntuacionFinal(puntos);
                mainMenuUI.GameOver();
            }
        }
    }
}