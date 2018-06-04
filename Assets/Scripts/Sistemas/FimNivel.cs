using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class FimNivel : MonoBehaviour {
    public Image imagem;
    private bool escurecer = false;
    private Gerenciador gerenciador;
	// Use this for initialization
	void Start () {
        gerenciador = GameObject.FindObjectOfType<Gerenciador>();
	}
	
	// Update is called once per frame
	void Update () {
        if (escurecer)
        {
            Color32 cor = imagem.color;
            if (128 >= cor.a)
            {
                cor.a++;
                imagem.color = cor;
            }
            else
            {
                
            }
        }
	}
    void OnTriggerEnter2D(Collider2D colisor)
    {
        if (colisor.tag == "Player")
        {
            escurecer = true;
            gerenciador.parar_jogador = true;
        }
    }
}
