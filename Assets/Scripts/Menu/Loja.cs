    using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System;
using System.Collections.Generic;

public class Loja : MonoBehaviour {
    [SerializeField]
    private Animator loja;
    private GameObject[] paineis;
    private ItemLoja[] artes;
    private ItemLoja[] musicas;
    private ItemLoja[] historias;
    public GameObject painelGaleria;
    public GameObject painelMusica;
    private bool acionouGaleria = false;
    private int indiceGaleria = 0;
    public Sprite[] imagensLoja;
    public AudioClip[] musicasClip;
    public int[] ordem_musicas;
    private bool acionouMusica;
    private int idMusica;
    public int[] idDoItem;
    private List<Sprite> artesSelecionadas;
    public string estadoAtual;
    public GameObject canvasTransicao;
    [SerializeField]
    private int[] niveisHistorias;
    [SerializeField]
    private int[] fasesHistorias;
    [SerializeField]
    private AudioClip somComprar;
	// Use this for initialization
	void Start () {
        IniciarVariaveis();
        LimparPainelLoja(); 
	}

    private void IniciarVariaveis()
    {
        artes = RetornarListarPorTipo(ConstantesDoSistema.Arte);
        musicas = RetornarListarPorTipo(ConstantesDoSistema.Musica);
        historias = RetornarListarPorTipo(ConstantesDoSistema.Historia);
        paineis = GameObject.FindGameObjectsWithTag("PainelBaseLoja");
        estadoAtual = "H";
        AtivarTela(estadoAtual);
    }
    private ItemLoja[] RetornarListarPorTipo(int tipo)
    {
        var lista = from l in Game_Loja.instancia.itemsDaLoja where l.Tipo == tipo select l;
        return lista.ToArray();

    }
	private void LimparPainelLoja()
    {
        foreach (var painel in paineis)
        {
            painel.transform.Find("Nome").GetComponent<Text>().text = "";
            painel.transform.Find("Preço").GetComponent<Text>().text = "";
            painel.transform.Find("Engr").gameObject.SetActive(false);
            painel.transform.Find("Circle").gameObject.SetActive(false);
            painel.SetActive(false);
        }

    }

    private void ColocarArte()
    {
        
        for (int i = 0; i < artes.Length; i++)
        {
            PreencherPainel(i, artes[i]);
        }
    }
    private void ColocarMusica()
    {
        for (int i = 0; i < musicas.Length; i++)
        {
            PreencherPainel(i, musicas[i]);
        }
    }
    private void ColocarHistoria()
    {
        for (int i = 0; i < historias.Length; i++)
        {
            PreencherPainel(i, historias[i]);
        }
    }
    private void PreencherPainel(int i, ItemLoja item)
    {
        var painels = from p in paineis where p.name.Equals((i+1).ToString()) select p;
        var painel = painels.First();
        PreencherInfoPainel(item, painel);
        PreencherLiberadoEComprado(item, painel);
        Button btn = painel.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        FazerEventoClick(item, btn, i);
        painel.SetActive(true);
    }

    private static void PreencherLiberadoEComprado(ItemLoja item, GameObject painel)
    {
        var circulo = painel.transform.Find("Circle");
        circulo.gameObject.SetActive(true);
        circulo.transform.Find("Lock").gameObject.SetActive(!item.Liberado);
        circulo.transform.Find("Check").gameObject.SetActive(item.Comprado);
    }

    private static void PreencherInfoPainel(ItemLoja item, GameObject painel)
    {
        painel.transform.Find("Nome").GetComponent<Text>().text = item.Nome.ToUpper();
        painel.transform.Find("Preço").GetComponent<Text>().text = Convert.ToString(item.Preco);
        painel.transform.Find("Engr").gameObject.SetActive(true);
    }

