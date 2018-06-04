using UnityEngine;
using System.Collections;

public class VerificarPosicoes : MonoBehaviour {
    [SerializeField]
    private GameObject outroLado;
    [SerializeField]
    private GameObject[] jogadores;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject jogador in jogadores)
        {
            if (jogador.transform.position.x >= transform.position.x && outroLado.transform.position.x >= jogador.transform.position.x &&
                jogador.transform.position.y >= transform.position.y && outroLado.transform.position.y >= jogador.transform.position.y)
            {
                float math1 = Mathf.Abs(jogador.transform.position.x - transform.position.x);
                float math2 = Mathf.Abs(outroLado.transform.position.x - jogador.transform.position.x);
                float add = math2 > math1 ? 1 : -1;
                jogador.transform.position = new Vector3(jogador.transform.position.x - add, jogador.transform.position.y, jogador.transform.position.z);
            }
        }
	}
}
