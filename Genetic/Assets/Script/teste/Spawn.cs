using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    public Slider bloc;
    public Slider couleur;
    public GameObject prefabBloc;
    int k;
    int l;

    GameObject[,] BlocArray = new GameObject[6 , 6];

    public void Spawner()
    {
        for (int i = 0; i < bloc.value * 2; i++)
        {
            k = Random.Range(0, 6);
            l = Random.Range(0, 6);
            BlocArray[k, l] = (GameObject.Instantiate(prefabBloc, new Vector3(k * 3, l * 3, 0), transform.rotation));
            Debug.Log(k + " " + l);
        }
    }
}
