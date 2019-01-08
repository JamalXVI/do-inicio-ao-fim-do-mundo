using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;
public class Selecao_v4 : MonoBehaviour {
    [SerializeField]
    private AudioClip somSelect;
    [SerializeField]
    private AudioClip somPlay;
    [SerializeField]
    private AudioClip somBack;
    [SerializeField]
    private GameObject conquistaHud;
    [SerializeField]
    private GameObject transicao;
    [SerializeField]
    private Text nomeFaseBranco;
    [SerializeField]
    private Text nomeFasePreto;
    [SerializeField]
    private GameObject config;
    [SerializeField]
    private GameObject loja;
    [SerializeField]
    private GameObject conquistaObjeto;
    [SerializeField]
    private GameObject quizz;
    [SerializeField]
    private GameObject quizzObject;
    [SerializeField]
    private Text textoQuizz;
    [SerializeField]
    private Text pontuacao;
    [SerializeField]
    private Animator linhaDoTempo;
    [SerializeField]
    private GameObject pontuacaoA;
    [SerializeField]
    private GameObject pontuacaoB;
    [SerializeField]
    private GameObject pontuacaoC;
    [SerializeField]
    private GameObject pontuacaoX;
    [SerializeField]
    private string[] nomeFases;
    [SerializeField]
    private int premioQuizzMoedas;
    [SerializeField]
    private int precoQuizz;
    [SerializeField]
    private float sensibilidadeTecla = 0.2f;
    [SerializeField]
    private GameObject certo;
    [SerializeField]
    private GameObject errado;
    private float contadorTecla;
    private bool teclaSensivel = false;
    private Hashtable pontuacoes;
    private List<GameObject> fases;
    private List<GameObject> fases_ativas;
    private List<GameObject> amostraFases;
    private int[] pontuacaoMaxima = new int[9];
    private int[] estadoAtualAnimacaoLinhaDoTempo = new int[9];
    private Hashtable estadosTransicoes;
    private string ultimaConquista = "";
    static int conquistaState = Animator.StringToHash("Conquista.idle");
    static int conquistaStateFinal = Animator.StringToHash("Conquista.Activment");
    private List<Conquista> conquistasNaoAtendidas;
    private int nivelAtual = 0;
    private int nivelMaximo = -1;
    private int indiceQuestaoQuizz = 0;
    private bool acabouQuizz = false;
    private bool acabouAnimacaoResp = false;
    private bool acionouQuestao = false;
    private bool iniciouQuizz = false;
    static int nivel1 = Animator.StringToHash("Selecao.(1)");
    static int nivel2 = Animator.StringToHash("Selecao.(2)");
    static int nivel3 = Animator.StringToHash("Selecao.(3)");
    static int nivel4 = Animator.StringToHash("Selecao.(4)");
    static int nivel5 = Animator.StringToHash("Selecao.(5)");
    static int nivel6 = Animator.StringToHash("Selecao.(6)");
    static int trans12 = Animator.StringToHash("Selecao.1to2");
    static int trans23 = Animator.StringToHash("Selecao.2to3");
    static int trans34 = Animator.StringToHash("Selecao.3to4");
    static int trans45 = Animator.StringToHash("Selecao.4to5");
    static int trans56 = Animator.StringToHash("Selecao.5to6");
    static int trans21 = Animator.StringToHash("Selecao.2to1");
    static int trans32 = Animator.StringToHash("Selecao.3to2");
    static int trans43 = Animator.StringToHash("Selecao.4to3");
    static int trans54 = Animator.StringToHash("Selecao.5to4");
    static int trans65 = Animator.StringToHash("Selecao.6to5");
    static int estadoCerto = Animator.StringToHash("Certo.CertoFim");
    static int estadoErrado = Animator.StringToHash("Errado.ErradoFim");
    private int estadoMundoAnterior;
	// Use this for initialization
    void Start()
    {
        CriarPontuacao();
        CriarConquistasNaoAtendidas();
        VerificarNotificacaoLoja();
        IniciarFases();
	    
	}

