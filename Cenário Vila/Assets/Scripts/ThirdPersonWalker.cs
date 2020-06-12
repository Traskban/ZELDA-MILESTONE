using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonWalker : MonoBehaviour
{
    public float velocidadeMov;
    public float deslocamentoAltura;
    public float intensidadePulo;
    public LayerMask camadaChao;
    public Animator anima;

    Transform tr;
    Rigidbody rb;
    Transform trCam;

    public bool estaNoChao;
    public bool estaEmPulo;
    public bool estaEmMovimento;

    public static Vector3 pontoChao;
        void Awake()
        {
            tr = GetComponent<Transform>();
            rb = GetComponent<Rigidbody>();
            trCam = GameObject.FindWithTag("Tripe").GetComponent<Transform>();
        }
    void FixedUpdate()
    {
        //receber dados de entrada do jogador
        bool apertouPulo = Input.GetButtonDown("Jump");
        bool apertouAtaque = Input.GetButtonDown("Fire1");
        float movH = Input.GetAxis("Horizontal");
        float movV = Input.GetAxis("Vertical");

        Vector3 mov = new Vector3(movH, 0, movV);
        if (mov.magnitude > 1f)
            mov.Normalize();

        //detecta dados
        RaycastHit chaohit;
        estaNoChao = Physics.Raycast(tr.position, Vector3.down, out chaohit, deslocamentoAltura + 0.05f, camadaChao);
        estaEmPulo = apertouPulo || !estaEmPulo;
        estaEmMovimento = mov.magnitude > 0.1f;

        //ataque
        if (apertouAtaque && !estaEmPulo)
        {
            anima.SetTrigger("atacou");
        }

        //pulo
        rb.useGravity = estaEmPulo;
        rb.constraints = (RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ);
        if (!estaEmPulo)
            rb.constraints = rb.constraints | RigidbodyConstraints.FreezePositionY;

        if (apertouPulo && estaNoChao)
        {
            rb.AddForce(Vector3.up * intensidadePulo, ForceMode.Impulse);
        }

        //rotacionar o jogador na direlção do movimento
        if (estaEmMovimento)
        tr.LookAt(tr.position + trCam.TransformDirection(mov) * 5);

        //andança do jogador
        if(estaEmMovimento)
        tr.Translate(0, 0, mov.magnitude * velocidadeMov * Time.deltaTime);

        //alimentando parametro anima
        anima.SetFloat("velocidade", mov.magnitude);

        //Acompanhar Chão
        if (!estaEmPulo) { 
            RaycastHit hit;
            bool rcBateunoChao = Physics.Raycast(tr.position, Vector3.down, out hit, Mathf.Infinity, camadaChao);

            if (rcBateunoChao)
            {
                Vector3 pos = tr.position;
                pos.y = hit.point.y + deslocamentoAltura;
                tr.position = pos;

                pontoChao = hit.point;
            }

         //Zerar Inercia 
            rb.velocity = Vector3.zero;
        }
    }
}
