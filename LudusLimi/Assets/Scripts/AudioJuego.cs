
using UnityEngine;

public class AudioJuego : MonoBehaviour
{
    public static AudioJuego Instance { get; private set; }

    private AudioSource reproductorEfectos;

    private AudioClip sonidoClick;
    private AudioClip sonidoComer;
    private AudioClip sonidoMenu;
    private AudioClip sonidoSlime;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        reproductorEfectos = gameObject.AddComponent<AudioSource>();
        reproductorEfectos.playOnAwake = false;
        reproductorEfectos.loop = false;
        reproductorEfectos.spatialBlend = 0f;
        reproductorEfectos.volume = 0.8f;

        sonidoClick = Resources.Load<AudioClip>("Audio/click");
        sonidoComer = Resources.Load<AudioClip>("Audio/eat");
        sonidoMenu = Resources.Load<AudioClip>("Audio/menu_open");
        sonidoSlime = Resources.Load<AudioClip>("Audio/slime");
    }

    public void Click()
    {
        Reproducir(sonidoClick);
    }

    public void Comer()
    {
        Reproducir(sonidoComer);
    }

    public void AbrirMenu()
    {
        Reproducir(sonidoMenu);
    }

    public void Slime()
    {
        Reproducir(sonidoSlime);
    }

    private void Reproducir(AudioClip clip)
    {
        if (clip != null && reproductorEfectos != null)
        {
            reproductorEfectos.PlayOneShot(clip);
        }
    }
}
