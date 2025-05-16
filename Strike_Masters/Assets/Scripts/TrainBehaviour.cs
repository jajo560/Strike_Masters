using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainBehaviour : MonoBehaviour
{
    public float trainSpeed = 20f;
    public float stunDuration = 2f;
    public float pushForce = 15f;
    public float destroyAfterSeconds = 10f;

    public AudioClip[] spawnSounds;
    private static int lastPlayedIndex = -1;

    private void Start()
    {
        PlayRandomSpawnSound();
        Destroy(gameObject, destroyAfterSeconds);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * trainSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 pushDir = (other.transform.position - transform.position).normalized;
                pushDir.y = 0;
                rb.AddForce(pushDir * pushForce, ForceMode.Impulse);
            }

            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.StartCoroutine(pm.ApplyStun(stunDuration));
            }
        }
    }

    void PlayRandomSpawnSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null && spawnSounds.Length > 0)
        {
            int newIndex;
            do
            {
                newIndex = Random.Range(0, spawnSounds.Length);
            } while (newIndex == lastPlayedIndex && spawnSounds.Length > 1);

            lastPlayedIndex = newIndex;
            audio.PlayOneShot(spawnSounds[newIndex]);
        }
    }
}

