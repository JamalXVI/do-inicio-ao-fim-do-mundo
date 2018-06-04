using UnityEngine;
using System.Collections;

public class Buraco : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnTriggerEnter2D(Collider2D colisor)
	{
		if (colisor.gameObject.tag.Contains("Colider")) {
			Game_Player.game_player.Buraco = true;
		}
	}
    void OnTriggerExit2D(Collider2D colisor)
    {
        if (colisor.gameObject.tag.Contains("Colider"))
        {
            Game_Conquistas.instancia.AdicionarBuraco();
        }
    }

}
