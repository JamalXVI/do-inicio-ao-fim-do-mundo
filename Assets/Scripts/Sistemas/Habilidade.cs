using UnityEngine;
using System.Collections;
using System;
[Serializable]
public class Habilidade{
    public string nome;
    public bool ativado;
    private bool escolhida;

    public bool Escolhida
    {
        get { return escolhida; }
        set { escolhida = value; }
    }
    
}
