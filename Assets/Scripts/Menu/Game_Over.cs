using UnityEngine;
using System.Collections;

public class Game_Over : MonoBehaviour {

	public void reiniciar()
    {
        ZerarValores();

        Application.LoadLevel(Game_Player.instancia.Nivel_Game_Over);

    }
    public void sair()
    {
        ZerarValores();
        Application.LoadLevel(Game_Player.instancia.troca_fases);
    }

    private static void ZerarValores()
    {

        Game_Player.instancia.Moedas = 0;
        Game_Player.instancia.QuantidadeItemFase = 0;
        Game_Player.instancia.QuantidadeInimigosFase = 0;
    }
}
