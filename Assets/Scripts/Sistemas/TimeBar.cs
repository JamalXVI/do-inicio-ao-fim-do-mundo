using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeBar : MonoBehaviour {
    private float tempo_atual;
    public float tempo_inicial;
    private float tempo_porcentual;
    private Image imagem;
    private Gerenciador gerenciador;
    void Awake()
    {
        imagem = gameObject.GetComponent<Image>();
        tempo_atual = tempo_inicial;
        gerenciador = GameObject.FindObjectOfType<Gerenciador>();
    }
    void Update()
    {
        if (gerenciador.jogo_rodando && tempo_atual > 0)
        {
            tempo_atual -= Time.deltaTime;
            tempo_porcentual = tempo_atual / tempo_inicial;
            imagem.fillAmount = tempo_porcentual;
        }

    }
    public float TempoRestante()
    {
        return (tempo_inicial - (tempo_inicial - tempo_atual));
    }
}
