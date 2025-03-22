using UnityEngine;

public class FadeInMusic : MonoBehaviour
{
    [Header("Music Settings")]
    [SerializeField] private AudioSource audioSource; // The audio source to fade in
    [SerializeField] private float fadeDuration = 3f; // Duration for the fade-in effect

    private float targetVolume = 0.15f; // The target volume after the fade-in
    private float initialVolume = 0f; // The initial volume before fade-in starts

    void Start()
    {
        // Ensure the audio starts with no volume (silent)
        audioSource.volume = 0f;
        audioSource.Play();

        // Start the fade-in process
        StartCoroutine(FadeInMusicCoroutine());
    }

    private System.Collections.IEnumerator FadeInMusicCoroutine()
    {
        float elapsedTime = 0f;

        // Gradually increase the volume from 0 to target volume
        while (elapsedTime < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(initialVolume, targetVolume, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the volume is set to the target volume after the fade is complete
        audioSource.volume = targetVolume;
    }
}
