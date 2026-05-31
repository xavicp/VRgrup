using System.Collections;
using UnityEngine;

public class ControladorDianas : MonoBehaviour
{
    [Header("Puntos de aparición")]
    public Transform puntoDianaIzquierda;
    public Transform puntoDianaDerecha;

    [Header("Prefab")]
    public GameObject prefabDiana;

    [Header("Tiempos")]
    public float tiempoCambio = 3f;
    public float delayRespawn = 0.5f;

    private GameObject dianaActual;

    public void EmpezarDianas()
    {
        StopAllCoroutines();

        if (dianaActual != null)
        {
            Destroy(dianaActual);
        }

        SpawnNuevaDiana();
    }

    public void DetenerDianas()
    {
        StopAllCoroutines();

        if (dianaActual != null)
        {
            Destroy(dianaActual);
        }
    }

    void SpawnNuevaDiana()
    {
        Transform puntoElegido;

        if (Random.Range(0, 2) == 0)
        {
            puntoElegido = puntoDianaIzquierda;
        }
        else
        {
            puntoElegido = puntoDianaDerecha;
        }

        dianaActual = Instantiate(
            prefabDiana,
            puntoElegido.position,
            puntoElegido.rotation
        );

        StartCoroutine(TemporizadorDiana());
    }

    IEnumerator TemporizadorDiana()
    {
        yield return new WaitForSeconds(tiempoCambio);

        if (dianaActual != null)
        {
            Destroy(dianaActual);
        }

        yield return new WaitForSeconds(delayRespawn);

        SpawnNuevaDiana();
    }

    public void DianaGolpeada()
    {
        StopAllCoroutines();

        if (dianaActual != null)
        {
            Destroy(dianaActual);
        }

        StartCoroutine(RespawnDiana());
    }

    IEnumerator RespawnDiana()
    {
        yield return new WaitForSeconds(delayRespawn);

        SpawnNuevaDiana();
    }
}