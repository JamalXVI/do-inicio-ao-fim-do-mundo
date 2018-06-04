using UnityEngine;
using System.Collections;

public class Grito : MonoBehaviour {
    private Gerenciador gerenciador;
    [SerializeField]
    private Vector2 distancia = new Vector2(12, 99999);
    [SerializeField]
    private bool derrubaArvore;
    [SerializeField]
    private Arvore_Cair arvore;
    [SerializeField]
    private bool matarInimigos;
    [SerializeField]
    private Game_Inimigo inimigos;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private bool ativaAnimator;

	// Use this for initialization
	void Start () {
        gerenciador = FindObjectOfType<Gerenciador>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerStay2D(Collider2D col)
    {
        MovementController mov = col.gameObject.GetComponentInParent<MovementController>();
        if (mov != null)
        {
            if (mov.gameObject.Equals(Game_Player.game_player.Jogador.gameObject) && mov.gameObject.name.Contains("Primitivo"))
            {
                AtivarHabilidade(mov.transform);
            }
        }
        

    }
    private void AtivarHabilidade(Transform col)
    {
        Game_Player.game_player.AtivarHabilidades("Grito");
        float total = distancia.x + this.GetComponent<BoxCollider2D>().size.x;
        gerenciador.IniciarHabilidade(Game_Player.game_player.Jogador, this.transform, new Vector2(total,
                    distancia.y), true);
    }

    public void FinalizarGrito()
    {
        if (derrubaArvore)
        {
            arvore.DerrubarArvore();
        }
        if (matarInimigos)
        {

                inimigos.gameObject.SetActive(false);
        }
        if (ativaAnimator)
        {
            animator.SetTrigger("Som");
        }
    }
}
