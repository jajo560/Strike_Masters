using System.Collections;
using UnityEngine;

public class PowerShoot : MonoBehaviour
{
    public BallHolder ballHolder; // Referencia al script BallHolder
    public float normalKickForce = 10f; // Fuerza normal del chut
    private bool isStrongKick;

    public bool isPlayer2 = false; // Identifica si este es el jugador 2

    void Update()
    {
        // Solo el jugador 2 puede hacer el chut fuerte con Right Shift
        if (isPlayer2)
        {
            isStrongKick = Input.GetKey(KeyCode.RightShift); // Si es jugador 2 y se presiona RightShift, se hace un chut fuerte
        }
        else
        {
            isStrongKick = false; // El jugador 1 no puede hacer un chut fuerte
        }

        // Verifica si se presiona RightShift para hacer un chut
        if (Input.GetKeyDown(KeyCode.RightShift) && ballHolder.isPossessed)
        {
            Debug.Log("FUERRTREEE");
            PerformKick();
        }
    }

    void PerformKick()
    {
        if (ballHolder != null && ballHolder.isPossessed)
        {
            // Calculamos la dirección del chut (en este caso, hacia el balón)
            Vector3 kickDirection = transform.forward;
            // Llamamos al método KickTheBall, pasándole si es un chut fuerte o no
            ballHolder.KickTheBall(kickDirection, normalKickForce, isStrongKick);
        }
    }
}
