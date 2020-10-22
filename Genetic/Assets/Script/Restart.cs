using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Créateur : Philippe Toutant (1335553)
/// Nom de la classe : Restart
/// Description :  Permet recommencer la scène pour enlever les blocs et liens créés
/// </summary>
public class Restart : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }
}
