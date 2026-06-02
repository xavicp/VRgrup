using System.Collections;
using UnityEngine;
using TMPro;

public class ControladorJuego : MonoBehaviour
{
    [Header("Configuracion de Lanzamiento")]
    public Transform[] puntosLanzamiento;

    [Header("Prefabs de Pelotas")]
    public GameObject pelotaPequena;
    public GameObject pelotaMediana;
    public GameObject pelotaGrande;

    [Header("Velocidad de Pelotas")]
    public float velocidadPelotaPequena = 0.8f;
    public float velocidadPelotaMediana = 1.5f;
    public float velocidadPelotaGrande = 2.2f;

    [Header("Movimiento de la pelota")]
    public float alturaParabola = 2f;
    public float tiempoEntreLanzamientos = 2f;

    [Header("Distancia recta de disparo")]
    public float distanciaDisparo = 10f;

    [Header("Estadisticas de Jugador")]
    public int vidaMaxima = 3;

    [Header("UI")]
    public MainMenuUI mainMenuUI;

    [Header("Dianas")]
    public ControladorDianas targetController;

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

        if (targetController != null)
        {
            targetController.StartTargets();
        }

        StartCoroutine(RutinaLanzamiento());
    }

    public void ReiniciarJuego()
    {
        StopAllCoroutines();

        juegoTerminado = true;

        if (targetController != null)
        {
            targetController.StopTargets();
        }

        vida = vidaMaxima;
        puntos = 0;
        tiempoPartida = 0f;

        record = PlayerPrefs.GetInt("Record", 0);

        juegoTerminado = false;

        ActualizarHUD();
        ActualizarVidasHUD();

        if (targetController != null)
        {
            targetController.StartTargets();
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

            int indicePlano =
                Random.Range(0, puntosLanzamiento.Length);

            Transform lanzadorElegido =
                puntosLanzamiento[indicePlano];

            GameObject prefabSeleccionado = null;
            float duracionTrayectoria = 1f;

            int tipoPelota = Random.Range(0, 3);

            switch (tipoPelota)
            {
                case 0:
                    prefabSeleccionado = pelotaPequena;
                    duracionTrayectoria = velocidadPelotaPequena;
                    break;

                case 1:
                    prefabSeleccionado = pelotaMediana;
                    duracionTrayectoria = velocidadPelotaMediana;
                    break;

                case 2:
                    prefabSeleccionado = pelotaGrande;
                    duracionTrayectoria = velocidadPelotaGrande;
                    break;
            }

            GameObject objetoClonado = Instantiate(
                prefabSeleccionado,
                lanzadorElegido.position,
                Quaternion.identity
            );

            Vector3 direccion =
                lanzadorElegido.forward;

            Vector3 destino =
                lanzadorElegido.position +
                direccion * distanciaDisparo;

            StartCoroutine(
                MoverPelotaParabola(
                    objetoClonado,
                    lanzadorElegido.position,
                    destino,
                    duracionTrayectoria
                )
            );

            Destroy(objetoClonado, 5f);
        }
    }

    IEnumerator MoverPelotaParabola(
        GameObject pelota,
        Vector3 inicio,
        Vector3 final,
        float duracion)
    {
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            if (pelota == null)
                yield break;

            float progreso =
                tiempo / duracion;

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

    public void TerminarPartida()
    {
        juegoTerminado = true;

        StopAllCoroutines();

        if (targetController != null)
        {
            targetController.StopTargets();
        }
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

            if (targetController != null)
            {
                targetController.StopTargets();
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