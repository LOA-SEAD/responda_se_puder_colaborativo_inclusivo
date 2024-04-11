using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NativeWebSocket;
using TMPro;

public class avaliacao_auxiliar : MonoBehaviour
{

    public Transform ContentPlayers;
    public GameObject prefabPlayerAval;
    private int m_Itens;

    private List<GameObject> quadrosPlayerAval = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

        m_Itens = dadosTimes.meuTime.Count;

        for (int i = 0; i < m_Itens; i++)
        {
            GameObject novoPlayer = Instantiate(prefabPlayerAval, transform.position, Quaternion.identity);
            novoPlayer.transform.SetParent(ContentPlayers);
            novoPlayer.transform.localScale = new Vector3(3.07787f, 0.276828f, 0.6935131f);
            quadrosPlayerAval.Add(novoPlayer);

            // Button btn = novaEquipe.GetComponentInChildren<Button>();
            // btn.onClick.AddListener(() => btnTeamChat(teamId));
        }

        for (int i = 0; i < quadrosPlayerAval.Count; i++)
        {
            GameObject equipe = quadrosPlayerAval[i];
            TextMeshProUGUI[] textFields = equipe.GetComponentsInChildren<TextMeshProUGUI>();

            foreach (TextMeshProUGUI textField in textFields)
            {
                if (textField.CompareTag("txt_nome_player"))
                {
                    if (dadosTimes.listaTimes.Count > i)
                    {
                        textField.text = dadosTimes.meuTime[i].name;
                    }
                    break;
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
