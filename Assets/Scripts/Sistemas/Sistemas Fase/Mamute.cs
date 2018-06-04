using UnityEngine;
using System.Collections;

public class Mamute : MonoBehaviour {

    private bool direita;
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
    [SerializeField]
    private bool precisaTrigger;
    private bool triggerAcionarJogador;

    // Use this for initialization
    void Start()
    {
        VerificarDireita();
        acabou_recomecar = true;
        triggerAcionarJogador = false;
        posicaoInicial = new Vector2(this.transform.position.x, this.transform.position.y);
        esperar = false;
        movimentoReverso = false;
        corpo = GetComponent<Rigidbody2D>();
    }
    private void VerificarDireita()
    {
        direita = (velocidade.x >= 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (CondicaoAcionarJogador())
        {
            if (!esperar)
            {
                Movimentar();
            }
            else
            {
                Esperar();
            }
            VerificarDireita();
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
    private bool CondicaoAcionarJogador()
    {
        if (!precisaTrigger)
        {
            return true;
        }
        if (precisaTrigger && triggerAcionarJogador)
        {
            return true;
        }
        return false;
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
    void OnCollisionStay2D(Collision2D col)
    {
        
        if (!col.gameObject.tag.Contains("Chao"))
        {
            if (col.gameObject.tag.Contains("Player"))
            {
               MovementController mov = col.gameObject.GetComponent<MovementController>();
               mov.Empurrar = corpo.velocity.x;
            }
            else
            {
                col.rigidbody.velocity = new Vector2(col.rigidbody.velocity.x + corpo.velocity.x,
            col.rigidbody.velocity.y);
            }

        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Contains("Chao"))
        {
            if (col.transform.position.x >= transform.position.x)
            {
                direita = false;
            }
            else
            {
                direita = true;
            }
            triggerAcionarJogador = true;

        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (!col.gameObject.tag.Contains("Chao"))
        {
            if (col.gameObject.tag.Contains("Player"))
            {
                MovementController mov = col.gameObject.GetComponent<MovementController>();
                mov.Empurrar = 0;
            }
            else
            {
            }
            triggerAcionarJogador = false;
        }
    }
}
