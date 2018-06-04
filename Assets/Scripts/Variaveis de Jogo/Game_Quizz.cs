using UnityEngine;
using System.Collections;

public class Game_Quizz : MonoBehaviour {
    public static Game_Quizz instancia;
    private Nivel_Quizz[] nivelQuizz;
    [SerializeField]
    private QuestoesQuizz[] questaoQuizz;
    private QuestoesEscritasQuizz[] questoesEscritas;
    public QuestoesEscritasQuizz[] QuestaoQuizz
    {
        get { return questoesEscritas; }
        set { questoesEscritas = value; }
    }

    public Nivel_Quizz[] NivelQuizz
    {
        get { return nivelQuizz; }
        set { nivelQuizz = value; }
    }


    // Use this for initialization

    void Awake()
    {
        VerificarSeJaExiste();

        Game_Quizz.instancia.NivelQuizz = new Nivel_Quizz[6];
        questoesEscritas = new QuestoesEscritasQuizz[questaoQuizz.Length];
        for (int i = 0; i < questaoQuizz.Length; i++)
        {
            questoesEscritas[i] = new QuestoesEscritasQuizz();
            questoesEscritas[i].Perguntas = questaoQuizz[i].Perguntas;
            questoesEscritas[i].RespostaA = questaoQuizz[i].RespostaA;
            questoesEscritas[i].RespostaB = questaoQuizz[i].RespostaB;
            questoesEscritas[i].RespostaC = questaoQuizz[i].RespostaC;
            questoesEscritas[i].RespostaCorreta = questaoQuizz[i].RespostaCorreta;
        }
        for (int i = 0; i < 6; i++ )
        {
            Game_Quizz.instancia.NivelQuizz[i] = new Nivel_Quizz();
            Nivel_Quizz nv = Game_Quizz.instancia.NivelQuizz[i];
            nv.Resposta = new char[999];
            nv.Acertadas = new bool[999];
            nv.Acertos = 0;
            nv.Erros = 0;
            nv.Liberado = false;
            nv.Notificado = false;
        }
    }


        // Update is called once per frame
    void Update () {

    }
    
    private void VerificarSeJaExiste()
    {
        if (instancia == null)
            instancia = this;
        else if (instancia != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

}
