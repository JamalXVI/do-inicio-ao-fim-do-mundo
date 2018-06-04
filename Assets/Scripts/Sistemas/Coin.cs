using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    private Gerenciador gerenciador;
    private bool japegou;
    [SerializeField]
    private AudioClip somMoeda;
    void Awake()
    {
        gerenciador = GameObject.FindObjectOfType<Gerenciador>();
    }
    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.gameObject.tag == "ColiderP" && !japegou)
        {
            japegou = true;
            gameObject.SetActive(false);
            gerenciador.adicionar_moeda(1);
            SoundManager.instance.PlaySingle(somMoeda);
            //Adicionar_Moedas();
        }
    }
    void Adicionar_Moedas()
    {
        Nivel n = Game_Player.game_player.RetornarNivel(Application.loadedLevel);
        
        n.Numero_Moedas++;
        Game_Player.game_player.AdicionarNivel(Application.loadedLevel, n);
    }
}
