using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vento : MonoBehaviour {
    [SerializeField]
    private float forcaExterna;
    private bool movimentoVento;

    public bool MovimentoVento
    {
        get { return movimentoVento; }
        set { movimentoVento = value; }
    }
    
	// Use this for initialization
	void Start () {
        movimentoVento = true;
	}
	
	// Update is called once per frame
    void Update()
    {
        MovementController[] movimentos = FindObjectsOfType<MovementController>();
        foreach (var movimento in movimentos)
        {
            if (Game_Temp.instancia.TerminouFase)
            {
                movimento.ForcaExterna = 0f;
            }else if (movimentoVento)
            {
                movimento.ForcaExterna = forcaExterna;
            }
            else
            {
                movimento.ForcaExterna = 0f;
            }
        }
	}
}
