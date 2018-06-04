using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
[Serializable]
public class Nivel : ICloneable{
	
	#region ICloneable Members
    private int vidas;

    public int Numero_Vidas
    {
        get { return vidas; }
        set { vidas = value; }
    }
    private int moedas;

    public int Numero_Moedas
    {
        get { return moedas; }
        set { moedas = value; }
    }
    private int itens;

    public int Numero_Itens
    {
        get { return itens; }
        set { itens = value; }
    }
    private int inimigos;

    public int Numero_Inimigos
    {
        get { return inimigos; }
        set { inimigos = value; }
    }
    private int pontuacao;

    public int Pontuacao
    {
        get { return pontuacao; }
        set { pontuacao = value; }
    }
    private int vezes_jogadas;

    public int Vezes_Jogadas
    {
        get { return vezes_jogadas; }
        set { vezes_jogadas = value; }
    }
    private float tempoRestante;

    public float TempoRestante
    {
        get { return tempoRestante; }
        set { tempoRestante = value; }
    }
    
    
    
	// Use this for initialization
	void Start () {
        /*
        itens = 0;
        vidas = 0;
        moedas = 0;
        pontuacao = 0;
        inimigos = 0;
        vezes_jogadas = 0;
         */
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public object Clone()
	{
		return this.MemberwiseClone();
	}
	
	#endregion
}
