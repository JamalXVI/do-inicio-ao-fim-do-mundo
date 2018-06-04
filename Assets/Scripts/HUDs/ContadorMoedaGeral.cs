    using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContadorMoedaGeral : MonoBehaviour {
    private Text texto; 
	// Use this for initialization
	void Start () {
        texto = GetComponent<Text>();
	}
	
	// Update is called once per frame
    void Update()
    {
        texto.text = Game_Player.game_player.Moedas_Jogo.ToString();
	
	}
}
