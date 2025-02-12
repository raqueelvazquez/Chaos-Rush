using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesMenu : MonoBehaviour
{
    public void Comenzar(int NumeroEscena)
    {
        SceneManager.LoadScene(NumeroEscena);
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Aquí se cierra el juego");
    }
}
