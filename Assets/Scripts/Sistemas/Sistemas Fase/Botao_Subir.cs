using UnityEngine;
using System.Collections;

public class Botao_Subir : MonoBehaviour {
    public bool apertado;
	// Use this for initialization
	void Start () {
        apertado = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        
        float distancia = transform.position.y + GetComponent<Collider2D>().bounds.size.y;
        if (col.gameObject.Equals(Game_Player.game_player.Jogador.gameObject)
            )
        {
            float tamanho = 0;
            foreach (Transform filho in col.transform)
            {
                if (filho.name.Contains("Colliders"))
                {
                    tamanho = filho.transform.GetComponent<CircleCollider2D>().bounds.size.y / 2;
                }
            }
            if (col.transform.position.y - tamanho >= distancia)
            {
                apertado = true;   
	        }
        }
    }

}
