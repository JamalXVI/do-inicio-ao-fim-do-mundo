using UnityEngine;
using System.Collections;

public class Gerenciador : MonoBehaviour
{
    private CheckPoint[] checkpoints;
    private Transform jogador;
    public int nivel;
    private Vector3 posicao;
    public Vector2 forcanoPersonagem;
    public int pontuacao_nivel;
    public bool parar_jogador;
    private GameObject[] moedas;
    private int total_moedas;
    private Game_Inimigos game_inimigos;
    public bool jogo_rodando;
    private bool pausar_jogo;
    public GameObject canvas_pause;
    private bool reiniciar = false;
    public Vector2 distancia_cancelar_habilidade = new Vector2(20f,20f);
    public bool comecou_habilidade;
    public Transform corpo_habilidade;
    public Transform jogador_momento;
    public bool habilidade_em_x;
    [SerializeField]
    private AudioClip somMorrendo;
    private bool[] pegouFrutas;
    private bool jaAdicionouVidaFrutas;
    public bool Pausar_Jogo
    {
        get { return pausar_jogo; }
        set
        {
            pausar_jogo = value;
            if (pausar_jogo)
            {
                canvas_pause.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                canvas_pause.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public int TotalMoedas
    {
        get { return total_moedas; }
    }
    public void AdicionarFruta(int i)
    {
        pegouFrutas[i] = true;
        VerificarFrutasMaxima();
    }
    public bool verificarSePegouTodasAsFrutas()
    {
        bool pegou = true;
        foreach (bool b in pegouFrutas)
        {
            if (!b)
            {
                pegou = false;
            }
        }
        return pegou;
    }
    // Use this for initialization
    void Awake()
    {
        pegouFrutas = new bool[5];
        if (Game_Player.instancia.Jogador == null)
        {
            Game_Player.instancia.PegarJogador();
        }
        //Game_Player.game_player.reniciar();
        moedas = GameObject.FindGameObjectsWithTag("Coin");
        total_moedas = moedas.Length;
        jogo_rodando = true;
        checkpoints = GameObject.FindObjectsOfType<CheckPoint>();
        Nivel ni = Game_Player.instancia.RetornarNivel(Application.loadedLevel);
        if (ni == null)
        {
            ni = new Nivel();
        }
        Game_Player.instancia.AdicionarNivel(Application.loadedLevel, ni);
        Game_Player.instancia.Iniciou_fase = true;
        jaAdicionouVidaFrutas = false;
        
    }
    void Start()
    {
        
        
        pontuacao_nivel = 0;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Game_Player.instancia.Jogador == null)
        {
            Game_Player.instancia.PegarJogador();
        }
        if (Game_Player.instancia != null && !reiniciar)
        {
            Game_Player.instancia.reniciar();
            reiniciar = !reiniciar;
        }
        VerificarFrutasMaxima();
        LimparHabilidades();
    }

    private void VerificarFrutasMaxima()
    {
        if (jaAdicionouVidaFrutas || !verificarSePegouTodasAsFrutas())
        {
            return;
        }
        jaAdicionouVidaFrutas = true;
        Game_Player.instancia.Vidas_Extras++;
        Game_Conquistas.instancia.ConquistaTodasAsFrutas();
    }
    public void LimparHabilidades()
    {
        if ((comecou_habilidade) && !Game_Player.instancia.Em_Habilidade)
        {
            bool condicao_mov_esq = (jogador_momento.transform.position.x < corpo_habilidade.transform.position.x - 4.0f &&
                !jogador_momento.GetComponent<MovementController>().Direita && habilidade_em_x);
            bool condicao_mov_dir = (jogador_momento.transform.position.x > corpo_habilidade.transform.position.x + 4.0f &&
                jogador_momento.GetComponent<MovementController>().Direita && habilidade_em_x);
            if (Mathf.Abs(corpo_habilidade.transform.position.x - jogador_momento.transform.position.x) >= distancia_cancelar_habilidade.x ||
                Mathf.Abs(corpo_habilidade.transform.position.y - jogador_momento.transform.position.y) >= distancia_cancelar_habilidade.y ||
                jogador_momento != Game_Player.instancia.Jogador || !Game_Player.instancia.Movimento_Atual.grounded ||
                 condicao_mov_esq || condicao_mov_dir)
                
            {
                PararHabilidade();
            }
        }
    }
   public void IniciarHabilidade(Transform jogador, Transform objeto, Vector2 distancia, bool em_x)
    {
        comecou_habilidade = true;
        corpo_habilidade = objeto;
        jogador_momento = jogador;
        distancia_cancelar_habilidade = distancia;
        habilidade_em_x = em_x;
    }
    public void PararHabilidade()
   {
       if (!Game_Player.instancia.Em_Habilidade)
       {
       comecou_habilidade = false;
       Game_Player.instancia.LimparHabilidades();
           
       }
   }
    public void fimJogo(Transform jogador_hit)
    {
        if (jogador_hit == null)
        {
            return;
        }
        ReiniciarJogo();
        /*
        if (Game_Player.game_player.VerificarReiniciarJogo(jogador_hit))
        {
            ReiniciarJogo();
        }else
        {
            //Game_Player.game_player.ativar_segundo_jogador();
            Game_Player.game_player.InativarJogadorMorto();
         
        }
         */

    }

    public void ReiniciarJogo()
    {
        CheckPoint ckbesc = null;
        Game_Player.instancia.reiniciar_jogadores();
        Transform jogador = Game_Player.instancia.Jogador;
		Game_Player.instancia.Buraco = false;

        if ((Game_Player.instancia.VidasTotais) > 0)
        {
            int ultimo = -1;
            foreach (CheckPoint ckb in checkpoints)
            {
                if ((ckb.numeracao > ultimo && ckb.PassouJogador) || ckb.inicial && ultimo == -1)
                {
                    ultimo = ckb.numeracao;
                    ckbesc = ckb;
                }
            }

        }
        else
        {
            Game_Player.instancia.Jogador = null;
            checkpoints = null;
            Game_Player.instancia.Nivel_Game_Over = Application.loadedLevel;
            Application.LoadLevel(Game_Player.instancia.game_over);
            Game_Player.instancia.ZerarVariaveis();
            return;

        }
        if (Game_Player.instancia.Jogador == null)
        {
            return;
        }
        posicao = new Vector3(ckbesc.retornar_posicoes().x, ckbesc.retornar_posicoes().y, jogador.position.z);
        Game_Player.instancia.ReiniciarPosicoesJogadores(posicao);
        Game_Player.instancia.Vidas_Extras--;
        jogador.GetComponent<MovementController>().DeixarInvencivel();
    }
    public void adicionar_moeda(int quantidade)
    {
        Game_Player.instancia.Moedas += quantidade;
        if (Game_Player.instancia.Moedas >= total_moedas)
        {
            Game_Player.instancia.Vidas_Extras++;
        }
    }
    public void MatarPersonagem()
    {
        Transform jogador = Game_Player.instancia.Jogador;   

        jogador.GetComponent<MovementController>().SetAllCollidersStatus(false);
        jogador.GetComponent<MovementController>().vivo = false;
        jogador.GetComponent<SpriteRenderer>().sortingLayerName = "Foregroud";
        jogador.GetComponent<Rigidbody2D>().AddForce(forcanoPersonagem);
        SoundManager.instance.PlaySingle(somMorrendo);
        //Game_Player.game_player.acionar_segundo_jogador();
        
    }
}
