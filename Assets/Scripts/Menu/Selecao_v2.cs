using UnityEngine;
using System.Collections;

public class Selecao_v2 : MonoBehaviour {
    private GameObject[] itens_selecao;
    private Hashtable ativados;
    private Hashtable quizes_ativados;
    private Hashtable trancados;
    private int indice_maximo;
	private int indice_maximo_quizz;
    void Awake()
    {
        indice_maximo = 0;
		indice_maximo_quizz = 0;
        ativados = new Hashtable();
        quizes_ativados = new Hashtable();
        trancados = new Hashtable();
        itens_selecao = GameObject.FindGameObjectsWithTag("Selection");
        loop_selecao();

    }
    public void loop_selecao()
    {
        foreach (GameObject item in itens_selecao)
        {
            if (!item.name.Contains("_"))
            {
                continue;
            }
            if (item.name.Contains("R"))
            {
                verificar_quizz(item);
            }
            else
            {
                verificar_fase(item);
            }
        }

    }
    private void verificar_quizz(GameObject item)
    {
        /*
        if (0 >= 
            Game_Player.game_player.Moedas_Quizz)
        {
            return;
        }
        */
        bool loop = false;
        string nome = item.name.Split('R')[0];
        string cod = item.name.Split('_')[1];

		for (int i = 0; i < Game_Player.instancia.ordem_fases.Length; i++) {
			if (nome == null || cod == null || Game_Player.instancia.ordem_fases [i] == null) {
				continue;
			}
            /*
			if (Game_Player.game_player.verificar_quizz(Game_Player.game_player.ordem_fases[i]) &&
			    i + 1 == int.Parse(nome) && cod.Contains("S") && !quizes_ativados.ContainsKey(item))
			{
				indice_maximo_quizz = i + 1;
				quizes_ativados.Add(item, true);
				loop = true;
				break;
			}

			if (Game_Player.game_player.verificar_quizz(Game_Player.game_player.ordem_fases[i]) &&
			    i + 1 == int.Parse(nome) && cod.Contains("L") && !trancados.ContainsKey(item))
			{
				trancados.Add(item, true);
				loop = true;
				break;
			}
            */

		}
		if (loop)
		{
			loop_selecao();
			return;
		}
		if (!quizes_ativados.ContainsKey(item) && cod.Contains("S"))
		{
			item.SetActive(false);
		}
		if (quizes_ativados.ContainsKey(item) && cod.Contains("S"))
		{
			item.SetActive(true);
		}
		if (trancados.ContainsKey(item) && cod.Contains("L"))
		{
			item.gameObject.SetActive(false);
		}
    }
    private void verificar_fase(GameObject item)
    {
        bool loop = false;
        string nome = item.name.Split('_')[0];
        string cod = item.name.Split('_')[1];
        for (int i = 0; i < Game_Player.instancia.ordem_fases.Length; i++)
        {
            if (nome == null || cod == null || Game_Player.instancia.ordem_fases[i] == null)
            {
                continue;
            }
			if (Game_Player.instancia.VerificarSePassouFase(i) &&
                i + 1 == int.Parse(nome) && cod.Contains("S") && !ativados.ContainsKey(item))
            {
                indice_maximo = i + 1;
                ativados.Add(item, true);
                loop = true;
                break;
            }
            else if (cod.Contains("S"))
            {
                if (int.Parse(nome) == indice_maximo + 1 && !ativados.ContainsKey(item))
                {
                    ativados.Add(item, true);
                    loop = true;
                    break;

                }
            }
			if (Game_Player.instancia.VerificarSePassouFase(i) &&
                i + 1 == int.Parse(nome) && cod.Contains("L") && !trancados.ContainsKey(item))
            {
                trancados.Add(item, true);
                loop = true;
                break;
            }
            else if (cod.Contains("L"))
            {
                if (int.Parse(nome) == indice_maximo + 1 && !trancados.ContainsKey(item))
                {
                    trancados.Add(item, true);
                    loop = true;
                    break;

                }
            }
        }
        if (loop)
        {
            loop_selecao();
            return;
        }
        if (!ativados.ContainsKey(item) && cod.Contains("S"))
        {
            item.SetActive(false);
        }
        if (ativados.ContainsKey(item) && cod.Contains("S"))
        {
            item.SetActive(true);
        }
        if (trancados.ContainsKey(item) && cod.Contains("L"))
        {
            item.gameObject.SetActive(false);
        }
    }
    public void Clicar_01()
    {
        Application.LoadLevel(Game_Player.instancia.ordem_fases[0]);
    }
    public void Clicar_02()
    {
        Game_Player.instancia.PodeReiniciar = true;
        Game_Player.instancia.Parar_Jogador = false;
        Application.LoadLevel(Game_Player.instancia.ordem_fases[1]);
    }
}
