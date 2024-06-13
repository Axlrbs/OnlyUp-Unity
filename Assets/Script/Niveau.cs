using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Niveau : MonoBehaviour
{
    public string NomDeScene;
    
    public void AllerAuNiveau()
    {
        SceneManager.LoadScene(NomDeScene);
    }

    private void OnTriggerEnter(Collider other)
    {
        AllerAuNiveau();
    }
}
