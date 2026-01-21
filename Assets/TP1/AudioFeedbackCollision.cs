using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioFeedbackCollision : MonoBehaviour
{
    private AudioSource audioSource;

    [Tooltip("Minimal speed to play sound")]
    public float minVelocity = 0.5f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check impact speed
        if (collision.relativeVelocity.magnitude > minVelocity)
        {
            // Volume proportional to speed
            float volume = Mathf.Clamp01(collision.relativeVelocity.magnitude / 5f);
            audioSource.PlayOneShot(audioSource.clip, volume);
        }
    }
}