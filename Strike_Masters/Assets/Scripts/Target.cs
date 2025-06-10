using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int scoreValue = 10;
    public bool isMoving = false;
    public Vector3 moveDirection = Vector3.right;
    public float moveSpeed = 2f;
    public bool isTimed = false;
    public float toggleInterval = 2f;

    public float minDuration = 1f;
    public float maxDuration = 3f;
    public TargetSpawner spawner;

    private void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.TargetDestroyed();
        }
    }
    private void Start()
    {
        isMoving = Random.value < 0.5f;

        if (isMoving)
        {
            if (Random.value < 0.5f)
            {
                moveDirection = Vector3.right;
            }
            else
            {
                moveDirection = Vector3.forward;
            }

            moveSpeed = Random.Range(1f, 3f);
        }

        if (isTimed)
            StartCoroutine(ToggleVisibility());

        float duration = Random.Range(minDuration, maxDuration);
        StartCoroutine(DestroyAfterSeconds(duration));
    }
    private void Update()
    {
        if (isMoving)
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            BallHolder ball = other.GetComponent<BallHolder>();
            if (ball != null && ball.lastShooter != null)
            {
                PlayerMovement shooter = ball.lastShooter.GetComponent<PlayerMovement>();
                if (shooter != null)
                {
                    ManagerTarget.Instance.TargetHit(shooter.isPlayer2 ? 2 : 1);
                    Debug.Log("HIT");            
                    if (spawner != null)
                    {
                        spawner.PlayHitSound();
                    }
                }
            }

            Destroy(gameObject);
        }
    }

    IEnumerator ToggleVisibility()
    {
        while (true)
        {
            yield return new WaitForSeconds(toggleInterval);
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }

    IEnumerator DestroyAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
