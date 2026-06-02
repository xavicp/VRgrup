using UnityEngine;

public class RotarHaciaJugador : MonoBehaviour
{
    private Transform objetivoJugador;

    void Start()
    {
        if (Camera.main != null)
        {
            objetivoJugador = Camera.main.transform;
        }
        else
        {
            Debug.LogError("No se encontró la Main Camera.");
        }
    }

    void Update()
    {
        if (objetivoJugador != null)
        {
            Vector3 direccion =
                objetivoJugador.position - transform.position;

            // Avoid vertical rotation
            direccion.y = 0;

            // Rotation towards the player
            transform.rotation =
                Quaternion.LookRotation(direccion);
        }
    }
}