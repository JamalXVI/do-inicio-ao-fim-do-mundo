using UnityEngine;
using System.Collections;

public class FrutaHUD : MonoBehaviour {
    private Animator animDescer;
    [SerializeField]
    private Animator abacaxi;
    [SerializeField]
    private Animator banana;
    [SerializeField]
    private Animator caju;
    [SerializeField]
    private Animator maca;
    [SerializeField]
    private Animator manga;
	// Use this for initialization
	void Start () {
        animDescer = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void AnimarFruta(int fruta)
    {
        animDescer.SetTrigger("Call");
        switch (fruta)
        {
            case 0:
                abacaxi.SetTrigger("Fruta");
                break;
            case 1:
                banana.SetTrigger("Fruta");
                break;
            case 2:
                caju.SetTrigger("Fruta");
                break;
            case 3:
                maca.SetTrigger("Fruta");   
                break;
            case 4:
                manga.SetTrigger("Fruta");
                break;
            default:
                break;
        }
    }
}
