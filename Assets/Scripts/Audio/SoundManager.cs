using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
    public AudioSource efxLoopSource;
    public AudioSource efeitosPersonagens;
    public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.
    private float contadorPararMusica;
    public AudioSource musica;
    private bool fadeOut = false;
    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        efxSource.volume = PlayerPrefs.GetFloat("volumeSom");
        efxLoopSource.volume = PlayerPrefs.GetFloat("volumeSom");
        if (!fadeOut)
        {
            musica.volume = PlayerPrefs.GetFloat("volumeMusica");

        }
        else
        {
            dandoFadeOut();
        }
    }

    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;
        efxSource.Play();
    }
    public void PlayPersonagens(AudioClip clip)
    {
        efeitosPersonagens.clip = clip;
        efeitosPersonagens.Play();
    }
    public void PlayMusic(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        musica.clip = clip;
        musica.playOnAwake = false;
        musica.pitch = 1;
        musica.Play();
    }
    public void PlayMusicWithPitch(AudioClip clip, float pitch)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        musica.clip = clip;
        musica.playOnAwake = false;
        musica.pitch = pitch;
        musica.Play();
    }
    //Used to play single sound clips.
    public void PlayLoop(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxLoopSource.clip = clip;
        efxLoopSource.loop = true;  
        //Play the clip.
        efxLoopSource.Play();
    }
    public void StopLoop()
    {
        efxLoopSource.Stop();
    }
    public void FadeOutMusica()
    {
        fadeOut = true;
         
    }
    private void dandoFadeOut()
    {
        contadorPararMusica += Time.deltaTime;
        if (contadorPararMusica >= 0.15f)
        {
            contadorPararMusica = 0;
            musica.volume -= 0.15f / 1f;
        }
        if (musica.volume <= 0)
        {
            musica.Stop();
            contadorPararMusica = 0;
            fadeOut = false;

        }
    }
    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source to the randomly chosen pitch.
        efxSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        efxSource.clip = clips[randomIndex];

        //Play the clip.
        efxSource.Play();
    }

    public void PararMusica()
    {
        musica.Stop();
    }
}
