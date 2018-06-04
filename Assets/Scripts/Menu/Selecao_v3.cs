using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using System;
using System.Linq;
public class Selecao_v3 : MonoBehaviour {
    private List<GameObject> fases;
    private List<GameObject> fases_ativas;
    private List<GameObject> descricao_fases;
    private int[] pontuacaoMaxima = new int[999];
    private int estagioFase = 0;
    private Animator animatorMundo;
    private Animator animator;
    private GameObject estagioSelecionado;
    //
    public AudioClip somSelect;
    public AudioClip somPlay;
    public AudioClip somBack;
    private List<GameObject> selecoes;
    private int mundo;
    private bool[] desativadoMundo;
    [SerializeField]
    private GameObject conquistaHud;
    private string ultimaConquista = "";
    static int conquistaState = Animator.StringToHash("Conquista.idle");
    static int conquistaStateFinal = Animator.StringToHash("Conquista.Activment");
    private List<Conquista> conquistasNaoAtendidas;
    [SerializeField]
    private GameObject transicao;
	// Use this for initialization
	void Awake () {
        var cnat = from f in Game_Conquistas.instancia.conquistas where f.Anunciada == false select f;
        conquistasNaoAtendidas = cnat.ToList();
        fases = new List<GameObject>(GameObject.FindGameObjectsWithTag("Selection"));
        descricao_fases = new List<GameObject>(GameObject.FindGameObjectsWithTag("DescricaoFases"));
        fases_ativas = new List<GameObject>();
        animator = this.GetComponent<Animator>();
        animatorMundo = GameObject.Find("Mundos").GetComponent<Animator>();
        pontuacaoMaxima[0] = ConstantesDoSistema.PotuacaoMaximaFase01;
        pontuacaoMaxima[1] = ConstantesDoSistema.PotuacaoMaximaFase02;
        pontuacaoMaxima[2] = ConstantesDoSistema.PotuacaoMaximaFase03;
        pontuacaoMaxima[3] = ConstantesDoSistema.PotuacaoMaximaFase04;
        selecoes = new List<GameObject>(GameObject.FindGameObjectsWithTag("Selecao"));
        mundo = 1;
        desativadoMundo = new bool[selecoes.Count];
        VerificarFasesAtivas();
        AtivarMundoAtual(1);
        MudarMundo(1, true);
        MudarMundo(2, false);
        MudarMundo(3, false);

    }
    private void VerificarFasesAtivas()
    {
        for (int i = 0; i < Game_Player.game_player.ordem_fases.Length; i++)
        {
            int valor = i == 0 ? 0 : i - 1; 
            if (Game_Player.game_player.VerificarSePassouFase(valor) || i == 0)
            {
                try
                {
                    Nivel nivelEsc = Game_Player.game_player.RetornarNivel(Game_Player.game_player.ordem_fases[valor]);
                    if (pontuacaoMaxima[valor] != 0)
                    {
                        float num = 100 * nivelEsc.Pontuacao / pontuacaoMaxima[valor];
                        
                        AtivarEstrela((valor + 1), num, (valor + 1) < 10 ? (valor + 1).ToString() : "0"+(valor+1));
                    }
                }
                catch (Exception e)
                {
                }
                AtivarFase(i);
            }
        }
        InativarFases();
        DesativarMundos();
    }

    private void DesativarMundos()
    {
        for (int i = 1; i <= 3; i++)
        {
            var fases_mundo =
            from f in fases_ativas where Convert.ToInt32(f.name) <= i * 3 select f;
            if (fases_mundo.ToArray().Length <= 0)
            {
                DesativarMundo(i);
            }
        }
    }

    private void DesativarMundo(int i)
    {
        /*
            var valor = i < 10 ? "0" + i : i.ToString();
            GameObject selecionado = GameObject.Find("Mundo" + valor);
            selecionado.SetActive(false);
            */
        desativadoMundo[i] = true;
    }

    private GameObject RetornarMundoSelecionado(int i)
    {
        var selecionados = from s in selecoes where s.name.Contains(i.ToString()) select s;
        return selecionados.First<GameObject>();
    }

    public void AtivarMundoAtual(int i)
    {
        if (desativadoMundo[i])
        {
            return;
        }   
        string valor = i < 10 ? "0" + i : i.ToString();
        animatorMundo.SetBool("01", false);
        animatorMundo.SetBool("02", false);
        animatorMundo.SetBool("03", false);
        RetornarCanvasSelecionado(1).gameObject.SetActive(true);
        RetornarCanvasSelecionado(2).gameObject.SetActive(true);
        RetornarCanvasSelecionado(3).gameObject.SetActive(true);
        foreach (var fase in fases_ativas)
        {
            fase.GetComponentInChildren<Toggle>().isOn = false;
        }
        animatorMundo.SetTrigger(valor);
        mundo = i;

    }

    private void AtualizarFrame()
    {
        if (RetornarCanvasSelecionado(mundo).alpha == 1)
        {
            return;
        }
        for (int j = 1; j <= 3; j++)
        {
            bool ativado = false;
            if (mundo == j)
            {
                ativado = true;
            }
            MudarMundo(j, ativado);
        }
    }
    void FixedUpdate()
    {
        AtualizarFrame();
        VerificarConquistas();
    }
    private void MudarMundo(int i, bool ativado)
    {
        
        CanvasGroup cg = RetornarCanvasSelecionado(i);
        float valorTrans = 0.1f;
        if (!ativado)
	    {
		  valorTrans *= -1;
	    }
        cg.alpha = cg.alpha < 0 ? 0 : cg.alpha > 1 ? 1 : cg.alpha + valorTrans;
        if ((!ativado && cg.alpha == 0) || (ativado && cg.alpha == 1))
        {
            cg.gameObject.SetActive(ativado);
            cg.interactable = ativado;
            cg.blocksRaycasts = ativado;
        }
    }

