using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NativeWebSocket;
using TMPro;

public class config : MonoBehaviour
{

    public GameObject painelConfig;

    public void AtivaDesativaConfig()
    {
        painelConfig.SetActive(!painelConfig.activeSelf);
    }

}
