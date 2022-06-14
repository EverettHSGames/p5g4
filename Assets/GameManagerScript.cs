using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public TextMeshPro textmesh;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject e = GameObject.FindGameObjectWithTag("Enemy");
        if (e == null)
        {
            textmesh.gameObject.SetActive(true);
            textmesh.text = "You have beat the game";
            Time.timeScale = 0;
        }
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p == null)
        {
            textmesh.gameObject.SetActive(true);
            textmesh.text = "You kinda suck not gonna lie";
            Time.timeScale = 0;
        }
    }
}
