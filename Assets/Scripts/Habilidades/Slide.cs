using UnityEngine;
using System.Collections;

public class Slide : MonoBehaviour {
    private Gerenciador gerenciador;
    [SerializeField]
    private Vector2 distancia = new Vector2(12, 99999);
	// Use this for initialization
	void Start () {
        gerenciador = FindObjectOfType<Gerenciador>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionStay2D(Collision2D col)
    {

        if (col.gameObject.Equals(Game_Player.game_player.Jogador.gameObject) && col.gameObject.name.Contains("Primitivo"))
        {
            AtivarHabilidade(col);
        }
    }

    private void AtivarHabilidade(Collision2D col)
    {
        Game_Player.game_player.AtivarHabilidades("Slide");
        float total = distancia.x + this.GetComponent<BoxCollider2D>().size.x;
        gerenciador.IniciarHabilidade(Game_Player.game_player.Jogador, this.transform, new Vector2(total,
                    distancia.y), true);
    }

}
