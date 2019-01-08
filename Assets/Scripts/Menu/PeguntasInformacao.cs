using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;

public class PeguntasInformacao : MonoBehaviour {
    [SerializeField]
    private GameObject canvasAviso;
    [SerializeField]
    private GameObject canvasPerguntas;
    [SerializeField]
    private GameObject transicao;
    [SerializeField]
    private Toggle feminino;
    [SerializeField]
    private Toggle masculino;
    [SerializeField]
    private Toggle outros;
    [SerializeField]
    private InputField idade;
    [SerializeField]
    private Dropdown etnia;
    private bool jaFezIntro;
	// Use this for initialization
	void Start () {
        jaFezIntro = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Aceitar()
    {
        if (!jaFezIntro)
        {
            jaFezIntro = true;
            AcionarSegundoCanvas();
        }
    }

    private void AcionarSegundoCanvas()
    {
        Game_Player.game_player.FornecerInformacoes = true;
        Thread t = new Thread(SalvarJogador);
        t.Start();
        canvasAviso.SetActive(false);
        canvasPerguntas.SetActive(true);
    }
    public void ColetarDados()
    {
        if (!Game_Player.game_player.FornecerInformacoes)
        {
            return;
        }
        WWWForm form = new WWWForm();
        form.AddField("chaveAuth", "65#$%*412tLOK");
        string sexo = "";
        if (feminino.isOn)
        {
            sexo = "feminino";
        }
        else if (masculino.isOn)
        {
            sexo = "masculino";
        }
        else if (outros.isOn)
        {
            sexo = "outros";
        }
        form.AddField("mac", ConstantesDoSistema.RetornarMac());
        form.AddField("etinia", etnia.options[etnia.value].text);
        form.AddField("sexo", sexo);
        form.AddField("idade", idade.text);
        WWW request = new WWW(ConstantesDoSistema.Endereco + "index.php", form);
        StartCoroutine(EsperarResposta(request));
        CarregarFase(Game_Player.game_player.troca_fases);
    }
    public void Negar()
    {
        Game_Player.game_player.FornecerInformacoes = false;
        Game_Player.game_player.Salvar(ConstantesDoSistema.Caminho);
        canvasAviso.SetActive(false);
        CarregarFase(Game_Player.game_player.troca_fases);
    }
    private void CarregarFase(int i)
    {
        Game_Temp.instancia.TrocarFase = true;
        Game_Temp.instancia.FaseTroca = i;
        Game_Temp.instancia.SemSalvar = true;
        transicao.SetActive(true);
    }

    IEnumerator EsperarResposta(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Game_Player.game_player.EnviarInfoNivel = false;
            Debug.Log("WWW Ok!: " + www.text);
        }
        else
        {
            Game_Player.game_player.EnviarInfoNivel = false;
            Debug.Log("WWW Error: " + www.error);

        }
    }
    #region Threads
    private void SalvarJogador()
    {
        Game_Player.game_player.Salvar(ConstantesDoSistema.Caminho);
        
    }
    #endregion
}
