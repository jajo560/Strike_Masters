using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    public float strongKickCooldown = 3f;
    private float strongKickTimer = 0f;
    private bool canStrongKick = true;

    public Image strongKickCooldownImage;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        lastRotation = transform.rotation;
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (!GameManager.Instance.isMatchStarted) return;

        // Actualizar cooldown visual y lógico
        if (!canStrongKick)
        {
            strongKickTimer -= Time.deltaTime;
            strongKickCooldownImage.fillAmount = 1f - (strongKickTimer / strongKickCooldown);

            if (strongKickTimer <= 0f)
            {
                canStrongKick = true;
                strongKickCooldownImage.fillAmount = 1f;
            }
        }

        if (!isStunned)
        {
            if (isPlayer2 && canStrongKick)
            {
                isStrongKick = Input.GetKey(KeyCode.RightControl);
            }
            else
            {
                isStrongKick = false;
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

                    if (isStrongKick)
                    {
                        canStrongKick = false;
                        strongKickTimer = strongKickCooldown;
                        strongKickCooldownImage.fillAmount = 0f;
                    }
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
