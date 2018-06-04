using UnityEngine;
using System.Collections;
using System;

public class BesouroRaro : MonoBehaviour {
    [SerializeField]
    private AudioClip somItemRaro;
    private int idItem = ConstantesDoSistema.IdPedra;
    // Use this for initialization
    void OnTriggerEnter2D(Collider2D colisor)
    {

        VirarItem(colisor.gameObject);
    }
    private void VirarItem(GameObject colisor)
    {
        if (IniciarColisaoJogador(colisor.gameObject))
        {
            SoundManager.instance.PlaySingle(somItemRaro);
            AdicionarItem();
            if (!Game_Loja.instancia.itemsDaLoja[4].Liberado)
            {
                Game_Loja.instancia.itemsDaLoja[4].Liberado = true;
                Game_Loja.instancia.itemsDaLoja[6].Liberado = true;
                Game_Loja.instancia.itemsDaLoja[7].Liberado = true;
                Game_Loja.instancia.itemsDaLoja[8].Liberado = true;
                Game_Temp.instancia.NotificacaoLoja = true;
            }
            Destroy(this.gameObject, 0f);

        }
    }
    void AdicionarItem()
    {
        if (Game_Player.game_player.quantidade_item[idItem] <= 0)
        {
            Game_Player.game_player.quantidade_item[idItem] += 1;
        }
        Game_Player.game_player.QuantidadeInimigosFase++;
    }
    public Boolean IniciarColisaoJogador(GameObject jogador)
    {

        if (jogador.tag.StartsWith("ColiderP"))
        {
            return true;
        }
        return false;
    }
}