    private void IniciarFases()
    {
        fases = new List<GameObject>(GameObject.FindGameObjectsWithTag("Selecao"));
        amostraFases = new List<GameObject>(GameObject.FindGameObjectsWithTag("DescricaoFases"));
        fases_ativas = new List<GameObject>();
        VerificarFasesAtivas();
        AtivarFase(nivelAtual);
    }

    private void CriarConquistasNaoAtendidas()
    {
        var cnat = from f in Game_Conquistas.instancia.conquistas where f.Anunciada == false && f.Completada == true select f;
        conquistasNaoAtendidas = cnat.ToList();
        Transform notificacao = conquistaObjeto.transform.Find("Notific");
        if (conquistasNaoAtendidas.Count > 0)
        {
            notificacao.gameObject.SetActive(true);
        }
        else
        {
            notificacao.gameObject.SetActive(false);
        }
    }
    private void VerificarNotificacaoLoja()
    {
        Transform notificacao = loja.transform.Find("Notific");
        if (Game_Temp.instancia.NotificacaoLoja)
        {
            notificacao.gameObject.SetActive(true);
        }
        else
        {

            notificacao.gameObject.SetActive(false);
        }
    }
    private void CriarPontuacao()
    {
        pontuacaoMaxima[0] = ConstantesDoSistema.PotuacaoMaximaFase01;
        pontuacaoMaxima[1] = ConstantesDoSistema.PotuacaoMaximaFase02;
        pontuacaoMaxima[2] = ConstantesDoSistema.PotuacaoMaximaFase03;
        pontuacaoMaxima[3] = ConstantesDoSistema.PotuacaoMaximaFase04;
        pontuacaoMaxima[4] = ConstantesDoSistema.PotuacaoMaximaFase05;
        pontuacaoMaxima[5] = ConstantesDoSistema.PotuacaoMaximaFase06;
        estadosTransicoes = new Hashtable();
        estadoAtualAnimacaoLinhaDoTempo[0] = nivel1;
        estadoAtualAnimacaoLinhaDoTempo[1] = nivel2;
        estadoAtualAnimacaoLinhaDoTempo[2] = nivel3;
        estadoAtualAnimacaoLinhaDoTempo[3] = nivel4;
        estadoAtualAnimacaoLinhaDoTempo[4] = nivel5;
        estadoAtualAnimacaoLinhaDoTempo[5] = nivel6;
        estadosTransicoes[estadoAtualAnimacaoLinhaDoTempo[1].ToString() + "F"] = trans12;
        estadosTransicoes[estadoAtualAnimacaoLinhaDoTempo[2].ToString() + "F"] = trans23;
        estadosTransicoes[estadoAtualAnimacaoLinhaDoTempo[3].ToString() + "F"] = trans34;
        estadosTransicoes[estadoAtualAnimacaoLinhaDoTempo[4].ToString() + "F"] = trans45;
        estadosTransicoes[estadoAtualAnimacaoLinhaDoTempo[5].ToString() + "F"] = trans56;
        estadosTransicoes[estadoAtualAnimacaoLinhaDoTempo[0].ToString() + "B"] = trans21;
        estadosTransicoes[estadoAtualAnimacaoLinhaDoTempo[1].ToString() + "B"] = trans32;
        estadosTransicoes[estadoAtualAnimacaoLinhaDoTempo[2].ToString() + "B"] = trans43;
        estadosTransicoes[estadoAtualAnimacaoLinhaDoTempo[3].ToString() + "B"] = trans54;
        estadosTransicoes[estadoAtualAnimacaoLinhaDoTempo[4].ToString() + "B"] = trans65;
        pontuacoes = new Hashtable();
        pontuacoes.Add("A", pontuacaoA);
        pontuacoes.Add("B", pontuacaoB);
        pontuacoes.Add("C", pontuacaoC);
        pontuacoes.Add("X", pontuacaoX);
    }
	
