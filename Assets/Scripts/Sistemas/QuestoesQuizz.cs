using UnityEngine;
using System.Collections;

public class QuestoesQuizz : MonoBehaviour
{
    [SerializeField]
    private string[] perguntas;

    public string[] Perguntas
    {
        get { return perguntas; }
        set { perguntas = value; }
    }

    [SerializeField]
    private string[] respostaA;

    public string[] RespostaA
    {
        get { return respostaA; }
        set { respostaA = value; }
    }

    [SerializeField]
    private string[] respostaB;

    public string[] RespostaB
    {
        get { return respostaB; }
        set { respostaB = value; }
    }
    [SerializeField]
    private string[] respostaC;

    public string[] RespostaC
    {
        get { return respostaC; }
        set { respostaC = value; }
    }
    [SerializeField]
    private char[] respostaCorreta;

    public char[] RespostaCorreta
    {
        get { return respostaCorreta; }
        set { respostaCorreta = value; }
    }


}
