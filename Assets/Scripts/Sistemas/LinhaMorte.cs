using UnityEngine;
using System.Collections;

public class LinhaMorte : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        verificar_se_passou_jogador();
	}
    public void verificar_se_passou_jogador()
    {
        Vector2 VetorInicial = new Vector2(transform.position.x, transform.position.y);
        Vector2 VetorFinal = new Vector2(transform.position.x+999999999, (transform.position.y));
        Debug.DrawLine(VetorInicial, VetorFinal, Color.red);
        if (Physics2D.Linecast(transform.position, VetorFinal,
            1 << LayerMask.NameToLayer("Jogador")))
        {
			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 999999,
			                                     1 << LayerMask.NameToLayer("Jogador"));

            //FindObjectOfType<Gerenciador>().matarPersonagem();
			if (Game_Player.game_player.VerificarSeEhPrincipal(hit.transform)) {
				hit.transform.GetComponent<MovementController>().SetAllCollidersStatus(false);
                hit.transform.GetComponent<MovementController>().vivo = false;
                hit.transform.GetComponent<SpriteRenderer>().sortingLayerName = "Foregroud";
               // Game_Player.game_player.AcionarSegundoJogador();
                
			}else{
				Game_Player.game_player.resetar_jogador();
			}
            
        }

    }

}
