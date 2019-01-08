using UnityEngine;
using System.Collections;

public class Plataforma_Descer : MonoBehaviour {
    public Vector2 distancia = new Vector2(12, 3);
    Gerenciador gerenciador;
    private bool verificaSumir = false;
    [SerializeField]
    private Transform objetoLinha;
    private GameObject coliders;
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!Condicao_Subir(col))
        {
            Acao_Colisao_Plataforma(col);

        }
    }
    void OnCollisionStay2D(Collision2D col)
    {
        if (!Condicao_Subir(col))
        {
            Acao_Colisao_Plataforma(col);
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.Equals(Game_Player.instancia.Jogador.gameObject))
        {
            gerenciador.PararHabilidade();
        }
    }
    private void Acao_Colisao_Plataforma(Collision2D col)
    {
        AtivarHabilidade(col);
    }
    private bool Condicao_Subir(Collision2D col)
    {
        var caixaColisao = GetComponent<BoxCollider2D>();
        if (col.gameObject.Equals(Game_Player.instancia.Jogador.gameObject) &&
            (col.transform.position.y < transform.position.y)
            && (transform.position.x -  caixaColisao.bounds.max.x/2) < col.transform.position.x &&
            (transform.position.x + caixaColisao.bounds.max.x / 2) > col.transform.position.x)
        {
            return true;
        }
        return false;
    }

    private void AtivarHabilidade(Collision2D col)
    {
        if (col.gameObject.Equals(Game_Player.instancia.Jogador.gameObject) && col.gameObject.name.Contains("Socolinha"))
        {
            if (col.transform.position.y > (this.transform.position.y + this.GetComponent<BoxCollider2D>().size.y-1))
            {
                Game_Player.instancia.AtivarHabilidades("Descer");
                float total = distancia.x + this.GetComponent<BoxCollider2D>().size.x;
                float totaly = distancia.y;
                gerenciador.IniciarHabilidade(Game_Player.instancia.Jogador, this.transform, new Vector2(total,
                    totaly), false);
            }
        }
    }

	// Use this for initialization
    void Start()
    {
        gerenciador = FindObjectOfType<Gerenciador>();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (verificaSumir)
        {
            if (objetoLinha.position.y > coliders.transform.position.y)
            {
                verificaSumir = false;
                AtivarColiders(true);
                Game_Player.instancia.PararHabilidades();
                
            }
           
        }
	}
    private void VerificarAnularDescer()
    {
    }

    internal void SumirColider(GameObject gameObject)
    {
        verificaSumir = true;
        coliders = gameObject;
        bool ativar = false;
        AtivarColiders(ativar);
    }

    private void AtivarColiders(bool ativar)
    {
        coliders.SetActive(ativar);
    }
}
