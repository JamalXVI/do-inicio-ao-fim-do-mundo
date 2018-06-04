using UnityEngine;
using System.Collections;

public class HabilidadeHUD : MonoBehaviour
{
    string nome_antigo;
    static int idle_transicao = Animator.StringToHash("HUDHabilidade.IdleEntre");
    static int idle_circle = Animator.StringToHash("HUDHabilidade.IdleCircle");
    bool transicao;
    Animator anim;
    int etapa;
    void Start()
    {
        anim = GetComponent<Animator>();
        transicao = true;
        etapa = 0;
        nome_antigo = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Game_Player.game_player.Habilidade_Atual != null)
        {
            if (!Game_Player.game_player.Habilidade_Atual.nome.Equals(""))
            {
                if (!nome_antigo.Equals(Game_Player.game_player.Habilidade_Atual.nome))
                {
                    nome_antigo = Game_Player.game_player.Habilidade_Atual.nome;
                    transicao = true;
                }
            }
        }else  if (!nome_antigo.Equals(""))
        {
            nome_antigo = "";
            transicao = true;
        }
        
        if (transicao)
        {
            switch (etapa)
            {
                case 0:
                    Iniciar_Transicao();
                    break;
                case 1:
                    Verificar_Metade();
                    break;
                case 2:
                    Finalizar_Animacao();
                    break;
                default:
                    break;
            }
        } 
    }
    void Finalizar_Animacao()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).nameHash == idle_circle)
        {
            anim.SetBool("FimTransicao", false);
            etapa = 0;
            transicao = false;
        }
    }   
    void Verificar_Metade()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).nameHash == idle_transicao)
        {
            anim.SetBool("FimTransicao",true);
            Ativar_Habilidade();
            etapa++;
        }
    }
    void Iniciar_Transicao()
    {
        anim.SetTrigger("IniciarTransicao");
        etapa++;
    }
    void Ativar_Habilidade()
    {
        
        foreach (Transform circulo in transform)
        {

            foreach (Transform habilidade in circulo)
            {
                if (nome_antigo.Equals(habilidade.name))
                {
                    habilidade.gameObject.SetActive(true);
                }
                else
                {
                    habilidade.gameObject.SetActive(false);
                }
            }
        }
    }
}
