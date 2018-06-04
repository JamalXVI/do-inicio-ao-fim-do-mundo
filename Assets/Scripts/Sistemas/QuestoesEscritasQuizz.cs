using UnityEngine;
using System.Collections;

public class QuestoesEscritasQuizz {

    // Use this for initialization[SerializeField]
    private string[] perguntas;

    public string[] Perguntas
    {
        get { return perguntas; }
        set { perguntas = value; }
    }

    private string[] respostaA;

    public string[] RespostaA
    {
        get { return respostaA; }
        set { respostaA = value; }
    }

    private string[] respostaB;

    public string[] RespostaB
    {
        get { return respostaB; }
        set { respostaB = value; }
    }
    private string[] respostaC;

    public string[] RespostaC
    {
        get { return respostaC; }
        set { respostaC = value; }
    }
    private char[] respostaCorreta;

    public char[] RespostaCorreta
    {
        get { return respostaCorreta; }
        set { respostaCorreta = value; }
    }
}
