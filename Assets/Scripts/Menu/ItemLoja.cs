using UnityEngine;
using System.Collections;
using System;
[Serializable]
public class ItemLoja{
  //  private string nome;
  //  private string descricao;
  //  private int preco;
  //  private bool comprado;
  //  private bool liberado;
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
    private int preco;

    public int Preco
    {
        get { return preco; }
        set { preco = value; }
    }
    private bool comprado;

    public bool Comprado
    {
        get { return comprado; }
        set { comprado = value; }
    }
    private bool liberado;

    public bool Liberado
    {
        get { return liberado; }
        set { liberado = value; }
    }
    private int tipo;

    public int Tipo
    {
        get { return tipo; }
        set { tipo = value; }
    }

    private int id;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    
    
    
}
