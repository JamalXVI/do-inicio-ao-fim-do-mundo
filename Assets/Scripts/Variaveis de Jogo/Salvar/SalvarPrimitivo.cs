using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class SalvarPrimitivo {
    public float duracao_slide;
    private float contador_slide;
    public Habilidade[] habilidades;
    private bool ativo = false;

    public bool Ativo
    {
        get { return ativo; }
        set { ativo = value; }
    }

    public float Contador_Slide
    {
        get { return contador_slide; }
        set { contador_slide = value; }
    }

    private bool pode_parar_slide;
    public bool Pode_Parar_Slide
    {
        get { return pode_parar_slide; }
        set { pode_parar_slide = value; }
    }
    
}
