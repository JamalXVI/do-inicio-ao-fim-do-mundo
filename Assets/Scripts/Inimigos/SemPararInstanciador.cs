using UnityEngine;
using System.Collections;

public class SemPararInstanciador : MonoBehaviour {
    public GameObject instanciando;
    public Transform posicao;
    public Transform parar;
    private bool terminou_instanciar = true;
    public float tempoVida = 10.0f;
    private float contador;
    public float intervalo;
    public bool ligar = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 VetorInicial = new Vector2(transform.position.x, transform.position.y);
        Vector2 VetorFinal = new Vector2(transform.position.x, (transform.position.y - 99999));
        Debug.DrawLine(VetorInicial, VetorFinal, Color.red);
        if (Physics2D.Linecast(transform.position, VetorFinal,
            1 << LayerMask.NameToLayer("Jogador")) && terminou_instanciar)
        {
            terminou_instanciar = false;
            contador = intervalo;
        }
        if (!terminou_instanciar)
        {
            /*
            if (Game_Player.game_player.Jogador.position.x < transform.position.x &&
                !Physics2D.Linecast(transform.position, VetorFinal,
                1 << LayerMask.NameToLayer("Jogador")))
            {
                terminou_instanciar = true;
            }
            */
            contador += Time.deltaTime;
            if (contador >= intervalo)
            {
                contador = 0;
                GameObject bala = (GameObject)Instantiate(instanciando, posicao.position, instanciando.transform.rotation);
                bala.GetComponent<Game_Inimigo>().criado = true;
                bala.GetComponent<Game_Inimigo>().tempo_vida = tempoVida;
                Destroy(bala, tempoVida);
                Game_Inimigos.game_inimigos.Adicionar_Inimigo(bala.GetComponent<Game_Inimigo>());
            }
            
        }
        Vector2 VetorInicial2 = new Vector2(parar.position.x, parar.position.y);
        Vector2 VetorFinal2 = new Vector2(parar.position.x, (parar.position.y - 99999));
        Debug.DrawLine(VetorInicial2, VetorFinal2, Color.red);
        if (Physics2D.Linecast(parar.position, VetorFinal2,
            1 << LayerMask.NameToLayer("Jogador")))
        {
            terminou_instanciar = true;
        }

    }
}
