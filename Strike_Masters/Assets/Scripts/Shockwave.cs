using System.Collections;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public float shockwaveRadius = 5f;
    public float shockwaveForce = 10f;
    public float cooldownTime = 5f;
    private bool canUseShockwave = true;

    public GameObject shockwaveEffectPrefab;
    private GameObject player;

    public float shockwaveCooldown = 5f;
    public float stunDuration = 2f;

    // Referencia al script de parry del segundo jugador
    public Parry secondPlayerParryScript;

    private void Start()
    {
        player = this.gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canUseShockwave)
        {
            ActivateShockwave();
        }
    }

    void ActivateShockwave()
    {
        // Crear la animación/efecto del shockwave
        if (shockwaveEffectPrefab != null)
        {
            Instantiate(shockwaveEffectPrefab, transform.position, Quaternion.identity);
        }

        // Verificar los objetos cercanos a través de la esfera de colisión
        Collider[] colliders = Physics.OverlapSphere(transform.position, shockwaveRadius);

        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Player"))
            {
                // Evitar que el primer jugador se aturda a sí mismo
                if (nearbyObject.gameObject == this.gameObject) continue;

                // Obtener el Rigidbody y aplicar la fuerza del shockwave
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 forceDirection = nearbyObject.transform.position - transform.position;
                    forceDirection.y = 0f;
                    rb.AddForce(forceDirection.normalized * shockwaveForce, ForceMode.Impulse);
                }

                // Obtener el componente PlayerMovement
                PlayerMovement playerMovement = nearbyObject.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    // Verificar si el segundo jugador está haciendo parry
                    if (secondPlayerParryScript != null && secondPlayerParryScript.IsParrying())
                    {
                        // Si el segundo jugador está haciendo parry, no aplicar stun
                        Debug.Log("Parry bloquea el stun");
                    }
                    else
                    {
                        // Si no se hace parry, el primer jugador aturde al segundo jugador
                        StartCoroutine(playerMovement.ApplyStun(stunDuration));
                    }
                }
            }
        }

        StartCoroutine(ShockwaveCooldown());
    }

    IEnumerator ShockwaveCooldown()
    {
        canUseShockwave = false;
        yield return new WaitForSeconds(cooldownTime);
        canUseShockwave = true;
    }
}
