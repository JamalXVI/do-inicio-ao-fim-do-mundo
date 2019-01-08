using UnityEngine;
using System.Collections;

public class Titulo : MonoBehaviour {
    [SerializeField]
    private GameObject transicao;
    private bool apertouCarregar = false;
	public void NewGame()
    {
            if (apertouCarregar)
            {
                return;
            }
            apertouCarregar = true;
            if (Game_Player.instancia.Carregar())
            {
                CarregarFase(1);
            }
            else
            {
                CarregarFase(7);
            }
        
    }

    private void CarregarFase(int i)
    {
        Game_Temp.instancia.TrocarFase = true;
        Game_Temp.instancia.FaseTroca = i;
        Game_Temp.instancia.SemSalvar = true;
        transicao.SetActive(true);
    }
    public void Sair()
    {
        Application.Quit();
    }
    void Awake()
    {
        Game_Player.instancia.socolinha.Ativo = true;
        Game_Player.instancia.primitivo.Ativo = true;

    }
    public void Creditos()
    {
        CarregarFase(11);
    }
    void Update()
    {
        if (Input.GetAxis("Submit") > 0 || Input.GetKey(Game_Player.instancia.Teclas["habilidade"]))
        {
            NewGame();
        }
    }
}
