using UnityEngine;

public class DetectorColision : MonoBehaviour
{
    private ControladorJuego controlador;

    void Start()
    {
        controlador = FindObjectOfType<ControladorJuego>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // MANO hits CUBO
        if (gameObject.CompareTag("Mano") &&
            collision.gameObject.CompareTag("Cubo"))
        {
            controlador.SumarPuntos();

            Destroy(collision.gameObject);
        }

        // PLAYER hit by ESFERA
        if (gameObject.CompareTag("MainCamera") &&
            collision.gameObject.CompareTag("Esfera"))
        {
            controlador.RestarVida();

            Destroy(collision.gameObject);
        }
    }
}