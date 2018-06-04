using UnityEngine;
using System.Collections;

public class Matar : MonoBehaviour {

    Gerenciador gerenciador;
    
    // Use this for initialization
    void Awake()
    {
        gerenciador = GameObject.FindObjectOfType<Gerenciador>();

    }
	// Update is called once per frame
	void Update () {
        verificar_se_passou_jogador();
	}
    public void verificar_se_passou_jogador()
    {
        Vector2 VetorInicial = new Vector2(transform.position.x, transform.position.y);
        Vector2 VetorFinal = new Vector2(transform.position.x + 999999999, (transform.position.y));
        Debug.DrawLine(VetorInicial, VetorFinal, Color.red);
        if (Physics2D.Linecast(transform.position, VetorFinal,
            1 << LayerMask.NameToLayer("Jogador")))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 999999,
             1 << LayerMask.NameToLayer("Jogador"));
            gerenciador.fimJogo(hit.transform);
        }
        else if (Physics2D.Linecast(transform.position, VetorFinal,
           1 << LayerMask.NameToLayer("Inimigo")))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 999999,
             1 << LayerMask.NameToLayer("Inimigo"));
            if (hit.transform!= null)
            {
              hit.transform.GetComponent<Game_Inimigo>().Destruido = true;
            }

        }
        else if (Physics2D.Linecast(transform.position, VetorFinal,
           1 << LayerMask.NameToLayer("Ground")))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 999999,
             1 << LayerMask.NameToLayer("Ground"));
            Destroy(hit.transform.gameObject);
        }

    }
}
