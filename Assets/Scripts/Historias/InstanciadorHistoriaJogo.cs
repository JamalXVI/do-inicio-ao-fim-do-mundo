using UnityEngine;
using System.Collections;

public class InstanciadorHistoriaJogo : MonoBehaviour {
    [SerializeField]
    private HistoriaNoJogo historia;
    [SerializeField]
    private GameObject paiHistoria;
    [SerializeField]
    private string textoAMostrar;
    [SerializeField]
    private int id;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Jogador_Passou())
        {
            paiHistoria.SetActive(true);
            historia.Iniciar(textoAMostrar);
            Game_Player.instancia.FalasNoJogo[id] = true;
            Destroy(this);
        }
    }
    private bool Jogador_Passou()
    {
        Vector2 VetorInicial = new Vector2(transform.position.x, transform.position.y);
        Vector2 VetorFinal = new Vector2(transform.position.x, (transform.position.y + 999999));
        Debug.DrawLine(VetorInicial, VetorFinal, Color.yellow);
        if (Physics2D.Linecast(transform.position, VetorFinal,
           1 << LayerMask.NameToLayer("Jogador")))
        {
            return true;
        }
        return false;
    }
}
