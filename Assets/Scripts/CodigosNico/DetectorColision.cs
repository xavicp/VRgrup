using UnityEngine;

public class DetectorColision : MonoBehaviour
{
    private ControladorJuego controlador;

    void Start()
    {
        // Buscamos automáticamente el cerebro del juego en la escena
        controlador = FindObjectOfType<ControladorJuego>();
    }

    // Este método se activa cuando algo choca con este objeto
    void OnCollisionEnter(Collision collision)
    {
        // Si este script está en la mano y choca con un Cubo
        if (gameObject.CompareTag("Mano") && collision.gameObject.CompareTag("Cubo"))
        {
            controlador.SumarPuntos();
            Destroy(collision.gameObject); // Destruye el cubo al golpearlo
        }

        // Si este script está en el jugador (cabeza) y le choca una Esfera
        if (gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Esfera"))
        {
            controlador.RestarVida();
            Destroy(collision.gameObject); // Destruye la esfera para que no golpee dos veces
        }
    }
}