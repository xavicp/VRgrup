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
        Debug.Log("He tocado: " + other.name);

        if (golpeada)
            return;

        if (other.CompareTag("Mano"))
        {
            Debug.Log("Diana golpeada por: " + other.name);

            golpeada = true;

            controladorJuego.SumarPuntos();

            controladorDianas.DianaGolpeada();

            Destroy(gameObject);
        }
    }
}