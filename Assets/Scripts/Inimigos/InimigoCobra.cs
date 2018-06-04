using UnityEngine;
using System.Collections;

public class InimigoCobra : Game_Inimigo {
    [SerializeField]
    private float velocidade;
    [SerializeField]
    private float tempoAndado;
    [SerializeField]
    private float tempoEsperando;
    private int direcao;
    private float contadorTempoAndando;
    private float contadorTempoEsperando;
    private bool movimentando;
    private bool esperando;
    private Rigidbody2D corpo;
    private bool finalizouCobra;
    private Animator anim;
    private Gerenciador gerenciador;
    private int idItem = ConstantesDoSistema.IdCobra;
	// Use this for initialization
	void Awake () {
        ConfiguracaoInicial();
        EncontrarObjetos();
	}

    private void EncontrarObjetos()
    {
        corpo = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gerenciador = FindObjectOfType<Gerenciador>();
    }

    private void ConfiguracaoInicial()
    {
        movimentando = true;
        esperando = false;
        finalizouCobra = false;
        contadorTempoAndando = 0f;
        contadorTempoEsperando = 0f;
        direcao = -1;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!parar_tudo)
        {
            MovimentacaoAndando();
            Esperando();
        }
        else
        {
            EstunarCobra();
        }
	}

    private void EstunarCobra()
    {
        if (!finalizouCobra)
        {
            finalizouCobra = true;
            Movimentar(0);
        }
    }

    private void Esperando()
    {
        if (esperando)
        {
            contadorTempoEsperando += Time.deltaTime;
            Movimentar(0);
            VerificarMovimentar();
        }
    }

    private void VerificarMovimentar()
    {
        if (contadorTempoEsperando >= tempoEsperando)
        {
            contadorTempoEsperando = 0f;
            MudarDirecao();
            esperando = false;
            movimentando = true;
        }
    }

    private void MudarDirecao()
    {
        direcao *= -1;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }

    private void MovimentacaoAndando()
    {
        if (movimentando)
        {
            contadorTempoAndando += Time.deltaTime;
            float velocidadeMov = direcao * velocidade;
            Movimentar(velocidadeMov);
            VerificarParar();
        }
    }

    private void Movimentar(float velocidadeMov)
    {
        corpo.velocity = new Vector2(velocidadeMov, corpo.velocity.y);
    }

    private void VerificarParar()
    {
        if (contadorTempoAndando >= tempoAndado)
        {

            contadorTempoAndando = 0f;
            esperando = true;
            movimentando = false;
        }
    }

    public override void TocandoViolao()
    {
        parar_tudo = true;
    }
    void OnCollisionEnter2D(Collision2D colisor)
    {
        if (parar_tudo)
        {
            VirarItem(colisor);
            return;
        }
        if (base.IniciarColisaoJogador(colisor.gameObject))
        {
            MovementController vivojog = colisor.gameObject.GetComponentInParent<MovementController>();
            if (vivojog.vivo && !vivojog.invencivel)
            {
                gerenciador.MatarPersonagem();
                Game_Player.game_player.MatarJogador();
                Game_Conquistas.instancia.AdicionarMortePorAnimais("cobra");
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), colisor.collider);
            }

        }
    }

    private void VirarItem(Collision2D colisor)
    {
        if (base.IniciarColisaoJogador(colisor.gameObject))
        {
            SoundManager.instance.PlaySingle(somMorrer);
            AdicionarItem();
            Game_Conquistas.instancia.AdicionarAnimal("cobra");
            Destroy(this.gameObject, 0f);

        }
    }
    void AdicionarItem()
    {
        if (Game_Player.game_player.quantidade_item[idItem] <= 0)
        {
            Game_Player.game_player.quantidade_item[idItem] += 1;
        }
        Game_Player.game_player.QuantidadeInimigosFase++;
    }
}
