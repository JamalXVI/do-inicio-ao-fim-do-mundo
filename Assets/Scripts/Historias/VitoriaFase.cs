using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Net.NetworkInformation;
using UnityEngine.SceneManagement;

public class VitoriaFase : MonoBehaviour {
    public int numero_estados = 13;
    private int estado_atual;
    public int multiplicador_vidas;
    public int multiplicador_engrenagens;
    public int multiplicador_inimigos_derrotados;
    public int multiplicador_itens_coletados;
    public int multiplicador_tempo_restante;
    public float tempo_transicao_animacao;
    public float tempo_animacao;
    private float contador_transicao_animacao;
    private float contador_animacao;
    private bool terminou_contar;
    public string[] ordem_animacao;
    private string texto;
    private string numero_atual;
    private int posicao_atual;
    public int maximo_casas;
    public Text[] textos;
    private string outros_numeros;
    private bool iniciou;
    private int tempo;
    Nivel nivel;
    bool iniciou_fim;
    public GameObject transicao;
    public float tempo_fim = 2f;
    private float contador_fim;
    int pontos;
    public TransicaoCanvas transicao_canvas;
	// Use this for initialization
	void Start () {
        estado_atual = 1;
        terminou_contar = true;
        iniciou = true;
        iniciou_fim = false;
        contador_animacao = 0;
        contador_transicao_animacao = 0;
        contador_fim = 0f;
        Adicionar_Resto();
        texto = "";
	}
	
	// Update is called once per frame
	void Update () {
        if (numero_estados > estado_atual)
        {   
            if (estado_atual % 2 == 1)
            {
                if (!terminou_contar)
                {
                    animar_numeros();
                }
                else
                {
                    proxima_animacao();
                }   
            }else
            {
                esperar_proxima_animacao();
            }
        }
        else
        {
           fim();
        }
	}
    private void fim()
    {
        if (!iniciou_fim)
        {
            if (contador_fim > tempo_fim)
            {
                iniciou_fim = true;
                transicao.SetActive(true);
            }
            else
            {
                contador_fim += Time.deltaTime;
            }

        }
        else
        {
        }
    }
    void animar_numeros()
    {
        int posicao = estado_atual/2;
        switch (posicao)
        {
            case  1:
                terminou_contar = true;
                break;
            case 0:
                terminou_contar = true;
                break;
            default:
                fazer_animacao(posicao);
                break;
        }
    }
    void fazer_animacao(int posicao)
    {
        System.Random rnd = new System.Random();
        contador_animacao += Time.deltaTime;
        contador_transicao_animacao += Time.deltaTime;
        if (contador_transicao_animacao > tempo_transicao_animacao)
        {
            contador_transicao_animacao = 0;
            numero_atual = rnd.Next(0,9).ToString();
            texto = numero_atual + outros_numeros;
            adicionar_zeros();
            textos[posicao].text = texto; //+ " x" + multiplicador_itens_coletados;
            
        }
        if (contador_animacao > tempo_animacao)
        {
            contador_animacao = 0;
            switch (ordem_animacao[posicao])
            {
                case "vidas":
                    animar_vidas(posicao);
                    break;
                case "moedas":
                    animar_moedas(posicao);
                    break;
                case "inimigos":
                    animar_inimigos(posicao);
                    break;
                case "itens":
                    animar_itens(posicao);
                    break;
                case "tempo":
                    animar_tempo(posicao);
                    break;
                case "total":
                    animar_total(posicao);
                    break;
                default:
                    break;
            }   
        }
    }
    void animar_vidas(int posicao)
    {
        
         int maximo = nivel.Numero_Vidas.ToString().Length;
         montar_texto(maximo, nivel.Numero_Vidas);
         if (int.Parse(outros_numeros) >= maximo)
         {
             textos[posicao].text = texto + " x" + multiplicador_vidas;
         }else
         {
             textos[posicao].text = texto;
         }
         finalizar_animacao(maximo);
    }
    void animar_itens(int posicao)
    {

        int maximo = nivel.Numero_Itens.ToString().Length;
        montar_texto(maximo, nivel.Numero_Itens);
        if (int.Parse(outros_numeros) >= maximo)
        {
            textos[posicao].text = texto + " x" + multiplicador_itens_coletados;
        }
        else
        {
            textos[posicao].text = texto;
        }
        finalizar_animacao(maximo);
    }
    void animar_total(int posicao)
    {
        int maximo = nivel.Pontuacao.ToString().Length;
        montar_texto(maximo, nivel.Pontuacao);
        if (outros_numeros.Length >= maximo)
        {
            textos[posicao].text = texto + " Pontos";
        }
        else
        {
            textos[posicao].text = texto;
        }
        finalizar_animacao(maximo);
    }
    void animar_inimigos(int posicao)
    {

        int maximo = nivel.Numero_Inimigos.ToString().Length;
        montar_texto(maximo, nivel.Numero_Inimigos);
        if (int.Parse(outros_numeros) >= maximo)
        {
            textos[posicao].text = texto + " x" + multiplicador_inimigos_derrotados;
        }
        else
        {
            textos[posicao].text = texto;
        }
        finalizar_animacao(maximo);
    }
    void animar_tempo(int posicao)
    {

        int maximo = tempo.ToString().Length;
        montar_texto(maximo, tempo);
        
        if (int.Parse(outros_numeros) >= maximo)
        {
            textos[posicao].text = texto + " x" + multiplicador_tempo_restante;
        }
        else
        {
            textos[posicao].text = texto;
        }
        finalizar_animacao(maximo);
    }
    void animar_moedas(int posicao)
    {

        int maximo = nivel.Numero_Moedas.ToString().Length;
        montar_texto(maximo, nivel.Numero_Moedas);
        if (int.Parse(outros_numeros) >= maximo)
        {
            textos[posicao].text = texto + " x" + multiplicador_engrenagens;
        }
        else
        {
            textos[posicao].text = texto;
        }
        finalizar_animacao(maximo);
    }
    string definir_estado()
    {
        estado_atual++;
        if (estado_atual < 10)
        {
            return "0" + estado_atual;  
        }
        return estado_atual.ToString();
    }
    void proxima_animacao()
    {
        string trigger = "";
        terminou_contar = false;
        if (estado_atual < 10)
        {
            trigger = "0" + estado_atual;
        }
        else
        {
            trigger = estado_atual.ToString();
        }
        if (gameObject.GetComponent<Animator>().GetBool(trigger))
        {
            return;
        }
        estado_atual++;
        if (estado_atual <= numero_estados)
        {

            terminou_contar = false;
            if (estado_atual < 10)
            {
                trigger = "0" + estado_atual;
            }
            else
            {
                trigger = estado_atual.ToString();
            }
            gameObject.GetComponent<Animator>().SetTrigger(trigger);
        }
    }
    void montar_texto(int maximo,int numero)
    {
        if (iniciou && posicao_atual == 0)
        {
            iniciou = false;
            posicao_atual = maximo-1;
        }
        outros_numeros = numero.ToString().Substring(posicao_atual);
        texto = outros_numeros;
        adicionar_zeros();
    }
    void adicionar_zeros()
    {
        for (int i = texto.Length; i < maximo_casas; i++)
        {
            texto = "0" + texto;
        }
    }
    void finalizar_animacao(int maximo)
    {

        if (outros_numeros.Length >= maximo)
        {
            terminou_contar = true;
            contador_transicao_animacao = 0;
            contador_animacao = 0;
            texto = "";
            numero_atual = "";
            outros_numeros = "";
            posicao_atual = 0;
            iniciou = true;
        }
        else
        {
            posicao_atual--;
        }
    }
    void esperar_proxima_animacao()
    {
        string trigger = "";
        if (estado_atual < 10)
        {
            trigger = "0" + estado_atual;
        }
        else
        {
            trigger = estado_atual.ToString();
        }
        if (!gameObject.GetComponent<Animator>().GetBool(trigger))
        {
            proxima_animacao();
        }
    }
    void Adicionar_Resto()
    {
        Nivel nAntigo = Game_Player.instancia.RetornarNivel(Application.loadedLevel);
        Nivel n = new Nivel();
        ConfigurarPontuacao(n);
        VerificarSePassouPontuacaoMaxima(nAntigo, n);
        LiberarMusica();
        InformacoesDeCarregando();
        ConfigurarQuizz();
        VerificarConquistas(n);
        InserirHistorias();
    }

