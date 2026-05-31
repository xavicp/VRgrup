using UnityEngine;

public class Diana : MonoBehaviour
{
    private ControladorDianas controladorDianas;
    private ControladorJuego controladorJuego;

    private bool golpeada = false;

    void Start()
    {
        controladorDianas =
            FindObjectOfType<ControladorDianas>();

        controladorJuego =
            FindObjectOfType<ControladorJuego>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(
            "Nombre: " + other.name +
            " | Tag: " + other.tag
        );

        if (golpeada)
            return;

        if (
            other.CompareTag("Mano") ||
            other.name.Contains("Capsule")
        )
        {
            Debug.Log("HE ENTRADO EN EL IF");
            Debug.Log("Diana golpeada por: " + other.name);

            golpeada = true;

            if (controladorJuego != null)
            {
                controladorJuego.SumarPuntos();
                Debug.Log("Puntos sumados");
            }
            else
            {
                Debug.LogError("No se encontró ControladorJuego");
            }

            if (controladorDianas != null)
            {
                controladorDianas.DianaGolpeada();
                Debug.Log("Respawn de diana");
            }
            else
            {
                Debug.LogError("No se encontró ControladorDianas");
            }

            Destroy(gameObject);
        }
    }
}