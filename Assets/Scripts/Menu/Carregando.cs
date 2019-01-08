using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Carregando : MonoBehaviour {
    public bool enviar_email;
    private bool enviouInfo;
    private bool enviandoInfo = true;
	// Use this for initialization
    void Start()
    {
        enviouInfo = false;
        /*
          
        Thread t1 = new Thread(salvar)
        {
        };
        t1.Start();
        while (!t1.IsAlive)
        {
            print("oi");
        };
          */
        StartCoroutine("salvar");
        
        
	}
	private void salvar()
    {
        /*
        if (Game_Player.game_player.EnviarEmail)
        {
            Game_Player.game_player.EnviarEmail = false;
            Email.email.Enviar();
        }
         */
        Game_Temp.instancia.ZerarValoresTemporarios();
        
        if (!Game_Temp.instancia.SemSalvar)
        {
            Game_Player.instancia.SalvarMoedas();
            Game_Player.instancia.Salvar(ConstantesDoSistema.Caminho);
            Game_Player.instancia.ZerarVariaveis();
            Game_Player.instancia.Parar_Jogador = false;
        }
        else
        {
            Game_Temp.instancia.SemSalvar = false;
        }
        if (Game_Player.instancia.EnviarInfoNivel)
        {
            AdicionarInfo(Game_Player.instancia.RetornarNivel(Game_Player.instancia.UltimoNivel), Game_Player.instancia.UltimoNivel);
            return;
        }
        else {
            enviandoInfo = false;
        }
        if (Game_Temp.instancia.TrocarFase)
        {
            enviouInfo = true;
            return;
        }

    }
	// Update is called once per frame
	void Update () {
        if (enviouInfo && !enviandoInfo)
        {
            enviouInfo = false; 

            StartCoroutine("CarregarNivel");
            Game_Temp.instancia.TrocarFase = false;
        }
	}
    IEnumerator CarregarNivel()
    {
        int fase = Game_Temp.instancia.TrocarFase ? Game_Temp.instancia.FaseTroca : Game_Player.instancia.troca_fases;
        AsyncOperation async = Application.LoadLevelAsync(fase);
        yield return async;

    }
    public void AdicionarInfo(Nivel nivel, int level)
    {
        if (!Game_Player.instancia.FornecerInformacoes)
        {
            enviouInfo = true;
            enviandoInfo = false;
            return;
        }
        try
        {
            WWWForm form = new WWWForm();
            form.AddField("chaveAuth", "65#$%*412tLOK");
            form.AddField("mac", ConstantesDoSistema.RetornarMac());
            form.AddField("nivel", level);
            form.AddField("vidas", nivel.Numero_Vidas);
            form.AddField("moedas", nivel.Numero_Moedas);
            form.AddField("itens", nivel.Numero_Itens);
            form.AddField("inimigos", nivel.Numero_Inimigos);
            form.AddField("tempo", Convert.ToString(nivel.TempoRestante));
            form.AddField("pontuacao", nivel.Pontuacao);
            WWW request = new WWW(ConstantesDoSistema.Endereco + "Enviar.php", form);
            StartCoroutine(EsperarResposta(request));
        }
        catch (Exception)
        {
            enviouInfo = true;
            enviandoInfo = false;
        }
    }
    IEnumerator EsperarResposta(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            enviouInfo = true;
            Game_Player.instancia.EnviarInfoNivel = false;
            Debug.Log("WWW Ok!: " + www.text);
        }
        else
        {
            enviouInfo = true;
            Game_Player.instancia.EnviarInfoNivel = false;
            Debug.Log("WWW Error: " + www.error);

        }
        enviandoInfo = false;
    }   
}
