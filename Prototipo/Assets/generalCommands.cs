using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class generalCommands : MonoBehaviour
{


    public void loadScene (string name) {
        SceneManager.LoadScene(name);
    }

    public void quit () {
        Application.Quit();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
