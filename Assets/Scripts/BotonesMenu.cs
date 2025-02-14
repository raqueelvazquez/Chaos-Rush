using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesMenu : MonoBehaviour
{
    public void Comenzar(int NumeroEscena)
    {
        StartCoroutine(CargarEscena(NumeroEscena));
    }

    IEnumerator CargarEscena(int NumeroEscena)
    {
        yield return new WaitForEndOfFrame(); // Espera a que termine el frame
        SceneManager.LoadScene(NumeroEscena);
    }


    public void Salir()
    {
        Application.Quit();
        Debug.Log("Aquí se cierra el juego");
    }
}
