using System.Collections;
using UnityEngine;

public class Parry : MonoBehaviour
{
    public float parryTimeWindow = 1f;
    public float parryCooldown = 3f;
    private bool isParrying = false;
    private bool canParry = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Period) && canParry)
        {
            Debug.Log("PARRY");
            StartCoroutine(TryParry());
        }
    }

    private IEnumerator TryParry()
    {
        isParrying = true;
        canParry = false;

        yield return new WaitForSeconds(parryTimeWindow);

        isParrying = false;

        yield return new WaitForSeconds(parryCooldown);
        canParry = true;
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
