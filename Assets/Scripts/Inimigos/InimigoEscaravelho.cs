using UnityEngine;
using System.Collections;

public class InimigoEscaravelho : Game_Inimigo {
    private Gerenciador gerenciador;
    void Awake()
    {
        gerenciador = FindObjectOfType<Gerenciador>();
    }
    void OnCollisionEnter2D(Collision2D colisor)
    {
        if (base.IniciarColisaoJogador(colisor.gameObject))
        {
            MovementController vivojog = colisor.gameObject.GetComponentInParent<MovementController>();
            if (vivojog.vivo && !vivojog.invencivel)
            {
                gerenciador.MatarPersonagem();
                Game_Player.game_player.MatarJogador();
            }

        }
    }
}
