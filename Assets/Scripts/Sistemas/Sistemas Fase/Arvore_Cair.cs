using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Arvore_Cair : MonoBehaviour {
    public float distancia_y_enxergar;
    public Transform detetor;
    private Rigidbody2D corpo;
    public Transform ponto_forca;
    public Vector2 forca;
    private bool caido;
    public bool direita;
    private List<BoxCollider2D> colliders;
    [SerializeField]
    private bool derrubavel = true;
    public bool Caindo
    {
        get { return caido; }
        set { caido = value; }
    }
    
	// Use this for initialization
	void Start () {
	    corpo = GetComponent<Rigidbody2D>();
        corpo.isKinematic = true;
        colliders = new List<BoxCollider2D>();
        foreach (Transform filho in transform)
	    {
            foreach (Transform f in filho)
	        {
                if (f.name.Contains("Tronco"))
                {
                    colliders.Add(f.gameObject.GetComponent<BoxCollider2D>());
                }
	        }
            
		 
	    }
	}
	
	// Update is called once per frame
	void Update () {
        if (Jogador_Passou() && derrubavel)
        {
            DerrubarArvore();
        }
	}
    public void DerrubarArvore()
    {
        corpo.isKinematic = false;
        corpo.AddForceAtPosition(forca, new Vector2(ponto_forca.position.x, ponto_forca.position.y));
        foreach (Collider2D col in colliders)
        {
            /*
            Game_Player.game_player.Jogador.GetComponent<MovementController>().
                ignorar_colisao_personagens(col.gameObject, true);
            Game_Player.game_player.Jogador_S.GetComponent<MovementController>().
                ignorar_colisao_personagens(col.gameObject, true);
             */
        }
        caido = true;
    }
    private bool Jogador_Passou()
    {
        if (caido)
        {
            return false;
        }
        Vector2 VetorInicial = new Vector2(detetor.position.x, detetor.position.y);
        Vector2 VetorFinal = new Vector2(detetor.position.x, (transform.position.y + distancia_y_enxergar));
        Debug.DrawLine(VetorInicial, VetorFinal, Color.yellow);
        if (Physics2D.Linecast(detetor.position, VetorFinal,
           1 << LayerMask.NameToLayer("Jogador")))
        {
            return true;
        }
        return false;
    }
     void OnCollisionEnter2D(Collision2D col)
    {
        if (corpo.velocity.magnitude < 0.01)
        {
            corpo.AddForceAtPosition(forca, new Vector2(ponto_forca.position.x, ponto_forca.position.y));
        }
    }
     void OnCollisionExit2D(Collision2D col)
     {
     }
}
