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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Period) && canParry)
        {
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

        cooldownTimer = parryCooldown;
        cooldownImage.fillAmount = 0f;
        cooldownImage.enabled = true;

        yield return new WaitForSeconds(parryTimeWindow);

        isParrying = false;

        yield return new WaitForSeconds(parryCooldown);

        canParry = true;
        cooldownImage.fillAmount = 1f;
    }

    public bool IsParrying()
    {
        return isParrying;
    }

    public bool CanParry()
    {
        return canParry;
    }

    public void AttemptParry()
    {
        if (isParrying)
        {
            Debug.Log("Parry realizado");
        }
    }
}
