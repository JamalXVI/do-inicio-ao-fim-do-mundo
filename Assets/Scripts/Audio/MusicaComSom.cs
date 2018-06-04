using UnityEngine;
using System.Collections;

public class MusicaComSom : MonoBehaviour {
    [SerializeField]
    private AudioClip musica;
    [SerializeField]
    private float pitch;
    // Use this for initialization
    void Awake()
    {
        SoundManager.instance.PlayMusicWithPitch(musica, pitch);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
