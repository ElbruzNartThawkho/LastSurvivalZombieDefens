using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject Taret_Bluepprint;
    public void TaretSpawn()
    {
        Instantiate(Taret_Bluepprint);
    }
    public void Load(string name)
    {
        SceneManager.LoadScene(name);
    } 
}