	// Update is called once per frame
    void Update()
    {
        
        VerificarConquistas();
        VerificarTeclas();
        VerificarMudancaFase();
        if (certo.active == true)
        {
            Animator animResp = certo.GetComponent<Animator>();
            if (animResp.GetCurrentAnimatorStateInfo(0).nameHash == estadoCerto)
            {
                certo.SetActive(false);
                acabouAnimacaoResp = true;
            }
        }
        if (errado.active == true)
        {
            Animator animResp = errado.GetComponent<Animator>();
            if (animResp.GetCurrentAnimatorStateInfo(0).nameHash == estadoErrado)
            {
                errado.SetActive(false);
                acabouAnimacaoResp = true;
            }
        }
        if (acabouAnimacaoResp && acionouQuestao)
        {
            acionouQuestao = false;
            acabouAnimacaoResp = false;
            ProximaQuestao();
            return;
        }
	}

    private void VerificarMudancaFase()
    {
        if (linhaDoTempo.GetCurrentAnimatorStateInfo(0).nameHash == estadoAtualAnimacaoLinhaDoTempo[nivelAtual]
                && estadoMundoAnterior != estadoAtualAnimacaoLinhaDoTempo[nivelAtual])
        {
            MudarFase();
        }
        else if (estadoMundoAnterior != estadoAtualAnimacaoLinhaDoTempo[nivelAtual])
        {
            MudarFase();
        }
        else if (nomeFaseBranco.text != nomeFases[nivelAtual])
        {
            MudarFase();
        }
    }

    private bool CondicaoEstadoTransicao(int estadoAnimacaoAtualEscolhido)
    {
        if (nivelAtual < 5)
        {
         if (estadoAnimacaoAtualEscolhido == (int)estadosTransicoes[estadoAtualAnimacaoLinhaDoTempo[nivelAtual].ToString() + "B"])
	     {
		    return true;
	     }   
        }
        if (nivelAtual > 0)
        {
            if (estadoAnimacaoAtualEscolhido == (int)estadosTransicoes[estadoAtualAnimacaoLinhaDoTempo[nivelAtual].ToString() + "F"])
	         {
		        return true;
	         }  
        }
        return false;
    }

    private void MudarFase()
    {
        estadoMundoAnterior = estadoAtualAnimacaoLinhaDoTempo[nivelAtual];
        AtivarFase(nivelAtual);
    }

    private void VerificarTeclas()
    {
        if (teclaSensivel)
        {
            EsperarSensibilidadeTecla();
            return;
        }
        if (Input.GetKey(Game_Player.instancia.Teclas["direita"]))
        {
            teclaSensivel = true;
            MoverDireita();
        }
        if (Input.GetKey(Game_Player.instancia.Teclas["esquerda"]))
        {
            teclaSensivel = true;
            MoverEsquerda();
        }
        if (Input.GetAxis("Submit") > 0 || Input.GetKey(Game_Player.instancia.Teclas["habilidade"]))
        {
            teclaSensivel = true;
            SelecionarCenario();
        }
        
    }

