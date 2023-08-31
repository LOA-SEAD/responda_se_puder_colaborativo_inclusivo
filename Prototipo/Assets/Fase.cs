using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Fase : MonoBehaviour
{
    public string cena;
    public float sec;
    public TMP_Text txt_nivel;
    public TMP_Text txt_lider; 

    private void Start()
    {
        SetLevelText();
        SetLeaderText();
        Invoke("NextQ", sec);
    }

    void NextQ()
    {
        SceneManager.LoadScene(cena);
    }



    public void SetLevelText()
    {
        txt_nivel.text = Manager.FASE;
    }

    public void SetLeaderText()
    {
        string t = dadosTimes.GetUser(Manager.leaderId);

        txt_lider.text = "Líder da fase: " + t;
    }



}