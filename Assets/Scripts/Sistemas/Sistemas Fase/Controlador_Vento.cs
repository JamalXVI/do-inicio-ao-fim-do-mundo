using UnityEngine;
using System.Collections;

public class Controlador_Vento : MonoBehaviour {
    private Vento vento;
    [SerializeField]
    private ParticleSystem ventoParticle;
    [SerializeField]
    private float tempoVento;
    private float contadorTempo = 0f;
    private bool ativo = true;
	// Use this for initialization
	void Start () {
        vento  = FindObjectOfType<Vento>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Game_Temp.instancia.TerminouFase)
        {
            ventoParticle.gameObject.SetActive(false);
            return;
        }
        contadorTempo += Time.deltaTime;
        if (contadorTempo >= tempoVento)
        {
            ativo = !ativo;
            contadorTempo = 0f;
            ventoParticle.gameObject.SetActive(ativo);
            vento.MovimentoVento = ativo;
            
        }
	}
}
