using UnityEngine;
using System.Collections;
using System;
[Serializable]
public class Conquista {
    private int id;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    private string nome;

    public string Nome
    {
        get { return nome; }
        set { nome = value; }
    }
    private string descricao;

    public string Descricao
    {
        get { return descricao; }
        set { descricao = value; }
    }

    private bool completada;

    public bool Completada
    {
        get { return completada; }
        set { completada = value; }
    }

    private bool anunciada;

    public bool Anunciada
    {
        get { return anunciada; }
        set { anunciada = value; }
    }
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
