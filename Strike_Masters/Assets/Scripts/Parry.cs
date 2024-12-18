using System.Collections;
using UnityEngine;

public class Parry : MonoBehaviour
{
    public float parryTimeWindow = 1f; // Tiempo en el que el parry puede deshacer el stun (1 segundo)
    private bool isParrying = false; // Si el jugador está haciendo parry
    private bool canParry = true; // Si se puede hacer parry

    void Update()
    {
        // Si el jugador presiona RightControl, y puede hacer parry
        if (Input.GetKeyDown(KeyCode.RightControl) && canParry)
        {
            Debug.Log("PARRY");
            StartCoroutine(TryParry());
        }
    }

    private IEnumerator TryParry()
    {
        // Activar el parry
        isParrying = true;
        canParry = false;

        // Esperamos el tiempo de la ventana de parry para ver si el parry se mantiene
        yield return new WaitForSeconds(parryTimeWindow);

        // Desactivamos el parry
        isParrying = false;

        // Enfriamiento para el parry (puede ser ajustado)
        yield return new WaitForSeconds(parryTimeWindow);
        canParry = true;
    }

    // Método para comprobar si el jugador está haciendo parry
    public bool IsParrying()
    {
        return isParrying;
    }

    // Método para que otros scripts comprueben si puede realizar el parry
    public bool CanParry()
    {
        return canParry;
    }

    // Método que se llama cuando el jugador intenta hacer el parry
    public void AttemptParry()
    {
        if (isParrying)
        {
            // Si se estaba haciendo parry, se puede deshacer el stun
            Debug.Log("Parry realizado");
        }
    }
}
