using UnityEngine;

public class Diana : MonoBehaviour
{
    private ControladorDianas targetController;
    private ControladorJuego gameController;

    private bool hasBeenHit = false;

    void Start()
    {
        targetController =
            FindObjectOfType<ControladorDianas>();

        gameController =
            FindObjectOfType<ControladorJuego>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(
            "Name: " + other.name +
            " | Tag: " + other.tag
        );

        if (hasBeenHit)
            return;

        if (
            other.CompareTag("Mano") ||
            other.name.Contains("Capsule")
        )
        {
            Debug.Log("TARGET HIT");
            Debug.Log("Hit by: " + other.name);

            hasBeenHit = true;

            if (gameController != null)
            {
                gameController.SumarPuntos();
                Debug.Log("Points added");
            }
            else
            {
                Debug.LogError("GameController not found");
            }

            if (targetController != null)
            {
                targetController.TargetHit();
                Debug.Log("Target respawn");
            }
            else
            {
                Debug.LogError("TargetController not found");
            }

            Destroy(gameObject);
        }
    }
}