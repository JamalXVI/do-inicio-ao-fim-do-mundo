using UnityEngine;
using System.Collections;

public class Instrucao : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Iniciar()
    {
        Application.LoadLevel(Game_Player.instancia.troca_fases);
    }
}
