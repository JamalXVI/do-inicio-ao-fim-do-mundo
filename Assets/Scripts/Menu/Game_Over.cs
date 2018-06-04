using UnityEngine;
using System.Collections;

public class Game_Over : MonoBehaviour {

	public void reiniciar()
    {
        ZerarValores();

        Application.LoadLevel(Game_Player.game_player.Nivel_Game_Over);

    }
    public void sair()
    {
        ZerarValores();
        Application.LoadLevel(Game_Player.game_player.troca_fases);
    }

    private static void ZerarValores()
    {

        Game_Player.game_player.Moedas = 0;
        Game_Player.game_player.QuantidadeItemFase = 0;
        Game_Player.game_player.QuantidadeInimigosFase = 0;
    }
}
