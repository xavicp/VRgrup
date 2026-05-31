using UnityEngine;

public class Diana : MonoBehaviour
{
    private ControladorDianas controlador;

    private bool golpeada = false;

    void Start()
    {
        controlador =
            FindObjectOfType<ControladorDianas>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (golpeada)
            return;

        if (other.CompareTag("Mano"))
        {
            golpeada = true;

            Debug.Log("Diana golpeada");

            controlador.DianaGolpeada();

            Destroy(gameObject);
        }
    }
}