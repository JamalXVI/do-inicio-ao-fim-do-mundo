using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CenaConquista : MonoBehaviour {
    private List<GameObject> conquistas;
    public int maximoConquista;
    public Text nome;
    public Text descricao;
    public GameObject transicao;
	// Use this for initialization
	void Start () {
        EncontrarObjetoConquista();
        PreencherObjetoConquista();
        nome.text = "";
        descricao.text = "";
	}

    private void EncontrarObjetoConquista()
    {
        conquistas = new List<GameObject>();
        GameObject lista = transform.Find("BasePanel").gameObject.transform.Find("Lista").gameObject;
        for (int i = 0; i < maximoConquista; i++)
        {
            conquistas.Add(lista.transform.Find(" (" + i + ")").gameObject);
        }
    }

    private void PreencherObjetoConquista()
    {
        foreach (var conquista in conquistas)
        {
            int idConquista = int.Parse(conquista.name.Split('(')[1].Split(')')[0]);
            IniciarObjeto(conquista, idConquista);
            CriarBotaoNoObjeto(conquista, idConquista);
            
        }
    }

    private static void IniciarObjeto(GameObject conquista, int idConquista)
    {
        Text texto = conquista.GetComponentInChildren<Text>();
        Conquista c = Game_Conquistas.instancia.conquistas[idConquista];
        texto.text = c.Nome;
        GameObject circulo = conquista.transform.Find("Circle").gameObject;
        GameObject check = circulo.transform.Find("Check").gameObject;
        GameObject cadeado = circulo.transform.Find("Lock").gameObject;
        bool visivel = false;
        if (c.Completada)
        {
            visivel = true;
        }
        check.SetActive(visivel);
        cadeado.SetActive(!visivel);
    }

    private void CriarBotaoNoObjeto(GameObject conquista, int idConquista)
    {
        Button btn = conquista.AddComponent<Button>() as Button;
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(delegate { ExibirConquista(idConquista); });
    }
	public void ExibirConquista(int idConquista)
    {
        nome.text = Game_Conquistas.instancia.conquistas[idConquista].Nome;
        descricao.text = Game_Conquistas.instancia.conquistas[idConquista].Descricao;
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Submit") > 0 || Input.GetKey(Game_Player.instancia.Teclas["habilidade"]))
        {
            Ok();
        }
	}
    public void Ok()
    {
        MudarDeCena(Game_Player.instancia.troca_fases);
    }
    private void MudarDeCena(int nivel)
    {
        Game_Temp.instancia.TrocarFase = true;
        Game_Temp.instancia.FaseTroca = nivel;
        transicao.SetActive(true);
        //Application.LoadLevel(Game_Player.game_player.carregando);
    }
}
