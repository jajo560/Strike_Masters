using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Parry : MonoBehaviour
{
    public float parryTimeWindow = 1f;
    public float parryCooldown = 3f;
    public Image cooldownImage;

    private bool isParrying = false;
    private bool canParry = true;
    private float cooldownTimer = 0f;
    public AudioClip cooldownReadySound;
    private AudioSource audioSource;
    public ParticleSystem barrierEffect;
    public AudioClip parrySound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Period) && canParry)
        {
            barrierEffect.Play();
            audioSource.PlayOneShot(parrySound);

            Debug.Log("PARRY");
            StartCoroutine(TryParry());
        }

        if (!canParry)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownImage.fillAmount = 1f - (cooldownTimer / parryCooldown); 

        }
    }

    private IEnumerator TryParry()
    {
        isParrying = true;
        canParry = false;

        yield return new WaitForSeconds(parryTimeWindow);
        cooldownTimer = parryCooldown;
        cooldownImage.fillAmount = 0f;
        cooldownImage.enabled = true;
        isParrying = false;

        yield return new WaitForSeconds(parryCooldown);
        if (cooldownReadySound != null)
        {
            audioSource.PlayOneShot(cooldownReadySound);
        }
        canParry = true;
        cooldownImage.fillAmount = 1f;
    }
    public bool IsParrying()
    {
        return isParrying;
    }

}
