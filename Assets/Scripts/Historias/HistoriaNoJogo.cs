using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class HistoriaNoJogo : MonoBehaviour
{
    public float tempo_maximo;
    public bool eh_tempo_medio;
    public string textoMostrar;
    [SerializeField]
    public AudioClip somAparecerMensagem;
    [SerializeField]
    private float tempoDestruirMensagem;
    [SerializeField]
    private GameObject pai;
    private bool destruirMensagem;
    private float contadorDestruirMensagem;
    private int posicoes_adicionais;
    private float adicionar_tempo;
    private bool bold_ativado;
    private bool italico_ativado;
    private bool size_ativado;
    private bool cor_ativado;
    private string texto_construido;
    private int numero_max_efeitos;
    private int posicao_bold;
    private int posicao_italic;
    private int posicao_size;
    private int tamanho_size;
    private int posicao_cor;
    private Boolean impedir_segundo = false;
    private float contador_impedir_segundo = 0f;
    private float tempo_impedir_segundo = 0.3f;
    private string cor;
    private float tempo_medio;
    private bool iniciou_frase;
    private string texto_atual;
    private int posicao_texto;
    private bool terminou_texto;
    private float contador_tempo;
    // Use this for initialization
    void Start()
    {
    }
    public void Iniciar(string texto)
    {
        textoMostrar = texto;
        iniciou_frase = false;
        destruirMensagem = false;
        contadorDestruirMensagem = 0;
        terminou_texto = false;
        SoundManager.instance.PlaySingle(somAparecerMensagem);
        IniciarFrase();
    }

    // Update is called once per frame
    void Update()
    {
        if (destruirMensagem)
        {
            DestruirMensagem();
            return;
        }
        if (iniciou_frase)
        {
            if (!terminou_texto)
            {

                AnimarTexto();
            }
        }
        else
        {
            IniciarFrase();
        }
    }

    private void DestruirMensagem()
    {
        contadorDestruirMensagem += Time.deltaTime;
        if (contadorDestruirMensagem >= tempoDestruirMensagem)
        {
            contadorDestruirMensagem = 0;
            pai.SetActive(false);
        }
    }
    void AnimarTexto()
    {
        contador_tempo += Time.deltaTime;
        if (contador_tempo >= tempo_medio)
        {
            if (texto_atual.Length > posicao_texto)
            {
                if (VerificarSintaxe())
                {

                    string pular = " ";
                    pular = AdicionarTextoRico();
                    string novo_texto = texto_construido + pular;
                    texto_construido = novo_texto;
                    GetComponent<Text>().text = texto_construido;
                    posicao_texto += posicoes_adicionais;
                }
                else
                {
                    texto_construido += texto_atual[posicao_texto];
                    GetComponent<Text>().text = texto_construido;
                    posicao_texto++;
                }
                string[] efeitos_textos = new string[999];
                if (bold_ativado)
                {
                    efeitos_textos[posicao_bold] = @"</b>";
                }
                if (italico_ativado)
                {
                    efeitos_textos[posicao_italic] = @"</i>";
                }
                if (size_ativado)
                {
                    efeitos_textos[posicao_size] = @"</size>";
                }
                if (cor_ativado)
                {
                    efeitos_textos[posicao_cor] = @"</color>";
                }
                for (int i = numero_max_efeitos; i >= 0; i--)
                {
                    if (efeitos_textos[i] != null)
                    {
                        GetComponent<Text>().text = GetComponent<Text>().text + efeitos_textos[i];
                    }
                }
                contador_tempo = adicionar_tempo;
                if (adicionar_tempo != 0)
                {
                    adicionar_tempo = 0;
                }
            }
            else
            {
                TerminarTexto();
            }
        }
    }
    void animar_textos()
    {

    }
    string AdicionarTextoRico()
    {
        string texto_t = "";
        texto_t += texto_atual[posicao_texto];
        texto_t += texto_atual[posicao_texto + 1];
        adicionar_tempo = 0;
        if (texto_t.Equals(@"\n"))
        {
            posicoes_adicionais = 2;
            return Environment.NewLine;
        }
        else if (texto_t.Equals(@"\e"))
        {

            string texto_temporario = texto_atual.Substring((posicao_texto + 1)).Split(']')[0].Split('[')[1];
            posicoes_adicionais = texto_temporario.Length + 4;

            adicionar_tempo = tempo_medio - float.Parse(texto_temporario);
        }
        else if (texto_t.Equals(@"\s"))
        {

            if (!size_ativado)
            {
                string texto_temporario = texto_atual.Substring((posicao_texto + 1)).Split(']')[0].Split('[')[1];
                posicoes_adicionais = texto_temporario.Length + 4;
                tamanho_size = int.Parse(texto_temporario);
                numero_max_efeitos++;
                size_ativado = true;
                posicao_size = numero_max_efeitos;
                return "<size=" + tamanho_size + ">";
            }
            else
            {
                posicoes_adicionais = 2;
                size_ativado = false;
                return "</size>";
            }

        }
        else if (texto_t.Equals(@"\c"))
        {

            if (!cor_ativado)
            {
                string texto_temporario = texto_atual.Substring((posicao_texto + 1)).Split(']')[0].Split('[')[1];
                posicoes_adicionais = texto_temporario.Length + 4;
                if (texto_temporario.Contains(@"#"))
                {
                    cor = texto_temporario;
                }
                else
                {
                    switch (texto_temporario.ToLower())
                    {
                        case "azul":
                            cor = "#0000ffff";
                            break;
                        case "preto":
                            cor = "#000000ff";
                            break;
                        case "verde":
                            cor = "#008000ff";
                            break;
                        case "azulm":
                            cor = "#000080ff";
                            break;
                        case "vermelho":
                            cor = "#ff0000ff";
                            break;
                        case "amarelo":
                            cor = "#ffff00ff";
                            break;
                        case "verdea":
                            cor = "#008080ff";
                            break;
                        case "roxo":
                            cor = "#800080ff";
                            break;
                        case "laranja":
                            cor = "#ffa500ff";
                            break;
                        case "rosa":
                            cor = "#ff00ffff";
                            break;
                        case "oliva":
                            cor = "#808000ff";
                            break;
                        case "azulc":
                            cor = "#add8e6ff";
                            break;
                        case "cinza":
                            cor = "#808080ff";
                            break;
                        case "prata":
                            cor = "#c0c0c0ff";
                            break;
                        case "marrom":
                            cor = "#800000ff";
                            break;
                        case "azule":
                            cor = "#0000a0ff";
                            break;
                        case "agua":
                            cor = "#00ffffff";
                            break;
                        default:
                            cor = "#ffffffff";
                            break;
                    }
                }
                numero_max_efeitos++;
                cor_ativado = true;
                posicao_cor = numero_max_efeitos;
                return "<color=" + cor + ">";
            }
            else
            {
                posicoes_adicionais = 2;
                cor_ativado = false;
                return "</color>";
            }

        }
        texto_t += texto_atual[posicao_texto + 2];
        if (texto_t.Equals(@"\bi"))
        {
            bold_ativado = true;
            posicoes_adicionais = 3;
            numero_max_efeitos++;
            posicao_bold = numero_max_efeitos;
            return "<b>";
        }
        else if (texto_t.Equals(@"\bf"))
        {
            posicoes_adicionais = 3;
            bold_ativado = false;
            return "</b>";
        }
        else if (texto_t.Equals(@"\ii"))
        {
            italico_ativado = true;
            posicoes_adicionais = 3;
            numero_max_efeitos++;
            posicao_italic = numero_max_efeitos;
            return "<i>";
        }
        else if (texto_t.Equals(@"\if"))
        {
            posicoes_adicionais = 3;
            italico_ativado = false;
            return "</i>";
        }
        return "";
    }
    bool VerificarSintaxe()
    {
        if ((posicao_texto + 1) >= texto_atual.Length)
        {
            return false;
        }
        string texto_t = "";
        texto_t += texto_atual[posicao_texto];
        texto_t += texto_atual[posicao_texto + 1];
        if (texto_t.Equals(@"\n"))
        {
            return true;
        }
        else if (texto_t.Equals(@"\e"))
        {
            return true;
        }
        else if (texto_t.Equals(@"\s"))
        {
            return true;
        }
        else if (texto_t.Equals(@"\c"))
        {
            return true;
        }
        if ((posicao_texto + 2) >= texto_atual.Length)
        {
            return false;
        }
        texto_t += texto_atual[posicao_texto + 2];
        if ((posicao_texto + 2) >= texto_atual.Length)
        {
            return false;
        }
        if (texto_t.Equals(@"\bi"))
        {
            return true;
        }
        else if (texto_t.Equals(@"\bf"))
        {
            return true;
        }
        else if (texto_t.Equals(@"\ii"))
        {
            return true;
        }
        else if (texto_t.Equals(@"\if"))
        {
            return true;
        }
        return false;
    }
    void TerminarTexto()
    {
        iniciou_frase = false;
        terminou_texto = false;
        destruirMensagem = true;
    }
    void IniciarFrase()
    {

            GetComponent<Text>().text = "";
            texto_construido = "";
            numero_max_efeitos = 0;
            GetComponent<Text>().supportRichText = true;
            posicao_texto = 0;
            texto_atual = textoMostrar;
            if (eh_tempo_medio)
            {
                tempo_medio = tempo_maximo / texto_atual.Length;
            }
            else
            {
                tempo_medio = tempo_maximo;
            }
            contador_tempo = 0;
            iniciou_frase = !iniciou_frase;
    }
    private void SubstituirTudo()
    {
        /*
                            cor = "#ffffffff";
                            break;
                    }
         */
        texto_atual = texto_atual.Replace(@"\c[", "<color=[");
        texto_atual = texto_atual.Replace(@"\c", "</color>");
        texto_atual = texto_atual.Replace(@"[agua]", "#00ffffff>");
        texto_atual = texto_atual.Replace(@"[azule]", "#00ffffff>");
        texto_atual = texto_atual.Replace(@"[marrom]", "#800000ff>");
        texto_atual = texto_atual.Replace(@"[prata]", "#c0c0c0ff>");
        texto_atual = texto_atual.Replace(@"[cinza]", "#808080ff>");
        texto_atual = texto_atual.Replace(@"[azulc]", "#add8e6ff>");
        texto_atual = texto_atual.Replace(@"[oliva]", "#808000ff>");
        texto_atual = texto_atual.Replace(@"[rosa]", "#ff00ffff>");
        texto_atual = texto_atual.Replace(@"[laranja]", "#ffa500ff>");
        texto_atual = texto_atual.Replace(@"[roxo]", "#800080ff>");
        texto_atual = texto_atual.Replace(@"[verdea]", "#008080ff>");
        texto_atual = texto_atual.Replace(@"[amarelo]", "#ffff00ff>");
        texto_atual = texto_atual.Replace(@"[vermelho]", "#ff0000ff>");
        texto_atual = texto_atual.Replace(@"[verde]", "#008000ff>");
        texto_atual = texto_atual.Replace(@"[azulm]", "#000080ff>");
        texto_atual = texto_atual.Replace(@"[azul]", "#0000ffff>");
        texto_atual = texto_atual.Replace(@"[preto]", "#000000ff>");
        texto_atual = texto_atual.Replace(@"\ii", "<i>");
        texto_atual = texto_atual.Replace(@"\if", "</i>");
        texto_atual = texto_atual.Replace(@"\bi", "<i>");
        texto_atual = texto_atual.Replace(@"\bf", "</i>");
        texto_atual = texto_atual.Replace(@"\s[", "<size=");
        texto_atual = texto_atual.Replace(@"\s", "</size>");
        texto_atual = texto_atual.Replace(@"\n", Environment.NewLine);
        Boolean achou = false;
        ArrayList strings = new ArrayList();
        int i_inicial = 0;
        string ti = "";
        for (int i = 0; i < texto_atual.Length; i++)
        {
            if (i < texto_atual.Length - 1)
            {
                ti = "";
                ti += texto_atual[i];
                ti += texto_atual[i + 1];
                if (ti.Equals(@"\e"))
                {
                    achou = true;
                    i_inicial = i;
                }
                ti = "";
                ti += texto_atual[i];

                if (achou && ti.Equals(@"]"))
                {
                    strings.Add(texto_atual.Substring(i_inicial, (i - i_inicial + 1)));
                    achou = false;
                }
            }

        }
        foreach (string s in strings)
        {
            texto_atual = texto_atual.Replace(s, "");
        }

        texto_atual = texto_atual.Replace(@"]", ">");

    }
    
}
