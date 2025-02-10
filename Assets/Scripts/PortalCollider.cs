using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalCollider : MonoBehaviour
{
    public int numeroEscena;

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            SceneManager.LoadScene(numeroEscena);
        }
    }
}
