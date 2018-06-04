using UnityEngine;
using System.Collections;
using System;

public class TocarViolao : MonoBehaviour
{
    private Gerenciador gerenciador;
    [SerializeField]
    private Vector2 distancia_cancelar = new Vector2(10, 10);
    [SerializeField]
    private Game_Inimigo inimigo;
    private bool tocou;
    // Use this for initialization
    public Game_Inimigo RetornarInimigo()
    {
        return inimigo;
    }
    void Start () {
        tocou = false;
        gerenciador = FindObjectOfType<Gerenciador>();
    }
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D col)
    {
        VerificarColisao(col);
	}

    private void VerificarColisao(Collider2D col)
    {
        if (!tocou)
        {
            VerificarSeTocar(col);
        }
        if (tocou)
        {
            gerenciador.LimparHabilidades();
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        VerificarColisao(col);
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (CondicaoIniciarHabilidade("Socolinha", col))
        {
            gerenciador.LimparHabilidades();
        }
    }
    private void VerificarSeTocar(Collider2D col)
    {

        if (CondicaoIniciarHabilidade("Socolinha", col))
        {
            MovementController mov = col.GetComponentInParent<MovementController>();
            IniciarHabilidade("Song");
        }
        else
        {
            gerenciador.LimparHabilidades();

        }
    }

    private void IniciarHabilidade(string habilidade)
    {
        Game_Player.game_player.AtivarHabilidades(habilidade);
        float total = distancia_cancelar.x + this.GetComponent<BoxCollider2D>().size.x;
        float totaly = distancia_cancelar.y + this.GetComponent<BoxCollider2D>().size.y;
        gerenciador.IniciarHabilidade(Game_Player.game_player.Jogador, this.transform, new Vector2(total,
            totaly), false);
       // tocou = true;
    }

    private bool CondicaoIniciarHabilidade(string personagem, Collider2D col)
    {
        MovementController mov = col.GetComponentInParent<MovementController>();
        if (mov != null)
        {
            if (mov.gameObject.Equals(Game_Player.game_player.Jogador.gameObject) && mov.gameObject.name.Contains(personagem))
            {
                return true;
            }
        }
        
        return false;
    }
}
