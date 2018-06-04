using UnityEngine;
using System.Collections;

public class Enxame : Game_Inimigo {
    private Gerenciador gerenciador;
    [SerializeField]
    private Vector2 distancia;
    [SerializeField]
    private float tempoEsperar;
    [SerializeField]
    private Vector2 velocidade;
    private float ultimoTempo;
    private bool esperar;
    private Vector2 posicaoInicial;
    private Rigidbody2D corpo;
    private bool movimentoReverso;
    private bool acabou_recomecar;
	// Use this for initialization
	void Start () {
        acabou_recomecar = true;
        corpo = GetComponent<Rigidbody2D>();
        posicaoInicial = new Vector2(this.transform.position.x, this.transform.position.y);
        esperar = false;
        movimentoReverso = false;
        gerenciador = GameObject.FindObjectOfType<Gerenciador>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!esperar)
        {
            Movimentar();
        }
        else
        {
            Esperar();
        }
	}
    private void Esperar()
    {
        if (Time.time >= ultimoTempo + tempoEsperar)
        {
            FinalizarEspera();
        }
    }
    private void FinalizarEspera()
    {
        esperar = false;
        movimentoReverso = !movimentoReverso;
        acabou_recomecar = true;
    }
    private void Movimentar()
    {
        if (Mathf.Abs(this.transform.position.x - posicaoInicial.x) >= distancia.x &&
            Mathf.Abs(this.transform.position.y - posicaoInicial.y) >= distancia.y)
        {
            if (!acabou_recomecar)
            {
                PararMovimento();
            }
            else
            {
                DarVelocidade();
            }
        }
        else
        {
            acabou_recomecar = false;
            DarVelocidade();
        }
    }
    private void DarVelocidade()
    {
        bool em_x = true;
        bool em_y = true;
        Vector2 velocidadeReal = movimentoReverso ? new Vector2(velocidade.x, velocidade.y) : new Vector2(-velocidade.x, -velocidade.y);
        if (Mathf.Abs(this.transform.position.x - posicaoInicial.x) >= distancia.x)
        {
            em_x = false;
        }
        if (Mathf.Abs(this.transform.position.y - posicaoInicial.y) >= distancia.y)
        {
            em_y = false;
        }
        if (em_x && em_y)
        {
            corpo.velocity = velocidadeReal;
        }
        else if (em_x)
        {
            corpo.velocity = new Vector2(velocidadeReal.x, 0);
        }
        else if (em_y)
        {
            corpo.velocity = new Vector2(0, velocidadeReal.y);
        }
        else
        {
            corpo.velocity = velocidadeReal;
        }
    }
    private void PararMovimento()
    {
        corpo.velocity = new Vector2(0, 0);
        esperar = true;
        ultimoTempo = Time.time;
    }
    void OnTriggerEnter2D(Collider2D colisor)
    {
        foreach (Transform col in Game_Player.game_player.Jogador)
        {
            if (colisor.gameObject == col.gameObject && vivo)
            {
                VerificarMatarJogador();
            }
        }
        
    }
    private void VerificarMatarJogador()
    {
        MovementController movimentoAtual = Game_Player.game_player.Movimento_Atual;
        if (movimentoAtual.vivo && !movimentoAtual.invencivel)
        {
            gerenciador.MatarPersonagem();
            Game_Player.game_player.MatarJogador();
            Game_Conquistas.instancia.SemRepelente();
        }
    }

}
