using UnityEngine;
using System.Collections;
using System;
public class Socolinha : MonoBehaviour {


    public float tempo_entre_notas;
    public float duracao_habilidade_musica;
    public float duracao_paralisado;
    public GameObject habilidade_Musica;
    public Habilidade[] habilidades;
    private float duracao_musica_total = 0f;
    private bool ativo;

    public bool Ativo
    {
        get { return ativo; }
        set { ativo = value; }
    }
    

    public float Duracao_Musica_Total
    {
        get { return duracao_musica_total; }
        set { duracao_musica_total = value; }
    }

    private float duracao_musica_parcial = 0f;

    public float Duracao_Musica_Parcial
    {
        get { return duracao_musica_parcial; }
        set { duracao_musica_parcial = value; }
    }
    
}
