using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour
{
    public float barrierDuration = 3f;
    public float barrierCooldown = 5f;
    private bool canUseBarrier = true;

    public GameObject barrierPrefab;
    public Transform player;
    private GameObject currentShield;

    public Image barrierCooldownImage;
    private float barrierCooldownTimer = 0f;
    public AudioClip cooldownReadySound;
    public AudioClip ability;
    private AudioSource audioSource;
    public ParticleSystem barrierEffect;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canUseBarrier)
        {
            audioSource.PlayOneShot(ability);
            ActivateBarrier();
        }
        if (!canUseBarrier)
        {
            barrierCooldownTimer -= Time.deltaTime;
            barrierCooldownImage.fillAmount = 1f - (barrierCooldownTimer / barrierCooldown);

            if (barrierCooldownTimer <= 0f)
            {
                canUseBarrier = true;
                barrierCooldownImage.fillAmount = 1f; 
                
                if (cooldownReadySound != null)
                {
                    audioSource.PlayOneShot(cooldownReadySound);
                }
            }
        }
    }

    void ActivateBarrier()
    {
        barrierEffect.Play();
        StartCoroutine(BarrierTimer());
        StartCoroutine(BarrierCooldown());
    }

    IEnumerator BarrierTimer()
    {
        currentShield = Instantiate(barrierPrefab, player.position, player.rotation * Quaternion.Euler(0, 90, 0));
        yield return new WaitForSeconds(barrierDuration);
        Destroy(currentShield);
    }

    IEnumerator BarrierCooldown()
    {
        canUseBarrier = false;
        barrierCooldownTimer = barrierCooldown;
        barrierCooldownImage.fillAmount = 0f;
        yield return new WaitForSeconds(barrierCooldown);
    }
}
