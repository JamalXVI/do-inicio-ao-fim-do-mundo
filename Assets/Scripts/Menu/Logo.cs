using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour {
    static int fim = Animator.StringToHash("Logo.EstadoFinal");
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        if (PlayerPrefs.HasKey("resolucao"))
        {
            string opcao = PlayerPrefs.GetString("resolucao");
            bool ligado = PlayerPrefs.GetInt("fullscreen") == 1 ? true : false;
            Screen.SetResolution(int.Parse(opcao.Split('x')[0]), int.Parse(opcao.Split('x')[1]), ligado);
        }
        if (!PlayerPrefs.HasKey("volumeSom"))
        {
            PlayerPrefs.SetFloat("volumeSom", 1f);
        }
        if (!PlayerPrefs.HasKey("volumeMusica"))
        {
            PlayerPrefs.SetFloat("volumeMusica", 1f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (anim.GetCurrentAnimatorStateInfo(0).nameHash == fim)
        {
            Application.LoadLevel(12);
        }
	}
}
