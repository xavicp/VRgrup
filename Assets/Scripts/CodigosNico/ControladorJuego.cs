using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJuego : MonoBehaviour
{
    [Header("Configuración de Lanzamiento")]
    public Transform[] puntosLanzamiento;
    public Transform jugador;

    public GameObject prefabEsfera;

    public float tiempoEntreLanzamientos = 2f;

    [Header("Movimiento de la pelota")]
    public float duracionTrayectoria = 1.5f;
    public float alturaParabola = 2f;

    [Header("Estadísticas de Jugador")]
    public int vidaMaxima = 3;

    [Header("UI")]
    public MainMenuUI mainMenuUI;

    private int vida;
    private int puntos;
    private float tiempoPartida;

    private bool juegoTerminado = false;

    void Start()
    {

    }

    // =========================
    // EMPEZAR PARTIDA
    // =========================

    public void EmpezarJuego()
    {
        vida = vidaMaxima;
        puntos = 0;
        tiempoPartida = 0f;

        juegoTerminado = false;

        StartCoroutine(RutinaLanzamiento());
    }

    void Update()
    {
        if (!juegoTerminado)
        {
            tiempoPartida += Time.deltaTime;
        }
    }

    // =========================
    // LANZAMIENTO DE PELOTAS
    // =========================

    IEnumerator RutinaLanzamiento()
    {
        while (!juegoTerminado)
        {
            yield return new WaitForSeconds(tiempoEntreLanzamientos);

            if (juegoTerminado)
                break;

            int indicePlano = Random.Range(0, puntosLanzamiento.Length);

            Transform lanzadorElegido =
                puntosLanzamiento[indicePlano];

            GameObject objetoClonado = Instantiate(
                prefabEsfera,
                lanzadorElegido.position,
                Quaternion.identity
            );

            StartCoroutine(
                MoverPelotaParabola(
                    objetoClonado,
                    lanzadorElegido.position,
                    jugador.position
                )
            );

            Destroy(objetoClonado, 5f);
        }
    }

    // =========================
    // MOVIMIENTO PARABÓLICO
    // =========================

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

    // =========================
    // PUNTOS
    // =========================

    public void SumarPuntos()
    {
        if (juegoTerminado)
            return;

        puntos += 10;

        Debug.Log("Puntos: " + puntos);
    }

    // =========================
    // VIDA
    // =========================

    public void RestarVida()
    {
        if (juegoTerminado)
            return;

        vida -= 1;

        Debug.Log("Vidas restantes: " + vida);

        // GAME OVER
        if (vida <= 0)
        {
            juegoTerminado = true;

            Debug.Log("GAME OVER");

            Debug.Log(
                "Puntos finales: " + puntos);

            Debug.Log(
                "Tiempo sobrevivido: " +
                tiempoPartida);

            // ACTIVAR GAME OVER UI
            if (mainMenuUI != null)
            {
                mainMenuUI.GameOver();
            }
        }
    }
}