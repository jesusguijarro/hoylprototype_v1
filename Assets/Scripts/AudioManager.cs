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
    private bool isMusicPaused = false; // Bandera para saber si la m�sica est� pausada

    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    // M�todo para pausar la m�sica principal y reproducir un clip temporal
    public void PlayTemporaryMusic(AudioClip tempClip)
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause(); // Pausa la m�sica de fondo
            isMusicPaused = true; // Marca que la m�sica est� pausada
        }

        // Crear un AudioSource temporal
        GameObject tempObject = new GameObject("TempAudioSource");
        tempSource = tempObject.AddComponent<AudioSource>();
        tempSource.clip = tempClip;
        tempSource.Play();

        // Inicia una coroutine para reanudar la m�sica principal
        StartCoroutine(WaitForTempMusicToEnd(tempObject));
    }

    // Coroutine para esperar que termine la m�sica temporal
    private IEnumerator WaitForTempMusicToEnd(GameObject tempObject)
    {
        while (tempSource.isPlaying)
        {
            yield return null; // Espera mientras se reproduce
        }

        Destroy(tempObject); // Destruye el objeto temporal

        // Reanuda la m�sica principal si estaba pausada
        if (isMusicPaused)
        {
            musicSource.Play();
            isMusicPaused = false; // Marca que la m�sica ya no est� pausada
        }
    }

    // M�todo para pausar la m�sica principal
    public void PauseMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
            isMusicPaused = true; // Marca que la m�sica est� pausada
        }
    }

    // M�todo para reanudar la m�sica principal
    public void ResumeMusic()
    {
        if (isMusicPaused)
        {
            musicSource.Play();
            isMusicPaused = false; // Marca que la m�sica ya no est� pausada
        }
    }

    // M�todo para reproducir efectos de sonido (sin cambios)
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
