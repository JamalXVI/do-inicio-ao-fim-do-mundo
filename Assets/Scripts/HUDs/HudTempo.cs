using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudTempo : MonoBehaviour {
    private float tempoAtual;
    [SerializeField]
    private float tempoInicial;
    private Gerenciador gerenciador;
    private Text texto;
	// Use this for initialization
    void Start()
    {
        tempoAtual = tempoInicial;
        gerenciador = GameObject.FindObjectOfType<Gerenciador>();
        texto = GetComponent<Text>();
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Game_Temp.instancia.TerminouFase)
        {
            return;
        }
        if (gerenciador.jogo_rodando && tempoAtual > 0)
        {
            tempoAtual -= Time.deltaTime;
            string segundos = Mathf.Round(tempoAtual) % 60 < 10 ? "0"+(Mathf.Round(tempoAtual) % 60).ToString() :
                (Mathf.Round(tempoAtual) % 60).ToString();
            string minutos = Mathf.Round(tempoAtual) / 60 <= 0 ? "0" :  Mathf.Floor(Mathf.Round(tempoAtual) / 60).ToString();
            texto.text = minutos + ":" + segundos;
        }
        else
        {
            Game_Player.instancia.Vidas_Extras = -1000;
            Game_Conquistas.instancia.AdicionarPerdaTempo();
            gerenciador.ReiniciarJogo();
        }
	}
    public float TempoRestante()
    {
        return (tempoInicial - (tempoInicial - tempoAtual));
    }
}
    