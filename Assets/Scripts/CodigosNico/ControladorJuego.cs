using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJuego : MonoBehaviour
{
    [Header("Configuración de Lanzamiento")]
    public Transform[] puntosLanzamiento;
    public GameObject prefabCubo;
    public GameObject prefabEsfera;

    public float tiempoEntreLanzamientos = 2f;
    public float fuerzaLanzamiento = 5f;

    [Header("Jugador")]
    public Transform jugador;

    [Header("Desviación del disparo")]
    public float desviacionHorizontal = 1.5f;
    public float desviacionVertical = 1f;

    [Header("Estadísticas de Jugador")]
    private int vida = 100;
    private int puntos = 0;
    private bool juegoTerminado = false;

    private int record = 0;

    void Start()
    {

    }

    public void EmpezarJuego()
    {
        vida = 100;
        puntos = 0;
        juegoTerminado = false;

        StartCoroutine(RutinaLanzamiento());
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
                Vector3 objetivo = jugador.position;

                objetivo.x += Random.Range(-desviacionHorizontal, desviacionHorizontal);
                objetivo.y += Random.Range(-desviacionVertical, desviacionVertical);

                Vector3 direccion =
                    (objetivo - lanzadorElegido.position).normalized;

                rb.AddForce(direccion * fuerzaLanzamiento, ForceMode.Impulse);
            }

            Destroy(objetoClonado, 5f);
        }
    }

    public void SumarPuntos()
    {
        if (juegoTerminado) return;

        puntos += 10;

        if (puntos > record)
        {
            record = puntos;
        }

        Debug.Log("¡Cubo golpeado! Puntos: " + puntos);
        Debug.Log("Récord actual: " + record);
    }

    public void RestarVida()
    {
        if (juegoTerminado) return;

        vida -= 25;

        Debug.Log("¡Te ha dado una esfera! Vida restante: " + vida);

        if (vida <= 0)
        {
            juegoTerminado = true;

            Debug.Log("GAME OVER. Puntos totales: " + puntos);
            Debug.Log("Récord máximo: " + record);
        }
    }
}