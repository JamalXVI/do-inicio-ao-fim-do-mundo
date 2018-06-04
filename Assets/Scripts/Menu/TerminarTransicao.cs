using UnityEngine;
using System.Collections;

public class TerminarTransicao : MonoBehaviour
{
    static int idle = Animator.StringToHash("HeliceOut.Idle");
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (anim.GetCurrentAnimatorStateInfo(0).nameHash == idle)
        {
            this.gameObject.active = false;
        }
	}
}
