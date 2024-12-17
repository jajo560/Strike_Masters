using System.Collections;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public float barrierDuration = 3f;
    public float barrierCooldown = 5f;
    private bool canUseBarrier = true;

    public GameObject barrierPrefab;
    public Transform player;
    private GameObject currentShield;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canUseBarrier)
        {
            ActivateBarrier();
        }
    }
    void ActivateBarrier()
    {
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
        yield return new WaitForSeconds(barrierCooldown);
        canUseBarrier = true;
    }
}
