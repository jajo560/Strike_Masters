using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string horizontalInput = "Horizontal";
    public string verticalInput = "Vertical";
    public KeyCode kickKey = KeyCode.Space;
    public float speed = 5f;
    public float kickForce = 10f;
    private bool isStunned = false;
    public float rotationSpeedWhileStunned = 70f;
    public bool isStrongKick = false;
    private Rigidbody rbPlayer;
    private Quaternion lastRotation;
    private Vector3 movement;
    public Animator animator;
    public AudioClip kickSound;
    private AudioSource audioSource;
    public bool isPlayer2 = false;
    public BallHolder ballHolder;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        lastRotation = transform.rotation;
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (!GameManager.Instance.isMatchStarted) return;
        if (!isStunned)
        {
            if (isPlayer2)
            {
                isStrongKick = Input.GetKey(KeyCode.RightShift);
            }

            float horizontal = Input.GetAxisRaw(horizontalInput);
            float vertical = Input.GetAxisRaw(verticalInput);

            movement = new Vector3(horizontal, 0, vertical).normalized;

            rbPlayer.velocity += movement * speed * Time.deltaTime;

            if (movement.magnitude > 0.1f)
            {
                if (animator != null)
                {
                    animator.SetBool("isRunning", true);
                }
                lastRotation = Quaternion.LookRotation(movement);
            }
            else
            {
                if (animator != null)
                {
                    animator.SetBool("isRunning", false);
                }
            }
            transform.rotation = lastRotation;

            // Verifica si el balón está poseído y si el jugador presiona la tecla de patada
            if (ballHolder != null && ballHolder.isPossessed && ballHolder.currentPlayer == gameObject)
            {
                if (Input.GetKeyDown(kickKey))
                {
                    KickBall();
                    if (kickSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(kickSound);
                    }
                    Debug.Log("CHUT");
                }
            }
        }
        else
        {
            transform.Rotate(0, rotationSpeedWhileStunned * Time.deltaTime, 0);
        }
    }

    void KickBall()
    {
        if (ballHolder != null)
        {
            if (animator != null)
            {
                animator.SetBool("isShooting", true);
                StartCoroutine(ResetShootingAnim());
            }

            Vector3 kickDirection = transform.forward;
            ballHolder.KickTheBall(kickDirection, kickForce, isStrongKick);
        }
    }

    IEnumerator ResetShootingAnim()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isShooting", false);
    }

    public IEnumerator ApplyStun(float duration)
    {
        isStunned = true;
        rbPlayer.isKinematic = true;
        yield return new WaitForSeconds(duration);
        rbPlayer.isKinematic = false;
        isStunned = false;
    }
}
