using UnityEngine;
using System.Collections;

public class MenuUnPause : MonoBehaviour {
    private Gerenciador gerenciador;
	// Use this for initialization
	void Awake () {
        gerenciador = GameObject.FindObjectOfType<Gerenciador>();
	}
	
	// Update is called once per frame
	public void Unpause () {
        gerenciador.Pausar_Jogo = false;
	}
    public void Sair()
    {
        Application.Quit();
    }
}
