using UnityEngine;
using System.Collections;

public class PedraCorpo : MonoBehaviour {

    public float velocidade = 2f;
    private Gerenciador gerenciador;
    public Transform pai;
    public Pedra bullet;
    public Vector2 forca;
    public float gravidade_decair;
    public bool Vivo
    {
        get { return bullet.vivo; }
        set { bullet.vivo = value; }
    }
    // Use this for initialization
    void Awake()
    {
        gerenciador = GameObject.FindObjectOfType<Gerenciador>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bullet.vivo)
        {
            pai.transform.Translate(-Vector2.right * velocidade * Time.deltaTime, Space.World);
        }

    }
    void OnTriggerEnter2D(Collider2D colisor)
    {

        if (colisor.gameObject.tag == "ColiderP" && bullet.vivo)
        {
            MovementController vivojog = colisor.gameObject.GetComponentInParent<MovementController>();
            if (vivojog.vivo && !vivojog.invencivel)
            {
                gerenciador.MatarPersonagem();
            }

        };
    }
    void OnCollisionEnter2D(Collision2D colisor)
    {

        if (colisor.gameObject.tag == "ColiderP" && bullet.vivo)
        {
            MovementController vivojog = colisor.gameObject.GetComponentInParent<MovementController>();
            if (vivojog.vivo && !vivojog.invencivel)
            {
                gerenciador.MatarPersonagem();
            }

        };
    }
    public void Destruir()
    {
        bullet.vivo = false;
        pai.GetComponent<Rigidbody2D>().gravityScale = gravidade_decair;
        pai.GetComponentInParent<SpriteRenderer>().sortingLayerName = "Foreground";
        pai.GetComponent<Rigidbody2D>().isKinematic = false;
        pai.GetComponent<Rigidbody2D>().AddForce(forca);

    }
}
