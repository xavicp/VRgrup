using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJuego : MonoBehaviour
{
    [Header("Configuraci�n de Lanzamiento")]
    public Transform[] puntosLanzamiento;
    public Transform jugador;

    public GameObject prefabCubo;
    public GameObject prefabEsfera;

    public float tiempoEntreLanzamientos = 2f;
    public float fuerzaLanzamiento = 5f;

    [Header("Estad�sticas de Jugador")]
    public int vidaMaxima = 100;

    private int vida;
    private int puntos;
    private float tiempoPartida;

    private bool juegoTerminado = false;

    private int record = 0;

    void Start()
    {

    }

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

    IEnumerator RutinaLanzamiento()
    {
        while (!juegoTerminado)
        {
            yield return new WaitForSeconds(tiempoEntreLanzamientos);

            if (juegoTerminado) break;

            int indicePlano = Random.Range(0, puntosLanzamiento.Length);
            Transform lanzadorElegido = puntosLanzamiento[indicePlano];

            GameObject objetoAVisualizar =
                (Random.Range(0, 2) == 0) ? prefabCubo : prefabEsfera;

            GameObject objetoClonado = Instantiate(
                objetoAVisualizar,
                lanzadorElegido.position,
                Quaternion.identity
            );

            Rigidbody rb = objetoClonado.GetComponent<Rigidbody>();

            if (rb != null && jugador != null)
            {
                Vector3 direccion =
                    (jugador.position - lanzadorElegido.position).normalized;

                Debug.Log("Vector de disparo: " + direccion);

                rb.AddForce(direccion * fuerzaLanzamiento, ForceMode.Impulse);
            }

            Destroy(objetoClonado, 5f);
        }
    }

    public void SumarPuntos()
    {
        if (juegoTerminado) return;

        puntos += 10;

        Debug.Log("Puntos: " + puntos);
    }

    public void RestarVida()
    {
        if (juegoTerminado) return;

        vida -= 10;

        Debug.Log("Vida restante: " + vida);

        if (vida <= 0)
        {
            juegoTerminado = true;

            Debug.Log("GAME OVER");
            Debug.Log("Puntos finales: " + puntos);
            Debug.Log("Tiempo sobrevivido: " + tiempoPartida);
        }
    }
}