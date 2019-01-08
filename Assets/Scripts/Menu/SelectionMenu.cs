using UnityEngine;
using System.Collections;

public class SelectionMenu : MonoBehaviour {
    private GameObject[] itens_selecao;
    private Hashtable ativados;
    private int indice_maximo;
    void Awake()
    {
        indice_maximo = 0;
        ativados = new Hashtable();
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
    