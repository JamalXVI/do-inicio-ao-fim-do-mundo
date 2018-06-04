using UnityEngine;
using System.Collections;

public class GroundCommun : MonoBehaviour {
    public bool checkpoint = false;
    public BoxCollider2D caixa;
    void Awake()
    {
        caixa = GetComponent<BoxCollider2D>();
    }
    void OnCollisionStay2D(Collision2D colisor)
    {
        if (colisor.gameObject.tag.StartsWith("PlayerP") && checkpoint)
        {
            if ((transform.position.y + caixa.bounds.size.y / 2) >= colisor.transform.position.y && (transform.position.y - caixa.bounds.size.y / 2) <= colisor.transform.position.y &&
                (transform.position.x + caixa.bounds.size.x / 2) >= colisor.transform.position.x && (transform.position.x - caixa.bounds.size.x / 2) <= colisor.transform.position.x)
            {
                print("ma oe");
            }
            
        }
    }
}
