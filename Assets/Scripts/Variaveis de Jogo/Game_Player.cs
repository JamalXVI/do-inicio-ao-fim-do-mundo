using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class Game_Player : Game_Base
{
    public static Game_Player instancia = null;
    private int vidas;
    private int vidas_extras;
    private int vidas_totais;
    public int vidas_iniciais = 3;
    private int moedas;
    private int pontuacao;
    private String nome_cena;
    private MovementController movimentos;
    public bool fim_jogo;
    public int[] quantidade_item;
    private Nivel[] niveis;
    private bool esperar_grounded;
    public Vector2 distancia_jogadores = new Vector2(40, 300);
    public Vector2 distancia_reversa_jogadores = new Vector2(20,300);
    public Vector2 distancia_reset = new Vector2(10, 0);
    public bool esperar_segunda_distancia;
    private bool parar_jogador;
    public Animator hud_cabeca;
    private bool segundo_jogador_acionado;
    private Transform jogador_morto;
    private bool iniciou_fase;
    private bool iniciou_dois_jogadores;
    public Socolinha socolinha;
    public Primitivo primitivo;
    private bool em_habilidade;
    private int nivel_game_over;
    private Habilidade habilidade_atual;
    private string habilidade_antiga;
    private string jogador_antigo;
    private bool parar_seguir;
    private bool pd_finalizar_troca;
    private bool suspender_camera;
    public int[] ordem_fases;
    private bool[] fases_completas;
    public int troca_fases;
    public int carregando;
    public int game_over;
    public bool[] historias;
    private bool podeReiniciar;
    private bool correr;
    private int moedas_jogo;
	private bool buraco;
    private int ultimo_nivel;
	public bool apertou_botao_troca;
	private bool habilidade_ativa;
	public float tempo_maximo_parado;
	public Vector2 distancia_max_camera;
	public float velocidade_camera_parado;
	public float tempo_espera_camera_movimento;
    private Dictionary<string, KeyCode> teclas;
    private bool enviarInfoNivel;
    private int quantidadeItemFase;
    private int quantidadeInimigosFase;
    private int quantidadeFrutas;
    private int moedasGastas;
    public int faseConquista;
    private bool[] falasNoJogo;
    private bool fornecerInformacoes;
    private bool jaFoiPerguntado;
    public bool JaFoiPerguntado
    {
        get { return jaFoiPerguntado; }
        set { jaFoiPerguntado = value; }
    }
    
    public bool FornecerInformacoes
    {
        get { return fornecerInformacoes; }
        set { fornecerInformacoes = value; }
    }
    
    public bool[] FalasNoJogo
    {
        get { return falasNoJogo; }
        set { falasNoJogo = value; }
    }
    
    public int MoedasGastas
    {
        get { return moedasGastas; }
        set { moedasGastas = value; }
    }
    
    public int QuantidadeFrutas
    {
        get { return quantidadeFrutas; }
        set { quantidadeFrutas = value; }
    }
        
    public int QuantidadeInimigosFase
    {
        get { return quantidadeInimigosFase; }
        set { quantidadeInimigosFase = value; }
    }


    public int QuantidadeItemFase
    {
        get { return quantidadeItemFase; }
        set { quantidadeItemFase = value; }
    }
    
    public bool EnviarInfoNivel
    {
        get { return enviarInfoNivel; }
        set { enviarInfoNivel = value; }
    }
    
    public Dictionary<string, KeyCode> Teclas
    {
        get { return teclas; }
        set { teclas = value; }
    }


    [SerializeField]
    private bool semSeguir;
    
	public MovementController Movimento_Atual{
		get{ return movimentos;}

	}
    public bool Em_Habilidade
    {
        get { return em_habilidade;}
    }
	public bool Habilidade_Ativa {
		get{return habilidade_ativa;}
		set{habilidade_ativa = value;}
	}
    

    public int UltimoNivel
    {
        get { return ultimo_nivel; }
        set { ultimo_nivel = value; }
    }
    
	public bool Buraco
	{
		get { return buraco;}
		set { buraco = value;}
	}
    public int Moedas_Jogo
    {
        get { return moedas_jogo - moedasGastas; }
    }
    
    public bool Correr
    {
        get { return correr; }
        set { correr = value; }
    }
    
    public bool PodeReiniciar
    {
        get { return podeReiniciar; }
        set { podeReiniciar = value; }
    }
    
    public bool Suspender_Camera
    {
        get { return suspender_camera; }
        set { suspender_camera = value; }
    }
    
    public bool Parar_Seguir
    {
        set { parar_seguir = value; }
        get
        {
            if (semSeguir)
            {
                return true;
            } 
            return parar_seguir; }
    }
    public Habilidade Habilidade_Atual
    {
        get { return habilidade_atual; }
    }
    
    public int Nivel_Game_Over
    {
        get { return nivel_game_over; }
        set { nivel_game_over = value; }
    }
    
    
    
    private bool pausar_jogadores;

    public bool Pausar_jogadores
    {
        get { return pausar_jogadores; }
        set { pausar_jogadores = value; }
    }
    
    public bool Segundo_Acionado
    {
        get { return segundo_jogador_acionado; }
    }
    public bool Iniciou_fase
    {
        get { return iniciou_fase; }
        set { iniciou_fase = value; }
    }
    

    public bool Parar_Jogador { get; set; }
    
    public Transform Jogador{ get; set; }
    public Transform JogadorSecundario { get; set; }
    
    public int Pontuacao
    {
        get { return pontuacao; }
        set { pontuacao = value; }
    }
    public int VidasTotais
    {
        get { return vidas_totais; }
    }
    public int Vidas
    {
        get { return vidas; }
        set
        {
            vidas = value;
            this.vidas_totais = vidas + vidas_extras;
        }
    }
    public int Vidas_Extras
    {
        get { return vidas_extras; }
        set
        {
            vidas_extras = value;
            this.vidas_totais = vidas + vidas_extras;
        }
    }
    public void Reiniciar_Vidas()
    {
        Vidas = vidas_iniciais;
        Vidas_Extras = 0;
    }
    public int Moedas
    {
        get { return moedas; }
        set { moedas = value; }
    }

    void Awake()
    {
        if (Game_Player.instancia == null)
        {
            DontDestroyOnLoad(gameObject);

            instancia = this;
            IniciarVariaveisDeJogo();
            DefinicoesTeclas();
        }
        else if (Game_Player.instancia != this)
        {

            Destroy(gameObject);
        }
        Definicoes();
        
    }

    private static void IniciarVariaveisDeJogo()
    {
        Game_Player.instancia.quantidade_item = new int[999];
        Game_Player.instancia.niveis = new Nivel[999];
        Game_Player.instancia.FalasNoJogo = new bool[999];
        Game_Player.instancia.apertou_botao_troca = false;
    }
    void Start()
    {
    }
    public void reniciar()
    {
        PegarJogador();
        Game_Player.instancia.fim_jogo = false;
        instancia.Reiniciar_Vidas();
    }
    public void PegarJogador()
    {
        Jogador = null;
        JogadorSecundario = null;
        foreach (GameObject jogadore in GameObject.FindGameObjectsWithTag("PlayerP"))
        {
            if (jogadore.name.Contains("Socolinha"))
            {
                if (!jogadore.Equals(jogador_morto))
                {
                    Jogador = jogadore.transform;
                }
            }
            else if (!jogadore.Equals(jogador_morto))
            {
                JogadorSecundario = jogadore.transform;
            }
        }
        if (Jogador == null)
        {
            Jogador = JogadorSecundario;
            JogadorSecundario = null;  
        }
        if (JogadorSecundario != null)
        {
            MudarSecundario();
            MudarPrimario();
            this.JogadorSecundario.position = new Vector3(Jogador.position.x-10, Jogador.position.y, Jogador.position.z);

        } 
        Game_Player.instancia.movimentos = Jogador.GetComponent<MovementController>();

        movimentos.ColisaoPersonagem(true);
        if (iniciou_fase)
        {
            iniciou_fase = false;
            if (JogadorSecundario != null)
            {
                iniciou_dois_jogadores = true;
            }else
            {
                iniciou_dois_jogadores = false;
            }
            desativar_se_desativados();
        }
    }

    private void desativar_se_desativados()
    {
        if(!iniciou_dois_jogadores)
        {
            return;
        }
        if(!primitivo.Ativo)
        {
            if(Jogador.name.ToLower().Contains("primitivo"))
            {
                Jogador.gameObject.SetActive(false);
                Jogador = JogadorSecundario;
            }
            else
            {
                JogadorSecundario.gameObject.SetActive(false);
            }
            JogadorSecundario = null;
            iniciou_dois_jogadores = false;
        }
        if (!socolinha.Ativo)
        {
            if (Jogador.name.ToLower().Contains("socolinha"))
            {
                Jogador.gameObject.SetActive(false);
                Jogador = JogadorSecundario;
            }
            else
            {
                JogadorSecundario.gameObject.SetActive(false);
            }
            
            JogadorSecundario = null;
            iniciou_dois_jogadores = false;
        }
    }
    public void MudarSecundario()
    {
        JogadorSecundario.GetComponent<MovementController>().Ativo = false;
        JogadorSecundario.GetComponent<MovementController>().ColisaoPersonagem(false);

    }
    public void MudarPrimario()
    {
        Jogador.GetComponent<MovementController>().Ativo = true;
        Jogador.GetComponent<MovementController>().ColisaoPersonagem(true);

    }
    private void Acordar()
    {
        if (Application.loadedLevelName == nome_cena)
        {
            Game_Player.instancia.mudar_valores = true;
        }
        else
        {
            Game_Player.instancia.mudar_valores = false;
        }
    }
    public void ZerarVariaveis()
    {
        moedas = 0;
        quantidadeItemFase = 0;

    }
    public void SalvarMoedas()
    {
        moedas_jogo += moedas;
    }
    public void AdicionarMoedas(int numero)
    {
        moedas_jogo += numero;
    }
    public void Salvar(string caminho)
    {
		SalvarJogador sv = new SalvarJogador();
        sv.socolinha = new SalvarSocolinha();
        sv.primitivo = new SalvarPrimitivo();
        SalvarSocolinha(sv);
        SalvarPrimitivo(sv);
		sv.ordem_fases = ordem_fases ;
		sv.fases_completas = this.fases_completas;
        sv.historias = this.historias;
		sv.niveis = (Nivel[]) niveis.Clone ();
		sv.vidas = this.vidas;
		sv.vidas_extras = this.vidas_extras;
		sv.vidas_totais = this.vidas_totais;
        sv.moedas = moedas_jogo;
        sv.moedas_gastas = moedasGastas;
        sv.quantidade_item = quantidade_item;
        sv.nivelQuizz = (Nivel_Quizz[])Game_Quizz.instancia.NivelQuizz.Clone();
        sv.conquistas = Game_Conquistas.instancia.conquistas;
        sv.variaveisConquistas = Game_Conquistas.instancia.VariaveisConquista;
        sv.itemsDaLoja = Game_Loja.instancia.itemsDaLoja;
        sv.falasNoJogo = (bool[])falasNoJogo.Clone();
        sv.fornecerInformacao = FornecerInformacoes;
        sv.jaFoiInformado = JaFoiPerguntado;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(caminho + "/" + instancia.GetType().Name + ".dat");
        bf.Serialize(file, sv);
		file.Close();
    }

    private void SalvarSocolinha(SalvarJogador sv)
    {
        sv.socolinha.Ativo = socolinha.Ativo;
        sv.socolinha.tempo_entre_notas = socolinha.tempo_entre_notas;
        sv.socolinha.Duracao_Musica_Parcial = socolinha.Duracao_Musica_Parcial;
        sv.socolinha.Duracao_Musica_Total = socolinha.Duracao_Musica_Total;
        sv.socolinha.habilidades = socolinha.habilidades;
        sv.socolinha.duracao_paralisado = socolinha.duracao_paralisado;
    }

    private void SalvarPrimitivo(SalvarJogador sv)
    {
        sv.primitivo.Ativo = primitivo.Ativo;
        sv.primitivo.duracao_slide = primitivo.duracao_slide;
        sv.primitivo.habilidades = primitivo.habilidades;
        sv.primitivo.Contador_Slide = primitivo.Contador_Slide;
        sv.primitivo.Pode_Parar_Slide = primitivo.Pode_Parar_Slide;
    }
    public bool Carregar()
    {
        if (File.Exists(Application.persistentDataPath + "/" + instancia.Pegar_Tipo() + ".dat"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                FileStream file = File.Open(Application.persistentDataPath + "/" + instancia.Pegar_Tipo() + ".dat", FileMode.Open);
                SalvarJogador sv = (SalvarJogador)bf.Deserialize(file);
                if (SalvarJogador.compararVersao(sv))
                {
                    CarregarVariaveis(sv);
                    //      Application.LoadLevel(Game_Player.game_player.troca_fases);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
            }
            
        }
        return false;
    }
	public void CarregarVariaveis(SalvarJogador sv)
	{
		vidas = sv.vidas;
		vidas_extras = sv.vidas_extras;
		vidas_totais = sv.vidas_totais;
        moedas_jogo = sv.moedas;
        moedasGastas = sv.moedas_gastas;
		fases_completas = sv.fases_completas;
        quantidade_item = sv.quantidade_item;
        falasNoJogo = (bool[])sv.falasNoJogo.Clone();
        CarregarQuizz(sv);

        fornecerInformacoes = sv.fornecerInformacao;
        JaFoiPerguntado = sv.jaFoiInformado;
        historias = sv.historias;
	    ordem_fases = sv.ordem_fases;
        CarregarPrimitivo(sv);
        CarregarSocolinha(sv);
		niveis = (Nivel[])sv.niveis.Clone ();
        Game_Conquistas.instancia.conquistas = (Conquista[])sv.conquistas.Clone();
        Game_Conquistas.instancia.VariaveisConquista = sv.variaveisConquistas;
        Game_Loja.instancia.itemsDaLoja = (ItemLoja[])sv.itemsDaLoja.Clone();
	}

    private static void CarregarQuizz(SalvarJogador sv)
    {
        Game_Quizz.instancia.NivelQuizz = (Nivel_Quizz[])sv.nivelQuizz.Clone() ;
    }

    private void CarregarPrimitivo(SalvarJogador sv)
    {
        primitivo.Ativo = sv.primitivo.Ativo;
        primitivo.duracao_slide = sv.primitivo.duracao_slide;
        primitivo.habilidades = sv.primitivo.habilidades;
        primitivo.Contador_Slide = sv.primitivo.Contador_Slide;
        primitivo.Pode_Parar_Slide = sv.primitivo.Pode_Parar_Slide;
    }

    private void CarregarSocolinha(SalvarJogador sv)
    {
        socolinha.Ativo = sv.socolinha.Ativo;
        socolinha.tempo_entre_notas = sv.socolinha.tempo_entre_notas;
        socolinha.Duracao_Musica_Parcial = sv.socolinha.Duracao_Musica_Parcial;
        socolinha.Duracao_Musica_Total = sv.socolinha.Duracao_Musica_Total;
        socolinha.habilidades = sv.socolinha.habilidades;
        socolinha.duracao_paralisado = sv.socolinha.duracao_paralisado;
    }
    public void ReiniciarPosicoesJogadores(Vector3 posicao)
    {
        Jogador.position = posicao;
        if (JogadorSecundario != null)
        {
            esperar_grounded = true;
            //JogadorSecundario.gameObject.SetActive(false);
            JogadorSecundario.position = posicao;
        }
    }
    /// <summary>
    /// UPDATE
    /// </summary>
    void Update()
    {
        if (podeReiniciar)
        {
            podeReiniciar = false;
            reniciar();
        }
        definir_habilidade_atual();
        if (JogadorSecundario != null)
        {
            alterar_jogador_secundario();
        }
        if (pd_finalizar_troca == true)
        {
            verificar_fim_troca();
            return;
        }
        nome_cena = Application.loadedLevelName;
    }
    private void alterar_jogador_secundario()
    {
        if (semSeguir)
        {
            return;
        }
        if (Parar_Seguir)
        {
            return;
        }
        float it = 1;
        if (!movimentos.Direita)
        {
            it *= -1;
        }
        if (verificar_distancia_esperar(it) && movimentos.EstaMovimentando())
        {
            
            esperar_segunda_distancia = true;
        }
        else
        {
            esperar_segunda_distancia = false;
        }
        if (esperar_grounded && Jogador.GetComponent<MovementController>().grounded && !em_habilidade)
        {
            JogadorSecundario.gameObject.SetActive(true);
            resetar_posicoes(it);
            esperar_grounded = false;
        }
        if (distancia_resetar())
        {
            resetar_posicoes(it);
        }
    }
    private bool verificar_distancia_esperar(float it)
    {
        return ((Jogador.position.x - JogadorSecundario.position.x <= distancia_reversa_jogadores.x && it == 1) ||
            (Jogador.position.x - JogadorSecundario.position.x >= -distancia_reversa_jogadores.x &&
             it != 1));
    }
	public void resetar_jogador()
	{
		if (jogador_morto != null) {
			return;
		}
		Vector3 posicao = new Vector3(Jogador.position.x,
		                              Jogador.position.y, Jogador.position.z);
		JogadorSecundario.position = posicao;
	}
    private void resetar_posicoes(float i)
    {
        Vector3 posicao = new Vector3(Jogador.position.x - i*distancia_reset.x,
            Jogador.position.y, Jogador.position.z);
        JogadorSecundario.position = posicao;
    }
    public void AdicionarNivel(int indice, Nivel nivel)
    {

        niveis[indice] = nivel;
    }
    public void FinalizarNivel(int indice)
    {

        for (int i = 0; i < ordem_fases.Length; i++)
        {
            if (ordem_fases[i] == indice)
            {
                fases_completas[i] = true;
            }
        }
    }
    public int RetornarIndiceFase(int nivel)
    {
        for (int i = 0; i < ordem_fases.Length; i++)
        {
            if (ordem_fases[i] == nivel)
            {
                return i;
            }
        }
        return -1;
    }
    public bool VerificarSePassouFase(int i)
    {
        return fases_completas[i];
    }
    public int RetornarNumNivel(int i)
    {
        for (int indice = 0; indice < ordem_fases.Length; indice++)
        {
            if (ordem_fases[indice] == i)
            {
                return indice;
            }
        }
        return 0;
    }
    public Nivel RetornarNivel(int indice)
    {
         return niveis[indice];
    }

    public void AcionarSegundoJogador()
    {
        if(!segundo_jogador_acionado && JogadorSecundario != null)
        {
            segundo_jogador_acionado = true;
            jogador_morto = Jogador;
            Jogador = JogadorSecundario;
            JogadorSecundario = null;
            movimentos = Jogador.GetComponent<MovementController>();
            movimentos.DeixarInvencivel();
            MudarPrimario();
        }
    }

    private void resetar_jogador(Transform jogador_t)
    {
        jogador_t.transform.GetComponent<MovementController>().SetAllCollidersStatus(true);
        jogador_t.transform.GetComponent<MovementController>().vivo = true;
        jogador_t.transform.GetComponent<SpriteRenderer>().sortingLayerName = "Character";
        jogador_t.gameObject.SetActive(true);
    }
    public bool PodeTrocar()
    {
        if (iniciou_dois_jogadores && JogadorSecundario != null)
        {
            return true;
        }
        return false;
    }
   
    public void InativarJogadorMorto()
    {
        parar_seguir = false;
    
       // Game_Player.game_player.segundo_jogador_acionado = true;
    }
    public void reiniciar_jogadores()
    {
        if (jogador_morto != null)
        {
            JogadorSecundario = Jogador;
            Jogador = jogador_morto;
            resetar_jogador(JogadorSecundario);
            MudarSecundario();
        }
        resetar_jogador(Jogador);
        movimentos = Jogador.GetComponent<MovementController>();
        MudarPrimario();
        jogador_morto = null;
        segundo_jogador_acionado = false;
    }
    public bool VerificarReiniciarJogo(Transform jogador_hit)
    {
        if (!iniciou_dois_jogadores)
        {
            return true;
        }
        /*
        if (buraco)
        {
			return true;
		}
         * */
		try {
			if (!jogador_morto.Equals(Jogador) && jogador_morto != null && jogador_hit.Equals(Jogador))
            {
				return true;
            };
		} catch (Exception ex) {
            jogador_morto = null;
			return false;
		}
        return false;
    }
	public bool VerificarSeEhPrincipal(Transform jogador_esc)
	{
		if ((jogador_esc == Jogador) ) {
			return true;
		}
		return false;
	}
    /// <summary>
    /// PARTE DE TROCA DE PERSONAGENS
    /// </summary>
    internal void finalizar_troca()
    {
        pd_finalizar_troca = true;
        
        
        
    }
    private void verificar_fim_troca()
    {
        if (movimentos.FimIdleAnimacao())
        {
            suspender_camera = true;
            JogadorSecundario.transform.position =
            JogadorSecundario.GetComponent<MovementController>().ResetarPosicao().position;
            Jogador.transform.position =
            Jogador.GetComponent<MovementController>().ResetarPosicao().position;
            JogadorSecundario.GetComponent<Animator>().enabled = false;
            Jogador.GetComponent<Animator>().enabled = false;
            Jogador.GetComponent<MovementController>().ResetarPosicaoBody();
            JogadorSecundario.GetComponent<MovementController>().ResetarPosicaoBody();
            finalizar_reset_animacao();
            
        }
        if (movimentos.PodeTerminarTroca())
        {
            finalizando_troca();
        }
    }
    private void finalizar_reset_animacao()
    {
        JogadorSecundario.GetComponent<Animator>().SetBool("TrocaIn", false);
        Jogador.GetComponent<Animator>().SetBool("TrocaOut", false);
        Jogador.GetComponent<Animator>().Play("Idle");
        JogadorSecundario.GetComponent<Animator>().Play("Idle");
        JogadorSecundario.GetComponent<Animator>().enabled = true;
        Jogador.GetComponent<Animator>().enabled = true;
    }
    public void finalizando_troca()
    {
		apertou_botao_troca = true;
		Transform trocar_jogador = Jogador;
        Jogador = JogadorSecundario;
        JogadorSecundario = trocar_jogador;
        movimentos = Jogador.GetComponent<MovementController>();
		alternar_personagens ();
        pd_finalizar_troca = false;
        suspender_camera = false;
        movimentos.ColisaoPersonagem(true);
    }
	public void alternar_personagens()
	{
		MudarPrimario();
		MudarSecundario();
	}
    public void trocar_personagens()
    {
        //Jogador.position = JogadorSecundario.position;
		if (!iniciou_dois_jogadores) {
			return;
		}
        float it = 1;
        if (!movimentos.Direita)
        {
            it *= -1;
        };
        if (verificar_distancia_esperar(it))
        {
            if ((it == 1 && Jogador.position.x - JogadorSecundario.position.x < 0) ||
                (it == -1 && Jogador.position.x - JogadorSecundario.position.x > 0) )
            {
                movimentos.mudar_face();
                JogadorSecundario.GetComponent<MovementController>().mudar_face();
            }
        }

        JogadorSecundario.GetComponent<Animator>().SetBool("TrocaIn", true);
        Jogador.GetComponent<Animator>().SetBool("TrocaOut", true);
        JogadorSecundario.GetComponent<Animator>().Play("Idle_back");
        Jogador.GetComponent<Animator>().Play("Idle_front");
        //JogadorSecundario.GetComponent<MovementController>().mudar_Colliders(false);
        //Jogador.GetComponent<MovementController>().mudar_Colliders(false);
        JogadorSecundario.GetComponent<MovementController>().ColisaoPersonagem(false);
        Jogador.GetComponent<MovementController>().ColisaoPersonagem(true);
        movimentos = Jogador.GetComponent<MovementController>();

    }
    /// <summary>
    /// HABILIDADES
    /// </summary>
    public void GerarHabilidade()
    {
        if (Habilidade_Atual == null)
        {
            return;
        }
        if (Jogador.name == "Socolinha")
        {
            if (Habilidade_Atual.nome == "Mover")
            {

                if (VerificarBotaoApertado())
                {
                    if (!Jogador.GetComponent<Animator>().GetBool("Empurrando"))
                    {
                        movimentos.IniciarEmpurrando();
                    }
                    movimentos.MoverEmpurrando();
                }
                else// if (Input.GetKeyUp(Game_Player.game_player.Teclas["habilidade"]))
                {
                    ;
                    FinalizarSocolinhaMover();
                }
            }
            else if (Habilidade_Atual.nome == "Descer")
            {
            }
            else if (Habilidade_Atual.nome == "Florescer")
            {
                ContarMusica();
                if (RenovarTocarMusica())
                {
                    AcaoTocarMusica();
                }
                if (VerificarAcabarMusica())
                {
                    AcabarMusica();
                }
            }
            else{
                ContarMusica();
                if (RenovarTocarMusica())
                {
                    AcaoTocarMusica();
                }
                if (VerificarAcabarMusica())
                {
                    AcabarMusica();
                }
            }
            
        }else{
            if (Habilidade_Atual.nome.Contains("Cavucar"))
            {
                if (movimentos.VerificarFimCavucar())
                {
                    PararHabilidades();

                }
            }
            else if (Habilidade_Atual.nome.Contains("Slide")){
               primitivo.Contador_Slide += Time.deltaTime;
               movimentos.DarSlide();
               if (primitivo.Contador_Slide >= 0.5f)
               {
                   TerminarHabilidade();
               }
            }
            else if (Habilidade_Atual.nome.Contains("Grito"))
            {
                if (movimentos.CondicaoPararGrito())
                {
                   TerminarHabilidade();
                   movimentos.PararGrito();
                }
            }
            else
            {
                if (movimentos.CondicaoPararLevantar())
                {
                    TerminarHabilidade();
                }
                else if (movimentos.CondicaoLevantando())
                {
                    movimentos.MoverCaixa();
                }
            }
                
        }
    }

    private static bool VerificarBotaoApertado()
    {
        if (!Input.GetKey(Game_Player.instancia.Teclas["habilidade"]) &&
            !Input.GetKeyUp(Game_Player.instancia.Teclas["habilidade"]))
        {
            return false;
        }
        return !Input.GetKeyUp(Game_Player.instancia.Teclas["habilidade"]) ||
                        Input.GetKeyDown(Game_Player.instancia.Teclas["habilidade"]);
    }

    private void TerminarHabilidade()
    {
        pausar_jogadores = false;
        movimentos.FinalizarHabilidades();
        em_habilidade = false;

    }
    public void AcabarMusica()
    {
        socolinha.Duracao_Musica_Total = 0f;
        TerminarHabilidade();
    }
    public bool VerificarAcabarMusica()
    {
        return socolinha.Duracao_Musica_Total >= socolinha.duracao_habilidade_musica;
    }
    public void ContarMusica()
    {
        socolinha.Duracao_Musica_Total += Time.deltaTime;
        socolinha.Duracao_Musica_Parcial += Time.deltaTime;
    }
    public bool RenovarTocarMusica()
    {
        return socolinha.Duracao_Musica_Parcial >= socolinha.tempo_entre_notas;
    }
    public void AcaoTocarMusica()
    {
        socolinha.Duracao_Musica_Parcial = 0;
        Musica();
    }
	public void PararHabilidades()
	{
        if (Habilidade_Atual == null)
        {
            return;
        }
		if (Jogador.name == "Socolinha") {
            if (Habilidade_Atual.nome == "Mover")
            {
                if (movimentos.PoderPararMover())
                {
                    FinalizarSocolinhaMover();

                }

            }
            else if (Habilidade_Atual.nome == "Descer")
            {
                    pausar_jogadores = false;
                    movimentos.FinalizarHabilidades();
                    em_habilidade = false;
                    Game_Player.instancia.Movimento_Atual.HabilidadeDescer(false);

            }
            else{
                socolinha.Duracao_Musica_Total = socolinha.duracao_habilidade_musica;

            }

		} else {
            if (Habilidade_Atual.nome.Contains("Cavucar"))
            {

                pausar_jogadores = false;
                movimentos.FinalizarHabilidades();
                em_habilidade = false;
            }
            else if (Habilidade_Atual.nome.Contains("Slide"))
            {
                primitivo.Contador_Slide = 0.5f;

            }
            else
            {
                movimentos.ForcarPararGrito();
            }

		}
	}

    private void FinalizarSocolinhaMover()
    {
        pausar_jogadores = false;
        movimentos.FinalizarHabilidades();
        em_habilidade = false;
        movimentos.FinalizarMover();
    }

    private void Musica()
    {
        Destroy((GameObject)Instantiate(socolinha.habilidade_Musica, Jogador.position,
            socolinha.habilidade_Musica.transform.rotation), socolinha.tempo_entre_notas);

    }

    public bool pode_atirar(GameObject gameObject)
    {
        if (gameObject.transform == JogadorSecundario)
        {
            return false;
        }
        return true;
    }

    public void MatarJogador()
    {
        parar_seguir = false;
        pausar_jogadores = false;
        movimentos.FinalizarHabilidades();
    }

    public void IniciarHabilidades()
    {
        em_habilidade = true;
        if (Jogador.name == "Socolinha")
        {
            
            switch (habilidade_atual.nome)
            {
                case "Mover":
                    Game_Player.instancia.Movimento_Atual.IniciarEmpurrando();
                    break;
                case "Descer":
                    Game_Player.instancia.Movimento_Atual.HabilidadeDescer(true);
                    break;
                case "Song":
                    socolinha.Duracao_Musica_Parcial = socolinha.tempo_entre_notas;
                    socolinha.Duracao_Musica_Total = 0f;
                    movimentos.IniciarSong();
                    break;
                case "Florescer":
                    socolinha.Duracao_Musica_Parcial = socolinha.tempo_entre_notas;
                    socolinha.Duracao_Musica_Total = 0f;
                    movimentos.IniciarFlorescer();
                    break;
            }
        }else if (Jogador.name == "Primitivo")
        {
            if (Habilidade_Atual.nome == "Cavucar")
            {
                movimentos.iniciarCavucar();
            }
            else if (Habilidade_Atual.nome == "Slide")
            {
                parar_seguir = true;
                primitivo.Contador_Slide = 0f;
                movimentos.IniciarSlide();
            }
            else if (Habilidade_Atual.nome == "Grito")
            {
                movimentos.IniciarGrito();
            }
            else {
                movimentos.IniciarLevantar();
            }
        }
        
    }
    public void iniciar_Habilidades(string habilidade)
    {
        em_habilidade = true;
        if (Jogador.name.Contains("Socolinha"))
        {
            switch (habilidade)
            {
                case "Mover" :
                    Game_Player.instancia.Movimento_Atual.IniciarEmpurrando();
                    break;
                case "Descer":
                    Game_Player.instancia.Movimento_Atual.HabilidadeDescer(true);
                    break;
                default:
                    break;
            }
        }
        else
        {
            parar_seguir = true;
            primitivo.Contador_Slide = 0f;
        }

    }
    private void definir_habilidade_atual()
    {
        if (Jogador == null)
        {
            return;
        }
        if (socolinha.habilidades == null || primitivo.habilidades == null || socolinha.habilidades.Length == 0 ||
            primitivo.habilidades.Length == 0)
        {
            Definicoes();
            return;
        }
		/*
        if (habilidade_atual != null)
        {
            if (habilidade_antiga.Equals(habilidade_atual.nome) && Jogador.name.Equals(jogador_antigo))
            {
                return;
            }
        }
        habilidade_atual = new Habilidade();
        if (Jogador.name == "Socolinha")
        {
            jogador_antigo = "Socolinha";
            for (int i = 0; i < socolinha.habilidades.Length;i++ )
            {

                Habilidade habilidade = socolinha.habilidades[i];
                if (habilidade.Escolhida)
                {
                    habilidade_atual = habilidade;
                }

            }
            if (habilidade_atual == null)
            {
               socolinha.habilidades[0].Escolhida = true;
               habilidade_atual = socolinha.habilidades[0];
               habilidade_antiga = habilidade_atual.nome;
            }

        }
        else
        {
            jogador_antigo = "Primitivo";
            for (int i = 0; i < primitivo.habilidades.Length; i++)
            {
                Habilidade habilidade = primitivo.habilidades[i];
                if (habilidade.Escolhida)
                {
                    habilidade_atual = habilidade;
                }

            }
            if (habilidade_atual == null)
            {
                primitivo.habilidades[0].Escolhida = true;
                habilidade_atual = primitivo.habilidades[0];
            }
        }
        habilidade_antiga = habilidade_atual.nome;
		*/
    }
    private void Definicoes()
    {
        primitivo.habilidades = new Habilidade[4];
        socolinha.habilidades = new Habilidade[4];
        primitivo.habilidades[0] = new Habilidade();
        primitivo.habilidades[0].nome = "Slide";
        primitivo.habilidades[0].ativado = true;
        primitivo.habilidades[0].Escolhida =false;
        primitivo.habilidades[1] = new Habilidade();
        primitivo.habilidades[1].nome = "Cavucar";
        primitivo.habilidades[1].ativado = true;
        primitivo.habilidades[1].Escolhida = false;
        primitivo.habilidades[2] = new Habilidade();
        primitivo.habilidades[2].nome = "Grito";
        primitivo.habilidades[2].ativado = true;
        primitivo.habilidades[2].Escolhida = false;
        primitivo.habilidades[3] = new Habilidade();
        primitivo.habilidades[3].nome = "Levantar";
        primitivo.habilidades[3].ativado = true;
        primitivo.habilidades[3].Escolhida = false;
        socolinha.habilidades[0] = new Habilidade();
        socolinha.habilidades[0].nome = "Song";
        socolinha.habilidades[0].ativado = true;
        socolinha.habilidades[0].Escolhida = false;
        socolinha.habilidades[1] = new Habilidade();
        socolinha.habilidades[1].nome = "Mover";
        socolinha.habilidades[1].ativado = true;
        socolinha.habilidades[1].Escolhida = false;
        socolinha.habilidades[2] = new Habilidade();
        socolinha.habilidades[2].nome = "Descer";
        socolinha.habilidades[2].ativado = true;
        socolinha.habilidades[2].Escolhida = false;
        socolinha.habilidades[3] = new Habilidade();
        socolinha.habilidades[3].nome = "Florescer";
        socolinha.habilidades[3].ativado = true;
        socolinha.habilidades[3].Escolhida = false;

        fases_completas = new Boolean[ordem_fases.Length];
        for (int i = 0; i < fases_completas.Length; i++)
        {
            fases_completas[i] = false;
        }
        historias = new Boolean[999];
    }
    public void DefinicoesTeclas() {
        Game_Player.instancia.Teclas = new Dictionary<string, KeyCode>();
        Game_Player.instancia.Teclas.Add("pulo", KeyCode.Space);
        Game_Player.instancia.Teclas.Add("direita", KeyCode.RightArrow);
        Game_Player.instancia.Teclas.Add("esquerda", KeyCode.LeftArrow);
        Game_Player.instancia.Teclas.Add("correr", KeyCode.LeftShift);
        Game_Player.instancia.Teclas.Add("habilidade", KeyCode.Z);
        Game_Player.instancia.Teclas.Add("troca", KeyCode.X);
        Game_Player.instancia.Teclas.Add("enter", KeyCode.Return);
        Game_Player.instancia.Teclas.Add("esc", KeyCode.Escape);
        Game_Input.TranscreverControles();

    }
    private bool distancia_resetar()
    {
        return ((Math.Abs(Jogador.position.x - JogadorSecundario.position.x) >= distancia_jogadores.x) ||
            Math.Abs(Jogador.position.y - JogadorSecundario.position.y) >= distancia_jogadores.y);
    }
    public bool pode_Parar_Seguir()
    {
        if (semSeguir)
        {
            return true;
        }
		if (!iniciou_dois_jogadores || jogador_morto != null) {
			return false;
		}
        return !distancia_resetar();
    }



    public void LimparHabilidades()
    {
        PararHabilidades();
        habilidade_atual = null;
        Game_Player.instancia.Habilidade_Ativa = false;
        foreach (Habilidade habilidade in socolinha.habilidades)
        {
            habilidade.Escolhida = false;
        }
    }

    public void AtivarHabilidades(string p)
    {
        habilidade_atual = null;
        Game_Player.instancia.Habilidade_Ativa = true;
        foreach (Habilidade habilidade in socolinha.habilidades)
        {
            if (!habilidade.nome.Contains(p))
            {

                habilidade.Escolhida = false;
            }else{
                habilidade.Escolhida = true;
                habilidade_atual = habilidade;
            }
        }
        foreach (Habilidade habilidade in primitivo.habilidades)
        {
            if (!habilidade.nome.Contains(p))
            {

                habilidade.Escolhida = false;
            }
            else {
                habilidade.Escolhida = true;
                habilidade_atual = habilidade;
            }
        }
    }
}
