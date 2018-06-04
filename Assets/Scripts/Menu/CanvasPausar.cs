using UnityEngine;
using System.Collections;

public class CanvasPausar : MonoBehaviour {
    private Gerenciador gerenciador;
	// Use this for initialization
	void Start () {
        gerenciador = FindObjectOfType<Gerenciador>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void RepetirFase()
    {
        Zerar_Valores();
        Application.LoadLevel(Application.loadedLevel);
    }

    private void Zerar_Valores()
    {
        gerenciador.Pausar_Jogo = false;
        Game_Player.game_player.Moedas = 0;
        Game_Player.game_player.QuantidadeItemFase = 0;
        Game_Player.game_player.QuantidadeInimigosFase = 0;
    }
    public void SelecaoFases()
    {
        Zerar_Valores();
        Application.LoadLevel(Game_Player.game_player.troca_fases);
    }
    public void Sair()
    {
        Application.Quit();
    }
    public void RetornarJogo()
    {   
        gerenciador.Pausar_Jogo = false;
    }
}
