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
    
    public static void DisableAllObjectsInteractions()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            Collider collider = obj.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            Button button = obj.GetComponent<Button>();
            if (button != null)
            {
                button.interactable = false;
            }
        }
    }

    public static void EnableAllObjectsInteractions()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            Collider collider = obj.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = true;
            }

            Button button = obj.GetComponent<Button>();
            if (button != null)
            {
                button.interactable = true;
            }
        }
    }


    public static void EnableInteraction(GameObject obj)
    {
        Collider2D collider2D = obj.GetComponent<Collider2D>();
        if (collider2D != null)
        {
            collider2D.enabled = true;
        }

        Button button = obj.GetComponent<Button>();
        if (button != null)
        {
            button.interactable = true;
        }
    }



    // Start is called before the first frame update
    void Start()
    {

        carregaDados.Load();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
