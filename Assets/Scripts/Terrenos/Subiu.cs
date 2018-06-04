using UnityEngine;
using System.Collections;

public class Subiu : GroundCommun
{
    public PularESubir pulou;
    void OnTriggerExit2D(Collider2D colisor)
    {
        //colisor.gameObject.transform.GetComponent<SpriteRenderer>().name == "Foot_L"
        if (colisor.gameObject.tag == "Chao")
        {
            //pulou.permitirpassagem();
            colisor.GetComponentInParent<MovementController>().SetAllCollidersStatus(true);
        }
    }
    void Update()
    {
    }
}
