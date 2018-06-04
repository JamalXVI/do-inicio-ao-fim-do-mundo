using UnityEngine;
using System.Collections;

public class Catapulta : MonoBehaviour {
    public Vector2 forca_catapulta;
    public Transform ponto_forca;
    private Rigidbody2D corpo;
    private float velocidade_jogador=80f;
    private float forca_pulo_jogador = 9000f;
	// Use this for initialization
    void Start()
    {
        corpo = GetComponent<Rigidbody2D>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!Game_Player.game_player.gameObject)
        {
            return;
        }
        if (col.gameObject.Equals(Game_Player.game_player.Jogador.gameObject))
        {
            pular_jogador(Game_Player.game_player.Jogador.GetComponent<Rigidbody2D>());
            pular_jogador(Game_Player.game_player.Jogador_S.GetComponent<Rigidbody2D>());
           // corpo.AddForceAtPosition(forca_catapulta,
            //    new Vector2(ponto_forca.position.x, ponto_forca.position.y));

        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (!Game_Player.game_player.gameObject)
        {
            return;
        }
        if (col.gameObject.Equals(Game_Player.game_player.Jogador.gameObject))
        {
            MovementController mov = Game_Player.game_player.Jogador_S.GetComponent<MovementController>();
            acao_jogador(mov);
            acao_jogador(Game_Player.game_player.Movimento_Atual);

        }
                    
    }
    void pular_jogador(Rigidbody2D jogador)
    {
        jogador.AddForce(new Vector2(0, forca_pulo_jogador));
    }
    void acao_jogador(MovementController mov)
    {
        mov.Empurrar = velocidade_jogador;
        mov.Catapulta = true;
        mov.AcaoPular();
    }
}
        