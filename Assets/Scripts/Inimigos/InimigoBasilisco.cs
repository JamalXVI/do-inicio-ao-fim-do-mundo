using UnityEngine;
using System.Collections;

public class InimigoBasilisco : Game_Inimigo {
    private static int attack = Animator.StringToHash("Basilisco.Attack");
    private static int idle = Animator.StringToHash("Basilisco.Idle");
    private static int stuned = Animator.StringToHash("Basilisco.Stuned");
    private Animator anim;
    private Gerenciador gerenciador;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        gerenciador = FindObjectOfType<Gerenciador>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D colisor)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).nameHash != stuned)
        {
            foreach (Transform col in Game_Player.instancia.Jogador)
            {
                if (colisor.gameObject == col.gameObject && vivo)
                {
                    anim.SetTrigger("Attack");
                    VerificarMatarJogador();
                }
            }
        }

    }
    private void VerificarMatarJogador()
    {
        MovementController movimentoAtual = Game_Player.instancia.Movimento_Atual;
        if (movimentoAtual.vivo && !movimentoAtual.invencivel)
        {
            gerenciador.MatarPersonagem();
            Game_Player.instancia.MatarJogador();
        }
    }
}
