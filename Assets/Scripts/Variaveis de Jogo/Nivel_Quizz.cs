using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Nivel_Quizz : ICloneable
{
    #region ICloneable Members
    private bool notificado;

    public bool Notificado
    {
        get { return notificado; }
        set { notificado = value; }
    }
    
    private int acertos;

    public int Acertos
    {
        get { return acertos; }
        set { acertos = value; }
    }
    private bool[] acertadas;

    public bool[] Acertadas
    {
        get { return acertadas; }
        set { acertadas = value; }
    }
    
    private char[] resposta;

    public char[] Resposta
    {
        get { return resposta; }
        set { resposta = value; }
    }

    private int erros;

    public int Erros
    {
        get { return erros; }
        set { erros = value; }
    }
    
    private bool liberado;

    public bool Liberado
    {
        get { return liberado; }
        set { liberado = value; }
    }

    // Use this for initialization
    void Start () {
	
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
