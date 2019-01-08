using UnityEngine;
using System.Collections;
using System;

public class TerraFofaDireita : MonoBehaviour
{
    private Gerenciador gerenciador;
    [SerializeField]
    private Transform terraFofa;
    [SerializeField]
    private Transform terraFofaEsquerda;
    private bool cavucado { get; set; }
    [SerializeField]
    private Vector2 distancia_cancelar = new Vector2(10, 10);
    private bool cavucou;
    [SerializeField]
    private Transform planta;
    private bool plantou;

    // Use this for initialization
    void Start()
    {
        plantou = false;
        cavucou = false;
        gerenciador = FindObjectOfType<Gerenciador>();

    }
    void OnCollisionEnter2D(Collision2D col)
    {

        if (!cavucou)
        {
            VerificarSeCavucar(col);
        }
        if (!plantou)
        {
            VeficarFlorescer(col);
        }
        if (cavucou && col.gameObject.name.Contains("Primitivo") || plantou && col.gameObject.name.Contains("Socolinha"))
        {
            gerenciador.LimparHabilidades();
        }
    }
    private void VeficarFlorescer(Collision2D col)
    {
        if (CondicaoIniciarHabilidade("Socolinha", col))
        {
            if (CondicaoVerificarContato(col))
            {
                IniciarHabilidade("Florescer");
            }
        }
        else
        {
            gerenciador.LimparHabilidades();

        }
    }
    private void VerificarSeCavucar(Collision2D col)
    {
        if (CondicaoIniciarHabilidade("Primitivo", col))
        {
            if (CondicaoVerificarContato(col))
            {
                IniciarHabilidade("Cavucar");
            }
            else
            {
                gerenciador.LimparHabilidades();
            }

        }
    }
    private void IniciarHabilidade(string habilidade)
    {
        Game_Player.instancia.AtivarHabilidades(habilidade);
        float total = distancia_cancelar.x + this.GetComponent<BoxCollider2D>().size.x;
        float totaly = distancia_cancelar.y + this.GetComponent<BoxCollider2D>().size.y;
        gerenciador.IniciarHabilidade(Game_Player.instancia.Jogador, this.transform, new Vector2(total,
            totaly), false);
    }
    private bool CondicaoVerificarContato(Collision2D col)
    {
        return col.transform.position.y > (this.transform.position.y + this.GetComponent<BoxCollider2D>().size.y);
    }
    private bool CondicaoIniciarHabilidade(string personagem, Collision2D col)
    {
        if (col.gameObject.Equals(Game_Player.instancia.Jogador.gameObject) && col.gameObject.name.Contains(personagem))
        {
            return true;
        }
        return false;
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.Equals(Game_Player.instancia.Jogador.gameObject)
         && col.gameObject.name.Contains("Primitivo"))
        {
            PararHabilidades();
        }
    }
    private void PararHabilidades()
    {
        //Corrigir o erro da caixa caindo no abismo;
        if (Game_Player.instancia.Habilidade_Atual != null)
        {
            Game_Player.instancia.PararHabilidades();
        }
    }
    public void FazerCavucar()
    {
        cavucou = true;
        Animator anim = terraFofaEsquerda.GetComponent<Animator>();
        anim.SetTrigger("Cavucar");
    }

    public void FazerFlorescer()
    {
        plantou = true;
        Animator anim = planta.GetComponent<Animator>();
        anim.SetTrigger("Florescer");
    }
}