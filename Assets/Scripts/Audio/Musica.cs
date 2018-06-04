using UnityEngine;
using System.Collections;

public class Musica : MonoBehaviour {

    [SerializeField]
    private AudioClip musica;
    [SerializeField]
    private int idMusica;
	// Use this for initialization
	void Awake ()
    {
        SoundManager.instance.PlayMusic(musica);
        Game_Temp.instancia.IdMusicaLiberar = idMusica;
    }
	
	// Update is called once per frame
	void Update () {
	}
}
