using UnityEngine;
using System.Collections;
using System;

public class Caixa : MonoBehaviour {
    Gerenciador gerenciador;
    public Vector2 distancia_caixa = new Vector2(12f,12f);
    public bool caindo;
    Rigidbody2D corpo;
    private bool levantandoPedra;
    private MovementController primitivo;
    private Vector2 posicaoAtualPersonagem;
    private Vector2 posicaoAtualCaixa;
    private bool empurrada;
    private float distanciaEntrePersonagemECaixaX;
    private float distanciaEntrePersonagemECaixaY;
    [SerializeField]
    private bool levantavel = true;
    void Start()
    {
        empurrada = false;
        levantandoPedra = false;
        corpo = GetComponent<Rigidbody2D>();
        gerenciador = FindObjectOfType<Gerenciador>();
        InanimarPedra(false);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        ImpedirContatoCaixa(col);
        if (col.gameObject.name.Contains("Socolinha"))
        {
            ColisaoSocolinha(col);

        }
        else if (col.gameObject.name.Contains("Primitivo") && levantavel)
        {
            ColisaoPrimitivo(col);
        }

 
        
    }
    

    private void ImpedirContatoCaixa(Collision2D col)
    {
        if (col.gameObject == gerenciador.corpo_habilidade)
        {
            col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    

    private void ColisaoPrimitivo(Collision2D col)
    {
        if (col.gameObject.Equals(Game_Player.instancia.Jogador.gameObject))
        {
            if (VerificarColisao(col))
            {
                Game_Player.instancia.AtivarHabilidades("Levantar");
                float total = distancia_caixa.x + this.GetComponent<BoxCollider2D>().size.x;
                float totaly = distancia_caixa.y + this.GetComponent<BoxCollider2D>().size.y;
                gerenciador.IniciarHabilidade(Game_Player.instancia.Jogador, this.transform, new Vector2(total,
                    totaly), true);
            }
            else
            {
                gerenciador.LimparHabilidades();
            }
        }

    }


    private void ColisaoSocolinha(Collision2D col)
    {
        if (col.gameObject.Equals(Game_Player.instancia.Jogador.gameObject))
        {
            if (VerificarColisao(col))
            {
                if (!caindo)
                {
                    Game_Player.instancia.AtivarHabilidades("Mover");
                    float total = distancia_caixa.x + this.GetComponent<BoxCollider2D>().size.x;
                    float totaly = distancia_caixa.y + this.GetComponent<BoxCollider2D>().size.y;
                    gerenciador.IniciarHabilidade(Game_Player.instancia.Jogador, this.transform, new Vector2(total,
                        totaly), true);
                    //Game_Player.game_player.Movimento_Atual.iniciar_habilidades("Mover");
                }
            }
            else
            {
                gerenciador.LimparHabilidades();
            }
        }
    }

    private bool VerificarColisao(Collision2D col)
    {
        MovementController mov = col.gameObject.GetComponent<MovementController>();
        if (col.transform.position.x > this.transform.position.x && mov.Direita ||
        col.transform.position.x < this.transform.position.x && !mov.Direita)
        {
            return false;
        }
        return col.transform.position.y < (this.transform.position.y + this.GetComponent<BoxCollider2D>().size.y);
    }

    // Update is called once per frame
    void Update ()
    {

        CondicaoPararMover();
        CondicaoParada();
        CorrigirParada();
        /*
        if (CancelaHabilidadeMover())
        {
            PararHabilidades();
        }
        */
    }

    private bool CancelaHabilidadeMover()
    {
        if (!empurrada)
        {
            return false;
        }
        return MovimentacaoY();
    }

    private void CorrigirParada()
    {
        if (corpo.velocity.x > 0 && !empurrada)
        {
           // corpo.isKinematic = true;
        }
    }

    private void CondicaoParada()
    {
        if (Mathf.Abs(corpo.velocity.y) >= 5)
        {
            caindo = true;
            PararHabilidades();
            
        }
        else if (caindo && Mathf.Abs(corpo.velocity.y) < 1)
        {
            caindo = false;
            InanimarPedra(false);
        }
    }

    private void PararHabilidades()
    {
        //Corrigir o erro da caixa caindo no abismo;
        if (Game_Player.instancia.Habilidade_Atual != null)
        {
            Game_Player.instancia.PararHabilidades();
            InanimarPedra(true);
            empurrada = false;
        }
    }
    private void CondicaoPararMover()
    {
        if (CorpoParado() && corpo.velocity.x > 0)
        {
            corpo.velocity = new Vector2(0, corpo.velocity.y);
        }
    }

    public bool CorpoParado()
    {
        return corpo.constraints == RigidbodyConstraints2D.FreezeAll;
    }
    void OnCollisionExit2D(Collision2D col)
    {
    }

    public void PrimitivoSegurandoCaixa(MovementController mov)
    {
        primitivo = mov;
        AnimandoPedraPrimitivo(false);
        IniciarPosicaoPersonagem(mov);
    }

    private void IniciarPosicaoPersonagem(MovementController mov)
    {
        posicaoAtualPersonagem = new Vector2(mov.transform.position.x, mov.transform.position.y);
        posicaoAtualCaixa = new Vector2(transform.position.x, transform.position.y);
    }


    private bool VerificarMovimentacaoObjetos()
    {
        return primitivo.transform.position.x != posicaoAtualPersonagem.x || 
                transform.position.x != posicaoAtualCaixa.x || MovimentacaoY();
    }

    private bool MovimentacaoY()
    {
        return primitivo.transform.position.y != posicaoAtualPersonagem.y || transform.position.y != posicaoAtualCaixa.y;
    }

    public void FinalizarHabilidadeLevantar()
    {
        AnimandoPedraPrimitivo(true);
        InanimarPedra(true);
    }

    private void AnimandoPedraPrimitivo(bool valor)
    {
        levantandoPedra = valor;
        Animator animPrimitivo = primitivo.GetComponent<Animator>();
        animPrimitivo.SetBool("PararLevantar", levantandoPedra);
        InanimarPedra(valor);
    }

    public void InanimarPedra(bool valor)
    {
        if (valor)
        {
            corpo.constraints = RigidbodyConstraints2D.None;
            corpo.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            corpo.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        corpo.isKinematic = false;
    }
    public void IniciarCaixaEmpurrar(MovementController mov)
    {
        distanciaEntrePersonagemECaixaX = Mathf.Abs(mov.transform.position.x)
         - Mathf.Abs(this.transform.position.x);
        distanciaEntrePersonagemECaixaY = Mathf.Abs(mov.transform.position.y)
         - Mathf.Abs(this.transform.position.y);
        if (!levantandoPedra && primitivo != null)
        {
            FinalizarHabilidadeLevantar();
        }
        IniciarPosicaoPersonagem(mov);
        empurrada = true;
    }
    public bool VerificarDistanciaEntreCaixaEPersonagem(MovementController mov)
    {
        var distAtualX = Mathf.Abs(mov.transform.position.x) - Mathf.Abs(this.transform.position.x);
        //var distAtualY = Mathf.Abs(mov.transform.position.y) - Mathf.Abs(this.transform.position.y);
        return Mathf.Abs(distAtualX - distanciaEntrePersonagemECaixaX) > 2.0f; //||
          //  Mathf.Abs(distAtualY - distanciaEntrePersonagemECaixaY) > 2.0f;
    }
}
