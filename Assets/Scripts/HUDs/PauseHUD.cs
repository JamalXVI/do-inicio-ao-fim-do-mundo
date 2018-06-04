using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class PauseHUD : MonoBehaviour {
    private Animator pai;
    private Animator filho;
    private string estado;
    private GameObject botao;
    private string estado_antigo;
    private string acao;
    private Gerenciador gerenciador;
    private bool sair;
    bool iniciou_fim;
    public Animator transicao_anim;
    public GameObject transicao;
    public float tempo_fim = 2f;
    private float contador_fim;
    static int black = Animator.StringToHash("Transicao.Black");
    static int sair_idle = Animator.StringToHash("PausePai.final");
    public GameObject pause_pai;
	// Use this for initialization
	void Start () {
        pai = GetComponent<Animator>();
        filho = GameObject.FindGameObjectWithTag("PauseFilho").GetComponent<Animator>();
        gerenciador = GameObject.FindObjectOfType<Gerenciador>();
        sair = false;
        iniciou_fim = false;
        contador_fim = 0;
	}
    private void atualizar_sair()
    {
        if (pai.GetCurrentAnimatorStateInfo(0).nameHash == sair_idle)
        {
            switch (acao)
            {
                case "retornar":
                    sair = false;
                    pai.SetBool("sair", false);
                    acao = "";
                    gerenciador.Pausar_Jogo = false;
                    break;
                case "sair":
                    Application.Quit();
                    break;
                case "selecao":
                    if (!iniciou_fim)
                    {
                         iniciou_fim = true;
                         transicao.SetActive(true);
                         Time.timeScale = 1;
                         pause_pai.SetActive(false);
                         

                    }
                    else
                    {

                    }
                    break;
                default:
                    break;
            }
        }
    }
	// Update is called once per frame
	void Update () {
        if (sair)
        {
            atualizar_sair();
            return;
        }
        if (verificarPosicao())
        {
            estado = botao.name.ToLower().
                Split(new string[] { "btn" }, StringSplitOptions.RemoveEmptyEntries)[0];
            if (estado.Equals(estado_antigo))
            {
                return;
            }
            estado_antigo = estado;
            switch (estado)
            {
                case  "retornar":
                    finalizar_outras_bools();
                    filho.SetBool("Retornar", true);
                    break;
                case "galeria":
                    finalizar_outras_bools();
                    filho.SetBool("Galeria", true);
                    break;
                case "fases":
                    finalizar_outras_bools();
                    filho.SetBool("Fases", true);
                    break;
                case "opcoes":
                    finalizar_outras_bools();
                    filho.SetBool("Opcoes", true);
                    break;
                case "sair":
                    finalizar_outras_bools();
                    filho.SetBool("Nada", true);
                    break;
                default:
                    finalizar_outras_bools();
                    break;
            }
        }else{
            finalizar_outras_bools();

        }
	}
    void finalizar_outras_bools()
    {
        filho.SetBool("Retornar", false);
        filho.SetBool("Fases", false);
        filho.SetBool("Nada", false);
        filho.SetBool("Galeria", false);
        filho.SetBool("Opcoes", false);
    }
    bool verificarPosicao()
    {
        PointerEventData pe = new PointerEventData(EventSystem.current);
        pe.position = Input.mousePosition;

        List<RaycastResult> hits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pe, hits);
        estado = "";
        foreach (RaycastResult hit in hits)
        {
            if (hit.gameObject.tag.Equals("PauseBotao"))
            {
                botao = hit.gameObject;
                return true;
            }
        }
        return false;
    }
    private void preparar_sair()
    {
        pai.SetBool("Sair", true);
        sair = true;
    }
    public void retornar()
    {
        preparar_sair();
        acao = "retornar";
    }
    public void mSair()
    {
        preparar_sair();
        acao = "sair";
    }
    public void selecao()
    {
        preparar_sair();
        acao = "selecao";
    }
}
