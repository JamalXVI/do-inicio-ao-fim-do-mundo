using UnityEngine;
using System.Collections;

public class PularESubir : MonoBehaviour
{

    // Use this for initialization
    private bool jogadorpassou;
    public bool passando;
    void Start()
    {
        jogadorpassou = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D colisor)
    {

        if (colisor.gameObject == Game_Player.game_player.Jogador)
        {
            passando = true;
            jogadorpassou = true;
            //impedirpassagem();
            colisor.GetComponentInParent<MovementController>().SetAllCollidersStatus(false);
        }
    }
    void OnTriggerExit2D(Collider2D colisor)
    {
        if (colisor.gameObject == Game_Player.game_player.Jogador)
        {
            passando = false;
        }

    }
    public void permitirpassagem()
    {
        jogadorpassou = false;
        impedirpassagem();
    }
    void impedirpassagem()
    {
        foreach (Transform child in transform)
        {
            foreach (Collider2D c in child.GetComponents<Collider2D>())
            {
                if (child.tag == "GrassyGrowndTrigger")
                {
                    c.isTrigger = jogadorpassou;
                }
            }
        }
    }

}
