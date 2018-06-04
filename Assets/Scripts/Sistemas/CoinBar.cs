using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinBar : MonoBehaviour {
    private float tempo_porcentual;
    private Image imagem;
    private Gerenciador gerenciador;
    private int moedas_atual = -1;
    void Awake()
    {
        imagem = gameObject.GetComponent<Image>();
        gerenciador = GameObject.FindObjectOfType<Gerenciador>();
    }
    void Update()
    {
        if (gerenciador.jogo_rodando && moedas_atual != Game_Player.game_player.Moedas)
        {
            
            moedas_atual = Game_Player.game_player.Moedas;
            float num = ((float)Game_Player.game_player.Moedas);
            float dem = ((float)gerenciador.TotalMoedas);
            tempo_porcentual =  num/ dem;
            imagem.fillAmount = tempo_porcentual;
        }

    }
}
