using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public enum EstadoIA
    {
        Atacando,
        Andando,
    }

    public EstadoIA estado;
    public float dano;
    public Animator controladorAnimacao;
    


    NavMeshAgent agenteNMA;
    Vida vida;
    Vida vidaJogador;


    void Awake()
    {
        agenteNMA = GetComponent<NavMeshAgent>();
        vida = GetComponent<Vida>();
        vidaJogador = GameObject.FindWithTag("Player").GetComponent<Vida>();
    }

    void Uptade()
    {
        if (agenteNMA.isStopped)
        {
            estado = EstadoIA.Atacando;
        }
        else
        {
            estado = EstadoIA.Andando;
        }

        if(estado == EstadoIA.Atacando)
        {
            vidaJogador.vida = vidaJogador.vida - dano * Time.deltaTime;
        }

        controladorAnimacao.SetFloat("velocidade", agenteNMA.velocity.magnitude);

        if(vida.vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Atacar()
    {
        vidaJogador.vida = vidaJogador.vida - dano;
    }
}
