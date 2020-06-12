using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemadaCamera : MonoBehaviour
{
    public float velocidadeRotacao;
    
    Transform trJogador;
    Transform tr;

    void Awake()
    {
        tr = GetComponent<Transform>();

        GameObject jogadorGbj = GameObject.FindWithTag("Player");
        trJogador = jogadorGbj.GetComponent<Transform>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        //copia posição do jogador no tripé
        tr.position = trJogador.position;

        //rotaciona de acordo com o movimento do mouse
        float movX = Input.GetAxis("Mouse X");
        tr.Rotate(0, movX * velocidadeRotacao * Time.deltaTime, 0);
    }

    public void AlinharRotacao()
    {
        tr.rotation = trJogador.rotation;
    }

}