    private void FazerEventoClick(ItemLoja item, Button btn, int i)
    {
        if (item.Liberado)
        {
            if (!item.Comprado)
            {
                btn.onClick.AddListener(delegate { ComprarItem(item, i); });
            }
            else
            {
                btn.onClick.AddListener(delegate { UsarItem(item); });

            }
        }
    }
    public void UsarItem(ItemLoja item)
    {
        switch (estadoAtual)
        {
            case "A":
                AcionarGaleriaArte(item);
                break;
            case "M":
                AcionarMusica(item);
                break;
            case "H":
                AcionarHistoria(item);
                break;
            default:
                break;
        }

    }
    private void AcionarHistoria(ItemLoja item)
    {
        for (int i = 0; i < niveisHistorias.Length; i++)
        {
            if (niveisHistorias[i] == item.Id)
            {
                CarregarFase(fasesHistorias[i]);
            }
        }
    }
    private void AcionarMusica(ItemLoja item)
    {
        acionouMusica = true;
        idMusica = item.Id;
        Text descricao = painelMusica.transform.Find("Panel").Find("Text").GetComponent<Text>();
        descricao.text = item.Descricao.ToUpper();
    }
    public void TocarMusica()
    {
        SoundManager.instance.PlayMusic(musicasClip[Array.IndexOf(ordem_musicas, idMusica)]);
    }
    public void PararMusica()
    {
        SoundManager.instance.PararMusica();
    }
    private void AcionarGaleriaArte(ItemLoja item)
    {
        artesSelecionadas = new List<Sprite>();

        for (int i = 0; i < idDoItem.Length; i++)
        {
            if (idDoItem[i] == item.Id)
            {
                artesSelecionadas.Add(imagensLoja[i]);
            }
        }
        Text descricao = painelGaleria.transform.Find("Panel").Find("Text").GetComponent<Text>();
        descricao.text = item.Descricao.ToUpper();
        acionouGaleria = true;
        indiceGaleria = 0;
        painelGaleria.SetActive(true);
        AtualizarImagemGaleriaArte();
        Button[] botoes = painelGaleria.transform.GetComponentsInChildren<Button>();

        foreach (var botao in botoes)
        {
            botao.onClick.RemoveAllListeners();
            if (botao.name.Contains("Direita"))
            {
                botao.onClick.AddListener(delegate { PassarFoto(1); });
            }
            else
            {

                botao.onClick.AddListener(delegate { PassarFoto(-1); });
            }
        }
    }
    public void PassarFoto(int i)
    {
        if (indiceGaleria + i < 0)
        {
            indiceGaleria = artesSelecionadas.Count - 1;
        }
        else 
        {
            indiceGaleria = ((indiceGaleria + i) % artesSelecionadas.Count);
        }
        AtualizarImagemGaleriaArte();
    }
    public void AtualizarImagemGaleriaArte()
    {
        GameObject imagem = painelGaleria.transform.Find("Image").gameObject;
        Image image = imagem.GetComponent<Image>();
        image.sprite = artesSelecionadas[indiceGaleria];
    }
    public void ComprarItem(ItemLoja item, int i)
    {
        if (Game_Player.instancia.Moedas_Jogo >= item.Preco)
        {
            Game_Player.instancia.MoedasGastas += item.Preco;
            item.Comprado = true;
            SoundManager.instance.PlaySingle(somComprar);
            PreencherPainel(i, item);
        }

    }
	// Update is called once per frame
	void Update () {
        if (acionouGaleria)
        {
            painelGaleria.SetActive(true);

        }
        else
        {
            painelGaleria.SetActive(false);
        }
        if (acionouMusica)
        {
            painelMusica.SetActive(true);
        }
        else
        {
            painelMusica.SetActive(false);
        }
	}
    public void AtivarTela(string s)
    {
        RemoverAcoesAnteriores();
        loja.SetTrigger(s);
        estadoAtual = s;
        switch (s)
        {
            case "A":
                ColocarArte();
                break;
            case "M":
                ColocarMusica();
                break; 
            case "H":
                ColocarHistoria();
                break;
            default:
                break;
        }
    }

    private void RemoverAcoesAnteriores()
    {
        loja.SetBool("H", false);
        loja.SetBool("A", false);
        loja.SetBool("M", false);
        loja.SetBool("E", false);
        LimparPainelLoja();
    }
    public void Iniciar()
    {
        if (acionouGaleria)
        {
            acionouGaleria = false;
        }
        else if (acionouMusica)
        {
            acionouMusica = false;
        }
        else
        {
            CarregarFase(Game_Player.instancia.troca_fases);
        }
    }
    private void CarregarFase(int i)
    {
        Game_Temp.instancia.TrocarFase = true;
        Game_Temp.instancia.FaseTroca = i;
        Game_Temp.instancia.SemSalvar = false;
        canvasTransicao.SetActive(true);
    }
}
