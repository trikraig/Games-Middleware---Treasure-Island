using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dontDestroy : MonoBehaviour {

    public GameObject player;
    GameObject spawnPoint;
    cameraController cc;


	// Use this for initialization
	void Start () {

        DontDestroyOnLoad(this.gameObject);
        cc = GetComponentInChildren<cameraController>();
        

    }

    private void Update()
    {
        if (spawnPoint == null)
        {
            spawnPoint = GameObject.FindWithTag("Respawn");
        }

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

    }

    public void MainLoadNextLevel()
    {
        SceneManager.LoadScene("island");
        cc.findTarget();
        
       
    }
	
}
