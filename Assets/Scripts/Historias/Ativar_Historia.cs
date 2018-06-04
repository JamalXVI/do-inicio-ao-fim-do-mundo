    using UnityEngine;
using System.Collections;

public class Ativar_Historia : MonoBehaviour {
    public GameObject vitoria;
    private bool iniciou_vitoria = false;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 VetorInicial = new Vector2(transform.position.x, transform.position.y);
        Vector2 VetorFinal = new Vector2(transform.position.x, (transform.position.y + 99999));
        Debug.DrawLine(VetorInicial, VetorFinal, Color.red);
        if (Physics2D.Linecast(transform.position, VetorFinal,
            1 << LayerMask.NameToLayer("Jogador")) && !iniciou_vitoria)
        {
            iniciou_vitoria = true;
            Game_Temp.instancia.TerminouFase = true;
            vitoria.SetActive(true);
            Game_Player.game_player.Parar_Jogador = true;
        }

    }
}
