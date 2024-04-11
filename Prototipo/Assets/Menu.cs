using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
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
        carregaDados.LoadURL();
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
