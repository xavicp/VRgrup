using System.Collections;
using UnityEngine;

public class ControladorDianas : MonoBehaviour
{
    [Header("Zona de Spawn")]
    public Transform zonaDianas;

    [Header("Prefab")]
    public GameObject prefabDiana;

    [Header("Tiempo")]
    public float tiempoCambio = 3f;

    private GameObject dianaActual;

    void Start()
    {
        SpawnNuevaDiana();
    }

    void SpawnNuevaDiana()
    {
        if (dianaActual != null)
        {
            Destroy(dianaActual);
        }

        Vector3 posicionAleatoria = ObtenerPosicionAleatoria();

        dianaActual = Instantiate(
            prefabDiana,
            posicionAleatoria,
            Quaternion.identity
        );

        StartCoroutine(TemporizadorDiana());
    }

    Vector3 ObtenerPosicionAleatoria()
    {
        Vector3 centro = zonaDianas.position;
        Vector3 escala = zonaDianas.localScale;

        float x = Random.Range(
            centro.x - escala.x / 2f,
            centro.x + escala.x / 2f
        );

        float y = Random.Range(
            centro.y - escala.y / 2f,
            centro.y + escala.y / 2f
        );

        float z = centro.z;

        return new Vector3(x, y, z);
    }

    IEnumerator TemporizadorDiana()
    {
        yield return new WaitForSeconds(tiempoCambio);

        SpawnNuevaDiana();
    }

    public void DianaGolpeada()
    {
        StopAllCoroutines();

        SpawnNuevaDiana();
    }
}