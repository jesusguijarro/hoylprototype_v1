using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--- Audio Source ---")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("--- Audio Clip ---")]
    public AudioClip background;
    public AudioClip portal;

    private AudioSource tempSource; // AudioSource temporal
    private bool isMusicPaused = false; // Bandera para saber si la música está pausada

    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    // Método para pausar la música principal y reproducir un clip temporal
    public void PlayTemporaryMusic(AudioClip tempClip)
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause(); // Pausa la música de fondo
            isMusicPaused = true; // Marca que la música está pausada
        }

        // Crear un AudioSource temporal
        GameObject tempObject = new GameObject("TempAudioSource");
        tempSource = tempObject.AddComponent<AudioSource>();
        tempSource.clip = tempClip;
        tempSource.Play();

        // Inicia una coroutine para reanudar la música principal
        StartCoroutine(WaitForTempMusicToEnd(tempObject));
    }

    // Coroutine para esperar que termine la música temporal
    private IEnumerator WaitForTempMusicToEnd(GameObject tempObject)
    {
        while (tempSource.isPlaying)
        {
            yield return null; // Espera mientras se reproduce
        }

        Destroy(tempObject); // Destruye el objeto temporal

        // Reanuda la música principal si estaba pausada
        if (isMusicPaused)
        {
            musicSource.Play();
            isMusicPaused = false; // Marca que la música ya no está pausada
        }
    }

    // Método para pausar la música principal
    public void PauseMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
            isMusicPaused = true; // Marca que la música está pausada
        }
    }

    // Método para reanudar la música principal
    public void ResumeMusic()
    {
        if (isMusicPaused)
        {
            musicSource.Play();
            isMusicPaused = false; // Marca que la música ya no está pausada
        }
    }

    // Método para reproducir efectos de sonido (sin cambios)
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
