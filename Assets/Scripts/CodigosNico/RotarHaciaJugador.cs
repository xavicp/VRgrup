using UnityEngine;

public class RotarHaciaJugador : MonoBehaviour
{
    // Aquí guardaremos la referencia de la cabeza del jugador (la cámara VR)
    private Transform objetivoJugador;

    void Start()
    {
        // Buscamos en la escena el objeto que tenga la etiqueta "Player" (tu cámara)
        GameObject jugador = Camera.main.gameObject;

        if (jugador != null)
        {
            objetivoJugador = jugador.transform;
        }
        else
        {
            Debug.LogError("¡Ojo! No se encuentra ningún objeto con la etiqueta 'Player'. Asegúrate de haberle puesto el Tag a la Main Camera.");
        }
    }

    void Update()
    {
        // Si encontramos al jugador, lo miramos en cada fotograma
        if (objetivoJugador != null)
        {
            // Esta línea mágica hace que el eje "Z" (el frente) del plano apunte al jugador
            transform.LookAt(objetivoJugador);

            // Truco técnico para planos: los planos de Unity por defecto miran "hacia arriba".
            // Para que su cara plana mire al frente hacia ti, a veces hay que rotarlos 90 grados en el eje X.
            // Si ves que se giran de canto, añade esta línea descomentándola:
            // transform.Rotate(90, 0, 0);
        }
    }
}