    private void EsperarSensibilidadeTecla()
    {
        contadorTecla += Time.deltaTime;
        if (contadorTecla >= sensibilidadeTecla)
        {
            contadorTecla = 0;
            teclaSensivel = false;
        }
    }
    public void MoverEsquerda()
    {
        Movimentar("prev", -1);

    }
    public void MoverDireita()
    {
        Movimentar("next", 1);
    }
    private void Movimentar(string valor, int add)
    {
        DesativarMovimentosAnteriores();
        SoundManager.instance.PlaySingle(somSelect);
        linhaDoTempo.SetTrigger(valor);
        MudarEscolhaNivel(add);
    }
    private void MudarEscolhaNivel(int add)
    {
        nivelAtual = Math.Max(Math.Min(nivelAtual + add, nivelMaximo), 0);
        
    }
    private void DesativarMovimentosAnteriores()
    {
        linhaDoTempo.SetBool("next", false);
        linhaDoTempo.SetBool("prev", false);
    }
    public void Ok()
    {
        SelecionarCenario();
    }
    public void Voltar()
    {
        if (iniciouQuizz)
        {
            return;
        }
        SoundManager.instance.PlaySingle(somBack);
        MudarDeCena(0);
    }
    private void SelecionarCenario()
    {
        if (iniciouQuizz)
        {
            return;
        }
        SoundManager.instance.PlaySingle(somPlay);
        MudarDeCena(Game_Player.instancia.ordem_fases[nivelAtual]);
    }
    private void VerificarFasesAtivas()
    {
        for (int i = 0; i < Game_Player.instancia.ordem_fases.Length; i++)
        {
            int valor = i == 0 ? 0 : i - 1;
            if (Game_Player.instancia.VerificarSePassouFase(valor) || i == 0)
            {
                linhaDoTempo.SetBool((i + 1).ToString(), true);
                LiberarCadeado(i);
                nivelMaximo++;
            }
            else
            {
                linhaDoTempo.SetBool((i + 1).ToString(), false);
            }
        }
    }
    public void Opcoes()
    {
        if (iniciouQuizz)
        {
            return;
        }
        SoundManager.instance.PlaySingle(somPlay);
        MudarDeCena(13);
    }
    public void Loja()
    {
        if (iniciouQuizz)
        {
            return;
        }
        SoundManager.instance.PlaySingle(somPlay);
        Transform notificacao = loja.transform.Find("Notific");
        notificacao.gameObject.SetActive(false);
        MudarDeCena(17);
    }
    public void AcionarQuizz()
    {
        if (iniciouQuizz)
        {
            return;
        }
        if (Game_Quizz.instancia.NivelQuizz[nivelAtual].Liberado && Game_Player.instancia.Moedas_Jogo >= precoQuizz)
        {
            SoundManager.instance.PlaySingle(somPlay);
            Game_Player.instancia.MoedasGastas += precoQuizz;
            indiceQuestaoQuizz = -1;
            quizzObject.SetActive(true);
            acabouQuizz = false;
            iniciouQuizz = true;
            AdicionarQuizz(nivelAtual, 0, 0);
            if (!Game_Quizz.instancia.NivelQuizz[nivelAtual].Notificado)
            {
                Game_Quizz.instancia.NivelQuizz[nivelAtual].Notificado = true;
                quizz.transform.Find("Notific").gameObject.SetActive(false);

            }
            ProximaQuestao();
        }
        else
        {
            SoundManager.instance.PlaySingle(somBack);
        }
    }
    public void AdicionarQuizz(int level, int acertos, int erros)
    {
        if (!Game_Player.instancia.FornecerInformacoes)
        {
            return;
        }
        WWWForm form = new WWWForm();
        form.AddField("chaveAuth", "65#$%*412tLOK");
        form.AddField("mac", ConstantesDoSistema.RetornarMac());
        form.AddField("nivel_qj", level);
        form.AddField("acertos_qj", acertos);
        form.AddField("erros_qj", erros);
        WWW request = new WWW(ConstantesDoSistema.Endereco + "adicionarQuizzes.php", form);
        StartCoroutine(EsperarResposta(request));
    }
    IEnumerator EsperarResposta(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Game_Player.instancia.EnviarInfoNivel = false;
            Debug.Log("WWW Ok!: " + www.text);
        }
        else
        {
            Game_Player.instancia.EnviarInfoNivel = false;
            Debug.Log("WWW Error: " + www.error);

        }
    }   
    private void ProximaQuestao()
    {
        indiceQuestaoQuizz++;
        Game_Player.instancia.Salvar(ConstantesDoSistema.Caminho);
        if (indiceQuestaoQuizz >= Game_Quizz.instancia.QuestaoQuizz[nivelAtual].Perguntas.Length)
        {
            quizzObject.SetActive(false);
            acabouQuizz = true;
            iniciouQuizz = false;
            
            return;
        }
        textoQuizz.text = Game_Quizz.instancia.QuestaoQuizz[nivelAtual].Perguntas[indiceQuestaoQuizz]
            + "\n A) " + Game_Quizz.instancia.QuestaoQuizz[nivelAtual].RespostaA[indiceQuestaoQuizz]
            + "\n B) " + Game_Quizz.instancia.QuestaoQuizz[nivelAtual].RespostaB[indiceQuestaoQuizz]
            + "\n C) " + Game_Quizz.instancia.QuestaoQuizz[nivelAtual].RespostaC[indiceQuestaoQuizz];
    }
    public void AlternativaA()
    {
        ColocarAlternativaQuizz('A');
    }
    public void AlternativaB()
    {
        ColocarAlternativaQuizz('B');

    }
    public void AlternativaC()
    {
        ColocarAlternativaQuizz('C');

    }
    private void ColocarAlternativaQuizz(char alternativa)
    {
        if (acabouQuizz || acabouAnimacaoResp)
        {
            return;
        }
        Game_Quizz.instancia.NivelQuizz[nivelAtual].Resposta[indiceQuestaoQuizz] = alternativa;
        int valor = -1;
        if (Game_Quizz.instancia.QuestaoQuizz[nivelAtual].RespostaCorreta[indiceQuestaoQuizz].ToString().ToUpper() == alternativa.ToString().ToUpper())
        {
            Game_Quizz.instancia.NivelQuizz[nivelAtual].Acertos++;
            certo.SetActive(true);
            certo.GetComponent<Animator>().Play("Certo");
            acabouAnimacaoResp = false;
            valor = RetornarValorChar(alternativa, valor);
            AdicionarQuizzes(nivelAtual, indiceQuestaoQuizz, 1, 0, valor);
            if (!Game_Quizz.instancia.NivelQuizz[nivelAtual].Acertadas[indiceQuestaoQuizz])
            {
                Game_Quizz.instancia.NivelQuizz[nivelAtual].Acertadas[indiceQuestaoQuizz] = true;
                Game_Player.instancia.AdicionarMoedas(premioQuizzMoedas);

            }
            else
            {
            }
        }
        else
        {
            Game_Quizz.instancia.NivelQuizz[nivelAtual].Erros++;
            errado.SetActive(true);
            errado.GetComponent<Animator>().Play("Errado");
            acabouAnimacaoResp = false;
            valor = RetornarValorChar(alternativa, valor);
            AdicionarQuizzes(nivelAtual, indiceQuestaoQuizz, 0, 1, valor);

        }
        GameObject eventSystem = GameObject.Find("EventSystem");
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        acionouQuestao = true;
        
    }

    private static int RetornarValorChar(char alternativa, int valor)
    {
        if (alternativa == 'A')
        {
            valor = 0;
        }
        else if (alternativa == 'B')
        {
            valor = 1;
        }
        else
        {
            valor = 2;
        }
        return valor;
    }
    public void AdicionarQuizzes(int level, int indice, int acertos, int erros, int resposta)
    {
        if (!Game_Player.instancia.FornecerInformacoes)
        {
            return;
        }
        WWWForm form = new WWWForm();
        form.AddField("chaveAuth", "65#$%*412tLOK");
        form.AddField("mac", ConstantesDoSistema.RetornarMac());
        form.AddField("nivel_qj", level);
        form.AddField("indice_q", indice);
        form.AddField("acertos_q", acertos);
        form.AddField("erros_q", erros);
        form.AddField("resposta_q", resposta);
        WWW request = new WWW(ConstantesDoSistema.Endereco + "adicionarQuizz.php", form);
        StartCoroutine(EsperarResposta(request));
    }
    public void IrParaConquistas()
    {
        SoundManager.instance.PlaySingle(somPlay);
        MudarDeCena(Game_Player.instancia.faseConquista);
    }
    private void MudarDeCena(int nivel)
    {
        Game_Temp.instancia.TrocarFase = true;
        Game_Temp.instancia.FaseTroca = nivel;
        transicao.SetActive(true);
        //Application.LoadLevel(Game_Player.game_player.carregando);
    }
    private void AtivarFase(int i)
    {
        string nome = (i + 1) < 10 ? "0" + (i + 1).ToString() : (i + 1).ToString();
        int j = (i + 1) % 3 == 0 ? 3 : (i + 1) % 3;
        int k = Convert.ToInt32(Convert.ToDouble((i) / 3));

        AtivarAmostraFase(k, j);
        Nivel nivel = null;
        try
        {
            nivel = Game_Player.instancia.RetornarNivel(Game_Player.instancia.ordem_fases[i]);
        }catch(Exception e)
        {

        }
        if (nivel != null)
        {
            pontuacao.text = "Pontuação: " + nivel.Pontuacao;
            MostrarPontuacao(nivel.Pontuacao, pontuacaoMaxima[i]);
        }
        else
        {
            pontuacao.text = "Ainda não há registros neste nível!";
            ativarLetra("X");
        }
        if (Game_Quizz.instancia.NivelQuizz[i].Liberado && !Game_Quizz.instancia.NivelQuizz[i].Notificado)
        {
            quizz.transform.Find("Notific").gameObject.SetActive(true);
        }
        else
        {
            quizz.transform.Find("Notific").gameObject.SetActive(false);
        }
        nomeFaseBranco.text = nomeFases[i].ToUpper();
        nomeFasePreto.text = nomeFases[i].ToUpper();

    }
    private void AtivarAmostraFase(int k, int j)
    {
        foreach (var fase in amostraFases)
        {
            if (fase.name.Contains((k + 1).ToString() + "-" + (j).ToString()))
            {
                fase.SetActive(true);
            }
            else
            {
                fase.SetActive(false);
            }
        }
    }
    private void LiberarCadeado(int i)
    {
        int j = (i + 1) % 3 == 0 ? 3 : (i + 1) % 3;
        int k = Convert.ToInt32(Convert.ToDouble((i) / 3));
        var filtro = from f in fases where f.name.Contains((k + 1).ToString() + "-" + (j).ToString()) select f;
        filtro.First<GameObject>().transform.Find("Block").gameObject.SetActive(false);
    }
    private void MostrarPontuacao(float pontuacao, float pontuacaoMaxima)
    {
        float pontos = pontuacao*100 / pontuacaoMaxima;
        if (pontos >= 80)
        {
            ativarLetra("A");
        }
        else if (pontos >= 60)
        {
            ativarLetra("B");
        }
        else
        {
            ativarLetra("C");
        }
    }
    private void ativarLetra(string letra)
    {
        foreach (string chave in pontuacoes.Keys)
        {
            GameObject objeto= (GameObject)pontuacoes[chave];
            if (chave.Equals(letra))
            {
                objeto.SetActive(true);
            }
            else
            {
                objeto.SetActive(false);
            }
        }
    }
   
    private void VerificarConquistas()
    {
        Animator animConquista = conquistaHud.GetComponent<Animator>();
        bool achouConquista = false;
        Conquista conquistaSel = new Conquista();
        
        if (animConquista.GetBool("Conquista") || animConquista.GetCurrentAnimatorStateInfo(0).nameHash != conquistaState)
        {
            if (animConquista.GetCurrentAnimatorStateInfo(0).nameHash == conquistaStateFinal)
            {
                ultimaConquista = "";
                animConquista.SetBool("Conquista", false);

            }
            return;
        }
        foreach (var conquista in conquistasNaoAtendidas)
        {
            if (ultimaConquista.Equals("") && conquista.Completada && !achouConquista && !conquista.Anunciada)
            {
                achouConquista = true;
                conquista.Anunciada = true;
                ultimaConquista = conquista.Nome;
                conquistaSel = conquista;
                animConquista.SetBool("Conquista", true);
                var painel = conquistaHud.transform.Find("Panel");
                painel.Find("Titulo").GetComponent<Text>().text = conquista.Nome;
                painel.Find("Descrição").GetComponent<Text>().text = conquista.Descricao;
                return;
            }
        }
    }
}
