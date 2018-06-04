using UnityEngine;
using System.Collections;
using System;

public class TextoPopUp : MonoBehaviour {
    public float destruirem;
    private Vector3 vetor;
    public float velocidade;
    public String pretexto_Item;
    Color sub = new Color32(0, 0, 0, 3);
    public void posicionar(Transform pos)
    {
        vetor = new Vector3(pos.position.x, pos.position.y, transform.position.z);
        transform.position = Camera.main.WorldToViewportPoint(vetor);
        mudar_filho();
        
    }
    public void mudar_filho()
    {
        foreach (Transform filho in transform)
        {
            if (filho.tag == "TextoFilho")
            {
                filho.transform.position = vetor;
                filho.GetComponent<SpriteRenderer>().color -= sub;
            }
        }
    }
    void Start()
    {
        Destroy(gameObject, destruirem);
    }
    void Update()
    {
        vetor.y += velocidade;
        transform.position = Camera.main.WorldToViewportPoint(vetor);
        mudar_filho();
        
        GetComponent<GUIText>().color -= sub;
    }
    public void Texto(String texto)
    {
        GetComponent<GUIText>().text = texto;
    }
    public void Texto_Item(String texto)
    {
        GetComponent<GUIText>().text = texto + pretexto_Item;
    }

}
