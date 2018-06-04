using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScrollCamera : MonoBehaviour
{

    private Transform player;

    public float smooth = 0.5f;
    private Vector2 velocidade;
    public Vector2 limiteMax;
    public Vector2 limiteMin;
    private bool camera_mov;
	private bool ignorar_posicao;
	private bool camera_rev;
    private float tempo_m = 0f;
    public List<Vector2> mais_limites;
    public List<Vector2> mais_limites_max;
    public List<Vector2> mais_limites_min;

    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
		camera_rev = false;
        velocidade = new Vector2(0.0f, 0.0f);
    }

    // Update is called once per frame
	private void movimentar_parado()
	{
		float tempo = Game_Player.game_player.tempo_espera_camera_movimento;
		int dir = 1;
		if (!Game_Player.game_player.Movimento_Atual.Direita) {
			dir *= -1;
		}
		if (Game_Player.game_player.distancia_max_camera.x >=
			(transform.position.x - Game_Player.game_player.Jogador.position.x)*dir
		    ) {
			if (!camera_rev) {
				
				transform.position += new Vector3(dir*Game_Player.game_player.velocidade_camera_parado,0,0);
			}
		}else{
			if (tempo_m >= tempo) {
				tempo_m = 0f;
				camera_rev = true;
			}else{
				tempo_m += Time.deltaTime;
			}
		}
		if ((transform.position.x - Game_Player.game_player.Jogador.position.x)*dir > 0) {
			if (camera_rev) {
				transform.position += new Vector3(-dir*Game_Player.game_player.velocidade_camera_parado,0,0);
			}
			
		}else{
			if (tempo_m >= tempo) {
				tempo_m = 0f;
				camera_rev = false;
			}else{
				tempo_m += Time.deltaTime;
			}
		}
	}
    void Update()
    {
        if (Game_Player.game_player.Movimento_Atual == null)
        {
            Game_Player.game_player.reniciar();
        }   
		if (Game_Player.game_player.Movimento_Atual.Tempo_Parado >= Game_Player.game_player.tempo_maximo_parado
		    ) {
			ignorar_posicao = true;
		} else {
			ignorar_posicao = false;
		}
        if (Game_Player.game_player.Suspender_Camera)
        {
            return;
		}
		if (ignorar_posicao) {
			movimentar_parado();
			return;
		}
        Transform player = Game_Player.game_player.Jogador;
        if (player == null)
        {
            return;
        }
        
        mover_por_limite(player, Game_Player.game_player.Movimento_Atual.Direita);

    }
    private void mover_por_limite(Transform player, bool direita)
    {
        Vector2 novaPosicao2D = new Vector2(transform.position.x, transform.position.y);
        Vector2 minimo = limiteMin;
        Vector2 maximo = limiteMax;
        if (mais_limites.Count > 0)
        {
            for (int i = 0; i < mais_limites.Count; i++)
            {
                if (player.position.x >= mais_limites[i].x && player.position.y >= mais_limites[i].y)
                {
                    
                    minimo = mais_limites_min[i];
                    maximo = mais_limites_max[i];
                }
            }
        }
        if ((transform.position.x > minimo.x && maximo.x > transform.position.x) ||
            (player.position.x > minimo.x && maximo.x > player.position.x))
        {
            float direcao = 1f;
            if (!direita)
            {
                direcao = -1f;
            }
            novaPosicao2D.x = player.position.x+direcao*0f;
        }
        if ((transform.position.y > minimo.y && maximo.y > transform.position.y) ||
            (player.position.y > minimo.y && maximo.y > player.position.y))
        {
            novaPosicao2D.y = player.position.y;
        }
        // Vector3 novaPosicao = new Vector3(novaPosicao2D.x, novaPosicao2D.y, transform.position.z);
        //transform.position = Vector3.Slerp(transform.position, novaPosicao, Time.time);
        transform.position = new Vector3(novaPosicao2D.x, novaPosicao2D.y, transform.position.z);
    }
}
