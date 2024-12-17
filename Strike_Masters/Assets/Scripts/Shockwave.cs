using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveSkill : MonoBehaviour
{
    public float shockwaveRadius = 5f;
    public float shockwaveForce = 10f;
    public float cooldownTime = 5f;
    private bool canUseShockwave = true;

    public GameObject shockwaveEffectPrefab;
    private GameObject player;

    public float shockwaveCooldown = 5f;
    public float stunDuration = 2f;

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
        if (shockwaveEffectPrefab != null)
        {
            Instantiate(shockwaveEffectPrefab, transform.position, Quaternion.identity);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, shockwaveRadius);

        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Player"))
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 forceDirection = nearbyObject.transform.position - transform.position;
                    forceDirection.y = 0f;
                    rb.AddForce(forceDirection.normalized * shockwaveForce, ForceMode.Impulse);
                }
                PlayerMovement player = nearbyObject.GetComponent<PlayerMovement>();
                if (player != null && nearbyObject.gameObject != this.gameObject)
                {
                    StartCoroutine(player.ApplyStun(stunDuration));
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
