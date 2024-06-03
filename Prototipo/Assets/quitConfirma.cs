using NativeWebSocket;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;
using TMPro;

public class quitConfirma : MonoBehaviour
{

    public GameObject painelSaida;
    public Button yesButton;
    public Button noButton;

    bool wantsToQuit = false;

    void Start()
    {
        Application.wantsToQuit += WantsToQuit;

        yesButton.onClick.AddListener(ConfirmQuitGame);
        noButton.onClick.AddListener(CancelQuitGame);
    }

    bool WantsToQuit()
    {
        if (!wantsToQuit)
        {
            Debug.Log("Player prevented from quitting.");
            painelSaida.SetActive(true);
        }
        return wantsToQuit;
    }

    void ConfirmQuitGame()
    {
        wantsToQuit = true;
        Application.Quit();
    }

    void CancelQuitGame()
    {
        wantsToQuit = false;
        painelSaida.SetActive(false);
    }
}
