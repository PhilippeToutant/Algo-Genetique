using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Créateur : Philippe Toutant (1335553)
/// Nom de la classe : teste
/// Description :  Ceci est le coeur de tous l'algorithme génétique
/// </summary>
public class teste : MonoBehaviour
{
    public Slider SliderBloc;
    public Slider SliderCouleur;
    public Material[] materials;
    public GameObject blocPrefab;
    private int index = 1;
    [SerializeField]
    private GameObject SpawnBox;
    public List<Vector3> objPositions = new List<Vector3>();
    private float sizeOfObj = 1f;
    private LineRenderer lineRenderer;

    /*
     Nom :SetBoxBoundaries
     Description : Permet de créée des limites au bloc pour ne pas qu'ils soivent créée dans un autre bloc
     Types entré : Null
     Types sorti : Null
    */
    private Vector3 SetBoxBoundaries()
    {
        Vector3 LocalPosition;
        BoxCollider Boundaries = SpawnBox.GetComponent<BoxCollider>();
        float random = Random.value;
        LocalPosition.x = Mathf.Lerp(Boundaries.center.x + SpawnBox.transform.position.x - Boundaries.extents.x * SpawnBox.transform.localScale.x,
    Boundaries.center.x + SpawnBox.transform.position.x + Boundaries.extents.x * SpawnBox.transform.localScale.x, random);
        random = Random.value;
        LocalPosition.y = Mathf.Lerp(Boundaries.center.y + SpawnBox.transform.position.y - Boundaries.extents.y * SpawnBox.transform.localScale.y,
    Boundaries.center.y + SpawnBox.transform.position.y + Boundaries.extents.y * SpawnBox.transform.localScale.y, random);
        random = Random.value;
        LocalPosition.z = Mathf.Lerp(Boundaries.center.z + SpawnBox.transform.position.z - Boundaries.extents.z * SpawnBox.transform.localScale.z,
    Boundaries.center.z + SpawnBox.transform.position.z + Boundaries.extents.z * SpawnBox.transform.localScale.z, random);
        if (isAvailable(LocalPosition))
        {
            objPositions.Add(LocalPosition);
            return LocalPosition;
        }
        else
            return SetBoxBoundaries();
    }
   
    private bool isAvailable(Vector3 pos)
    {
        for (int i = 0; i < objPositions.Count; i++)
        {
            if (Vector3.Distance(pos, objPositions[i]) < sizeOfObj)
                return false;
        }
        return true;
    }
    /*
 Nom :buttonpresse
 Description : Éxécute le code lorsque le boutton est enclenché
 Types entré : Null
 Types sorti : Null
*/
    public void buttonpresse()
    {
        int valueBloc = System.Convert.ToInt32(SliderBloc.value);
        int valueCouleur = System.Convert.ToInt32(SliderCouleur.value);
        int[,] tableau = new int[valueBloc, valueBloc];
        GameObject[] tableauBloc = new GameObject[valueBloc];
        int[] tableauCounterLien = new int[valueBloc];

        /*INSTANTIATION DE VALEUR DANS UNE MATRICE*/
        for (int i = 0; i < tableau.GetLength(0); i++)
        {
            for (int j = 0; j < tableau.GetLength(1); j++)
            {
                int pourcendelien = UnityEngine.Random.Range(1, 100);
                if (i == j)
                {
                    tableau[i, j] = 0;
                }
                else if (pourcendelien > 60)
                {
                    if (tableau[i, j] == 0)
                    {
                        tableau[i, j] = 1;
                        tableau[j, i] = 1;
                        tableauCounterLien[i]++;
                        tableauCounterLien[j]++;
                    }
                }
                else
                {
                    tableau[i, j] = 0;
                }
            }
        }
        /*Création des bloc*/
        for (int i = 0; i < valueBloc; i++)
        {
            index += 1;
            if (index == valueCouleur + 1)
            {
                index = 1;
                print(index);
            }
            tableauBloc[i] = Instantiate(blocPrefab, SetBoxBoundaries(), Quaternion.identity);
            tableauBloc[i].GetComponent<Renderer>().material = materials[index - 1];
        }

        /*Vérification minimal de lien par blocs*/
        for (int i = 0; i < tableau.GetLength(0); i++)
        {
            for (int j = 0; j < tableau.GetLength(1); j++)
            {
                if (tableauCounterLien[i] == 0)
                {
                    if (tableauBloc[i].GetComponent<Renderer>().sharedMaterial != tableauBloc[j].GetComponent<Renderer>().sharedMaterial)
                    {
                        tableau[i, j] = 1;
                        tableauCounterLien[i]++;
                        tableauCounterLien[j]++;
                    }
                }

                else if (tableauBloc[i].GetComponent<Renderer>().sharedMaterial == tableauBloc[j].GetComponent<Renderer>().sharedMaterial)
                {
                   tableau[i, j] = 0;
                    tableauCounterLien[i]--;
                    tableauCounterLien[j]--;
                }
            }
        }
   
        /*Création des liens*/
        for (int i = 0; i < tableau.GetLength(0); i++)
        {
            for (int j = 0; j < tableau.GetLength(1); j++)
            {
                if (tableau[i,j]==1)
                {
                    /*Instancié le lien entre 2 blocs*/
                    lineRenderer = new GameObject().AddComponent<LineRenderer>();
                    lineRenderer.startColor = Color.black;
                    lineRenderer.endColor = Color.black;
                    lineRenderer.startWidth = 0.1f;
                    lineRenderer.endWidth = 0.1f;
                    lineRenderer.positionCount = 2;
                    lineRenderer.useWorldSpace = true;

                    lineRenderer.SetPosition(0, new Vector3(tableauBloc[i].transform.position.x, tableauBloc[i].transform.position.y, tableauBloc[i].transform.position.z)); //x,y and z position of the starting point of the line
                    lineRenderer.SetPosition(1, new Vector3(tableauBloc[j].transform.position.x, tableauBloc[j].transform.position.y, tableauBloc[j].transform.position.z)); //x,y and z position of the starting point of the line
                }
            }
        }
    }
}
/*
int[,] tableau = new int[10, 10] {
{0, 1, 1, 0, 0, 0, 0, 0, 0, 0},   
{1, 0, 1, 1, 1, 0, 0, 0, 0, 0},   
{1, 1, 0, 0, 1, 0, 0, 0, 0, 0},
{0, 1, 0, 0, 1, 1, 0, 0, 0, 0},
{0, 1, 1, 1, 0, 1, 0, 0, 0, 0},
{0, 0, 0, 1, 1, 0, 1, 0, 0, 0},
{0, 0, 0, 0, 0, 1, 0, 1, 1, 0},
{0, 0, 0, 0, 0, 0, 1, 0, 1, 1},
{0, 0, 0, 0, 0, 0, 1, 1, 0, 1},
{0, 0, 0, 0, 0, 0, 0, 1, 1, 0}
};*/
