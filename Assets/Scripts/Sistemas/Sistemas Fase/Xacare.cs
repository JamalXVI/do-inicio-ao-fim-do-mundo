using UnityEngine;
using System.Collections;
using System;

public class Xacare : MonoBehaviour {
    private Animator anim;
    [SerializeField]
    private float tempoSubida;
    [SerializeField]
    private float tempoDescida;
    private float contador;
    private bool estadoSubida;
    [SerializeField]
    private float delay;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        contador = -delay;
        estadoSubida = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (estadoSubida)
        {
            Descer();
        }
        else {
            Subir();
        }
	}

    private void Descer()
    {
        if (VerificarTempo(tempoDescida))
        {
            MudarEstado(false);
        }
    }

    private void Subir()
    {
        if (VerificarTempo(tempoSubida))
        {
            MudarEstado(true);
        }
    }

    private void MudarEstado(bool estado)
    {
        estadoSubida = estado;
        anim.SetBool("Afundado", estadoSubida);
    }

    private bool VerificarTempo(float contagem)
    {
        contador += Time.deltaTime;
        if (contador >= contagem)
        {
            contador = 0f;
            return true;
        }
        return false;
    }
}
