using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shockwave : MonoBehaviour
{
    public float shockwaveRadius = 5f;
    public float shockwaveForce = 10f;
    public float cooldownTime = 5f;
    private bool canUseShockwave = true;

    public GameObject shockwaveEffectPrefab;
    private GameObject player;

    public float stunDuration = 2f;

    public Parry secondPlayerParryScript;

    [Header("UI Cooldown")]
    public Image shockwaveCooldownImage;
    private float cooldownTimer = 0f;

    private void Start()
    {
        player = this.gameObject;
        if (shockwaveCooldownImage != null)
            shockwaveCooldownImage.fillAmount = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canUseShockwave)
        {
            ActivateShockwave();
        }

        if (!canUseShockwave)
        {
            cooldownTimer -= Time.deltaTime;
            shockwaveCooldownImage.fillAmount = 1f - (cooldownTimer / cooldownTime);

            if (cooldownTimer <= 0f)
            {
                canUseShockwave = true;
                shockwaveCooldownImage.fillAmount = 1f;
            }
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
                if (nearbyObject.gameObject == this.gameObject) continue;

                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 forceDirection = nearbyObject.transform.position - transform.position;
                    forceDirection.y = 0f;
                    rb.AddForce(forceDirection.normalized * shockwaveForce, ForceMode.Impulse);
                }

                PlayerMovement playerMovement = nearbyObject.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    if (secondPlayerParryScript != null && secondPlayerParryScript.IsParrying())
                    {
                        Debug.Log("Parry bloquea el stun");
                    }
                    else
                    {
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
        cooldownTimer = cooldownTime;

        if (shockwaveCooldownImage != null)
            shockwaveCooldownImage.fillAmount = 0f;

        yield return new WaitForSeconds(cooldownTime);
    }
}
