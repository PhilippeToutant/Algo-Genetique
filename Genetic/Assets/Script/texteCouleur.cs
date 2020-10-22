using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Créateur : Philippe Toutant (1335553)
/// Nom de la classe : texteCouleur
/// Description :  Permet de lier le slider de couleur à un texte
/// </summary>
public class texteCouleur : MonoBehaviour
{
    Text nombre;
    // Start is called before the first frame update
    void Start()
    {
        nombre = GetComponent<Text>();
    }

    // Update is called once per frame
    public void texteUpdate(float value)
    {
        nombre.text = Mathf.RoundToInt(value) + " Couleurs";
    }
}
