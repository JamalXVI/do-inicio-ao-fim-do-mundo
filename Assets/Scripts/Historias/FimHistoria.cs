using UnityEngine;
using System.Collections;

public class FimHistoria : MonoBehaviour {
    public GameObject transicao;
    public int nivel;
    public int idHistoriaLoja;
    public bool naoEhHistoria = false;
    void Start()
    {
        MudarDeCena(nivel);
    }
    private void MudarDeCena(int nivel)
    {
        Game_Temp.instancia.TrocarFase = true;
        if (Game_Player.instancia.JaFoiPerguntado)
        {
             Game_Temp.instancia.FaseTroca = nivel;

        }
        else
        {
            Game_Player.instancia.JaFoiPerguntado = true;
            Game_Temp.instancia.FaseTroca = Game_Temp.instancia.NivelPerguntas;
        }
        Game_Loja.instancia.itemsDaLoja[idHistoriaLoja].Liberado = true;
        Game_Loja.instancia.itemsDaLoja[idHistoriaLoja].Comprado = true;
        if (!naoEhHistoria)
        {
            Game_Temp.instancia.EhHistoria = true;
            Game_Player.instancia.EnviarInfoNivel = true;

        }
        else
        {
            Game_Temp.instancia.EhHistoria = false;

        }
        Game_Temp.instancia.NotificacaoLoja = true;
        transicao.SetActive(true);
        //Application.LoadLevel(Game_Player.game_player.carregando);
    }
}
