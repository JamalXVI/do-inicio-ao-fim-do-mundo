using UnityEngine;
using System.Collections;

public class Parede_Subir : MonoBehaviour {
    public Botao_Subir botao;
    public Vector2 distancia;
    public Vector2 velocidade;
    public Collider2D ignorar;
    private bool iniciar;
    private bool terminar;
    private Vector2 posiciao_saida;
    private Rigidbody2D corpo;
    private bool saiu_x;
    private bool saiu_y;

	// Use this for initialization
	void Start () {
        iniciar = false;
        terminar = false;
        saiu_x = false;
        saiu_y = false;

        corpo = GetComponent<Rigidbody2D>();
        foreach (Transform filho in this.transform)
        {
            Collider2D col = filho.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(ignorar, col);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (botao.apertado && !iniciar)
        {
            iniciar = true;
            posiciao_saida = new Vector2(transform.position.x, transform.position.y);
            corpo.velocity = velocidade;
        }
	}
    void FixedUpdate()
    {
        if (iniciar && !terminar)
        {
            if (!saiu_x)
            {
                verificar_em_x();
            }
            if (!saiu_y)
            {
                verificar_em_y();   
            }
            
           
            if (saiu_x && saiu_y)
            {
                corpo.velocity = new Vector2(0, 0);
                
                terminar = true;
            }   
        }
    }
    private void verificar_em_y()
    {
        if (distancia.y < 0)
        {
            if (transform.position.y <= posiciao_saida.y + distancia.y)
            {
                corpo.velocity = new Vector2(corpo.velocity.x, 0);
                saiu_y = true;
            }
        }
        else
        {
            if (transform.position.y >= posiciao_saida.y + distancia.y)
            {
                corpo.velocity = new Vector2(corpo.velocity.x, 0);
                saiu_y = true;
            }
        }
    }
    private void verificar_em_x()
    {
        if (distancia.x < 0)
        {

            if (transform.position.x <= posiciao_saida.x + distancia.x)
            {
                corpo.velocity = new Vector2(0, corpo.velocity.y);
                saiu_x = true;
            }
        }
        else
        {
            if (transform.position.x >= posiciao_saida.x + distancia.x)
            {
                corpo.velocity = new Vector2(0, corpo.velocity.y);
                saiu_x = true;
            }
        }
    }
}
