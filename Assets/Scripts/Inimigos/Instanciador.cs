using UnityEngine;
using System.Collections;

public class Instanciador : MonoBehaviour {
    public GameObject instanciando;
    public Transform posicao;
    private bool instanciado = false;
    public float tempoVida = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 VetorInicial = new Vector2(transform.position.x, transform.position.y);
        Vector2 VetorFinal = new Vector2(transform.position.x, (transform.position.y-99999));
        Debug.DrawLine(VetorInicial, VetorFinal, Color.red);
        if (Physics2D.Linecast(transform.position, VetorFinal,
            1 << LayerMask.NameToLayer("Jogador")) && !instanciado)
        {
            instanciado = true;
            GameObject bala = (GameObject)Instantiate(instanciando, posicao.position, instanciando.transform.rotation);
            bala.GetComponent<Game_Inimigo>().criado = true;
            bala.GetComponent<Game_Inimigo>().tempo_vida = tempoVida;
            Destroy(bala,tempoVida);
            Game_Inimigos.game_inimigos.Adicionar_Inimigo(bala.GetComponent<Game_Inimigo>());
        }

	}
}