    private CanvasGroup RetornarCanvasSelecionado(int i)
    {
        GameObject selecionado = RetornarMundoSelecionado(i);
        CanvasGroup cg = selecionado.GetComponent<CanvasGroup>();
        return cg;
    }

    private void InativarFases()
    {
        var fases_inativas = from f in fases where !(fases_ativas.Contains(f)) select f;
        foreach (GameObject f in fases_inativas)
        {
            //f.GetComponentInChildren<Toggle>().enabled = false;
            f.GetComponentInChildren<Toggle>().isOn = false;
        }
    }
    private void AtivarFase(int i)
    {
        string nome = (i + 1) < 10 ? "0" + (i + 1).ToString() : (i + 1).ToString();
        var filtro = from f in fases where f.name.Contains(nome) select f;
        fases_ativas.Add(filtro.First<GameObject>());
        Nivel nivel = Game_Player.game_player.RetornarNivel(Game_Player.game_player.ordem_fases[i]);
        int j = (i+1) % 3 == 0 ? 3 : (i+1) % 3;
        int k = Convert.ToInt32(Convert.ToDouble((i) / 3));
        var descricaoFases = from d in descricao_fases where d.name.Contains((k+1).ToString() + "-" + (j).ToString()) select d;
        GameObject descricao =  descricaoFases.First<GameObject>();
        var textos = from d in descricao.GetComponentsInChildren<Text>().ToList() where d.name.Contains("Conteudo") select d;
        Text texto = textos.First<Text>();
        if (nivel != null)
        {
            float tempoAtual = nivel.TempoRestante;
            string segundos = Mathf.Round(tempoAtual) % 60 < 10 ? "0" + (Mathf.Round(tempoAtual) % 60).ToString() :
                (Mathf.Round(tempoAtual) % 60).ToString();
            string minutos = Mathf.Round(tempoAtual) / 60 <= 0 ? "0" : Mathf.Floor(Mathf.Round(tempoAtual) / 60).ToString();
            texto.text = "Engrenólogos Coletados: " + nivel.Numero_Moedas + Environment.NewLine + "Número de Vidas: " + nivel.Numero_Vidas +
                Environment.NewLine + "Tempo Restante: " + minutos+":"+segundos + Environment.NewLine + "Pontuação: " + nivel.Pontuacao;
        }
        else
        {
            texto.text = "Ainda não há registros neste nível!";
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
                var painel = conquistaHud.transform.FindChild("Panel");
                painel.FindChild("Titulo").GetComponent<Text>().text = conquista.Nome;
                painel.FindChild("Descrição").GetComponent<Text>().text = conquista.Descricao;
                return;
            }
        }
    }


    public void IniciarFase()
    {
        int i = int.Parse(estagioSelecionado.name);
        int nivel = Game_Player.game_player.ordem_fases[(i - 1)];
        MudarDeCena(nivel);
    }
    public void IrParaConquistas()
    {
        MudarDeCena(Game_Player.game_player.faseConquista);
    }
    private void MudarDeCena(int nivel)
    {
        Game_Temp.instancia.TrocarFase = true;
        Game_Temp.instancia.FaseTroca = nivel;
        transicao.SetActive(true);
        //Application.LoadLevel(Game_Player.game_player.carregando);
    }
    public void SelecionarFase()
    {
        
        switch (estagioFase)
        {
            case 0:
                SoundManager.instance.PlaySingle(somSelect);
                FazerSelecaoFase();
                break;
            case 1:
                SoundManager.instance.PlaySingle(somPlay);
                IniciarFase();
                break;
            default:
                break;
        }
    }

    private void FazerSelecaoFase()
    {
        var f2 = from f in fases_ativas select f;
        var filtro = from f in fases_ativas where f.GetComponentInChildren<Toggle>().isOn select f.gameObject;
        estagioSelecionado = (filtro).First<GameObject>();
        int estagio = int.Parse(estagioSelecionado.name);
        animator.SetTrigger(mundo+"-" + estagio);
       
        estagioFase++;
    }

    private void AtivarEstrela(int estagio, float num, string nome)
    {
        var Todasestrelas = GameObject.FindGameObjectsWithTag("Estrela");
        var estrelas = from e in Todasestrelas where e.gameObject.name.Contains(nome) select e;
        foreach (var estrela in estrelas)
        {
            if (estrela.name.Contains("b"))
            {
                AlterarCor(estrela.GetComponent<Image>());
            }
            if (num > 50 && estrela.name.Contains("p"))
            {
                AlterarCor(estrela.GetComponent<Image>());
            }
            if (num > 100 && estrela.name.Contains("o"))
            {
                AlterarCor(estrela.GetComponent<Image>());
            }
        }
    }
    private void AlterarCor(Image img)
    {
        Color cor = Color.white;
        cor.a = 0.9f;
        img.color = cor;
    }
    public void Opcoes()
    {
        MudarDeCena(13);
    }
    public void Loja()
    {
        MudarDeCena(17);
    }
    public void Voltar()
    {
        switch(estagioFase)
        {
            case 1:
                SoundManager.instance.PlaySingle(somBack);
                VoltarSelecao();
                break;
            case 0:
                VoltarTitulo();
                break;
            default:
                break;
        }
    }

    private void VoltarTitulo()
    {
        MudarDeCena(0);
    }

    private void VoltarSelecao()
    {
        animator.SetTrigger("Volta");
        estagioFase--;

    }
}
