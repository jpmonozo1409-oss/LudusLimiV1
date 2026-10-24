
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicaJuego : MonoBehaviour
{
    private AudioSource reproductor;

    private void Awake()
    {
        reproductor = GetComponent<AudioSource>();

        reproductor.playOnAwake = false;
        reproductor.loop = true;
        reproductor.volume = 0.20f;
        reproductor.spatialBlend = 0f;

        AudioClip musica = Resources.Load<AudioClip>(
            "Audio/slime_music"
        );

        if (musica != null)
        {
            reproductor.clip = musica;
            reproductor.Play();
        }
        else
        {
            Debug.LogError(
                "No se encontró Audio/slime_music"
            );
        }
    }
}
