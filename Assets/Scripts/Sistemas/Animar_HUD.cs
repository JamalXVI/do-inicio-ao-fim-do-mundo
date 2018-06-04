using UnityEngine;
using System.Collections;

public class Animar_HUD : MonoBehaviour {
	// Use this for initialization
    private Animator anim;
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Game_Player.game_player.Jogador == null)
        {
            return;
        }
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"+Game_Player.game_player.Jogador.name))
        {
            anim.SetBool(Game_Player.game_player.Jogador.name, true);
        }
        else
        {
            anim.SetBool("Socolinha", false);
            anim.SetBool("Primitivo", false);
        }
	}
}
