using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJuego : MonoBehaviour
{
    [Header("Configuración de Lanzamiento")]
    public Transform[] puntosLanzamiento; // Aquí arrastraremos los 3 planos
    public GameObject prefabCubo;
    public GameObject prefabEsfera;
    public float tiempoEntreLanzamientos = 2f;
    public float fuerzaLanzamiento = 5f; // Fuerza para empujar el objeto hacia el jugador

    [Header("Estadísticas de Jugador")]
    private int vida = 100;
    private int puntos = 0;
    private bool juegoTerminado = false;

    void Start()
    {
        // Empezamos a lanzar objetos repetidamente
        StartCoroutine(RutinaLanzamiento());
    }

    IEnumerator RutinaLanzamiento()
    {
        while (!juegoTerminado)
        {
            yield return new WaitForSeconds(tiempoEntreLanzamientos);

            if (juegoTerminado) break;

            // 1. Elegir un plano aleatorio de los 3
            int indicePlano = Random.Range(0, puntosLanzamiento.Length);
            Transform planoElegido = puntosLanzamiento[indicePlano];

            // 2. Elegir aleatoriamente si sale Cubo (0) o Esfera (1)
            GameObject objetoAVisualizar = (Random.Range(0, 2) == 0) ? prefabCubo : prefabEsfera;

            // 3. Crear el objeto en la posición del plano
            GameObject objetoClonado = Instantiate(objetoAVisualizar, planoElegido.position, Quaternion.identity);

            // 4. Empujar el objeto hacia adelante (dirección al jugador)
            Rigidbody rb = objetoClonado.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Lanzar en dirección opuesta al eje Z del plano (hacia el jugador)
                //rb.AddForce(Vector3.back * fuerzaLanzamiento, ForceMode.Impulse);
                // 'forward' es la dirección hacia donde está mirando el plano en ese instante
                rb.AddForce(planoElegido.forward * fuerzaLanzamiento, ForceMode.Impulse);
            }

            // Destruir el objeto automáticamente a los 5 segundos si no toca nada para que no sature el juego
            Destroy(objetoClonado, 5f);
        }
    }

    public void SumarPuntos()
    {
        if (juegoTerminado) return;
        puntos += 10;
        Debug.Log("¡Cubo golpeado! Puntos: " + puntos);
    }

    public void RestarVida()
    {
        if (juegoTerminado) return;
        vida -= 10;
        Debug.Log("¡Te ha dado una esfera! Vida restante: " + vida);

        if (vida <= 0)
        {
            juegoTerminado = true;
            Debug.Log("GAME OVER. Te has quedado sin vida. Puntos totales: " + puntos);
        }
    }
}