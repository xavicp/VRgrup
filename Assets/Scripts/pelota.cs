using UnityEngine;

public class pelota : MonoBehaviour
{
    private ControladorJuego controladorJuego;

    private bool haImpactado = false;

    void Start()
    {
        controladorJuego =
            FindObjectOfType<ControladorJuego>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (haImpactado)
            return;

        // Detectar jugador
        if (other.CompareTag("MainCamera"))
        {
            haImpactado = true;

            controladorJuego.RestarVida();

            Debug.Log("Jugador impactado");

            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (!haImpactado && controladorJuego != null)
        {
            controladorJuego.SumarPuntos();
            Debug.Log("Pelota esquivada");
        }
    }
}