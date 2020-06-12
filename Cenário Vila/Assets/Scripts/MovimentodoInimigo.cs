using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovimentodoInimigo : MonoBehaviour
{
    public float distanciaMinima;
    NavMeshAgent agenteNMA;

    void Awake()
    {
        agenteNMA = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector3 posicaoJogador = ThirdPersonWalker.pontoChao;
        agenteNMA.SetDestination(posicaoJogador);

        float distanciaEntreJogadorEInimigo = Vector3.Distance(transform.position, posicaoJogador);
        if (distanciaEntreJogadorEInimigo <= distanciaMinima)
        {
            agenteNMA.isStopped = true;
        }
        else
        {
            agenteNMA.isStopped = false;
        }
    }
}