    private static void LiberarMusica()
    {
        if (Game_Temp.instancia.IdMusicaLiberar != 0)
        {
            Game_Loja.instancia.itemsDaLoja[Game_Temp.instancia.IdMusicaLiberar].Liberado = true;
        }
    }

    private static void VerificarConquistas(Nivel n)
    {

        Game_Conquistas.instancia.PassarDeFase(Game_Player.instancia.RetornarIndiceFase(Application.loadedLevel));
        Game_Conquistas.instancia.VerificarLiberarConquistasMoedas(n.Numero_Moedas);
        
    }

    private static void InserirHistorias()
    {   
            Game_Player.instancia.historias[Application.loadedLevel] = true;
            if (Game_Temp.instancia.HistoriasFases[Game_Player.instancia.RetornarIndiceFase(Application.loadedLevel)] != 0)
            {
                Game_Temp.instancia.TrocarFase = true;
                Game_Temp.instancia.FaseTroca = Game_Temp.instancia.HistoriasFases[Game_Player.instancia.RetornarIndiceFase(Application.loadedLevel)];

            }

            

    }

    private static void InformacoesDeCarregando()
    {

        Game_Player.instancia.FinalizarNivel(Application.loadedLevel);
        Game_Player.instancia.UltimoNivel = Application.loadedLevel;
        Game_Player.instancia.EnviarInfoNivel = true;
    }

    private static void ConfigurarQuizz()
    {
        int indice = Game_Player.instancia.RetornarIndiceFase(Application.loadedLevel);
        if (!Game_Quizz.instancia.NivelQuizz[indice].Liberado)
        {
            Game_Quizz.instancia.NivelQuizz[indice].Liberado = true;
        }
    }

    private static void VerificarSePassouPontuacaoMaxima(Nivel nAntigo, Nivel n)
    {
        if (n.Pontuacao >= nAntigo.Pontuacao)
        {
            Game_Player.instancia.AdicionarNivel(Application.loadedLevel, n);
        }
    }

    private void ConfigurarPontuacao(Nivel n)
    {
        n.Numero_Vidas = Game_Player.instancia.VidasTotais;
        n.Numero_Moedas = Game_Player.instancia.Moedas;
        n.TempoRestante = FindObjectOfType<HudTempo>().TempoRestante();
        n.Numero_Itens = Game_Player.instancia.QuantidadeItemFase;
        n.Numero_Inimigos = Game_Player.instancia.QuantidadeInimigosFase;
        pontos = n.Numero_Vidas * multiplicador_vidas + n.Numero_Moedas * multiplicador_engrenagens +
            n.Numero_Inimigos * multiplicador_inimigos_derrotados + n.Numero_Itens * multiplicador_itens_coletados
            + (int)Math.Round(n.TempoRestante * multiplicador_tempo_restante);
        n.Pontuacao = pontos;
        n.Vezes_Jogadas++;
        tempo = (int)Math.Round(FindObjectOfType<HudTempo>().TempoRestante());
        nivel = n;
    }
 
    
}
