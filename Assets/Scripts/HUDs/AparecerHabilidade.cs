using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AparecerHabilidade : MonoBehaviour {
    private string nomeAntigo;
    private bool transicao;
    private Transform jogador;
    private Animator anim;
    [SerializeField]
    private bool socolinha;
    void Awake()
    {
        transicao = true;
        nomeAntigo = "";
        anim = GetComponent<Animator>();
        jogador = GetComponentInParent<MovementController>().transform;
    }
	// Update is called once per frame
    void Update()
    {
        if (!Game_Player.instancia.VerificarSeEhPrincipal(jogador) || Game_Player.instancia.Em_Habilidade)
        {
            ApagarLampada();
            return;
        }
        if (Game_Player.instancia.Habilidade_Atual != null)
        {
            if (!Game_Player.instancia.Habilidade_Atual.nome.Equals(""))
            {
                if (!nomeAntigo.Equals(Game_Player.instancia.Habilidade_Atual.nome))
                {
                    nomeAntigo = Game_Player.instancia.Habilidade_Atual.nome;
                    anim.SetBool("Habilidade", true);
                }
            }
        }
        else if (!nomeAntigo.Equals(""))
        {
            ApagarLampada();
        }
        if ((Game_Temp.instancia.SinalizarSocolinha && socolinha) || (Game_Temp.instancia.SinalizarPrimitivo && !socolinha))
        {
            anim.SetBool("Troca", true);
        }
        else
        {
            anim.SetBool("Troca", false);

        }
	}
    private void ApagarLampada()
    {
        nomeAntigo = "";
        anim.SetBool("Habilidade", false);
    
    }
}
