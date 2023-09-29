using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NativeWebSocket;
using TMPro;

public class config : MonoBehaviour
{

    public TMP_Text instrucoesText;
    public TMP_Text paginaText;
    public List<string> instrucoes = new List<string>();
    private int indiceInstrucaoAtual = 0;
    private int pagAtual = 1;
    private int instrucoesPorPagina = 1;

    private void Start()
    {
        
        DefinirInstrucoes();


        AtualizarInstrucao();
    }

    public void DefinirInstrucoes()
    {
        string inst1 = "INSTRUÇÔES PAGINA 1";
        string inst2 = "INSTRUÇÔES PAGINA 2";   
        string inst3 = "INSTRUÇÔES PAGINA 3";
        string inst4 = "INSTRUÇÔES PAGINA 4";

        instrucoes.Add(inst1);
        instrucoes.Add(inst2);
        instrucoes.Add(inst3);
        instrucoes.Add(inst4);
        

    }

    public void ProximaInstrucao()
    {
        if (indiceInstrucaoAtual < instrucoes.Count - 1)
        {
            indiceInstrucaoAtual++;
            if (indiceInstrucaoAtual % instrucoesPorPagina == 0)
            {
                pagAtual++;
            }
            AtualizarInstrucao();
        }
    }

    public void InstrucaoAnterior()
    {
        if (indiceInstrucaoAtual > 0)
        {
            indiceInstrucaoAtual--;
            if (indiceInstrucaoAtual % instrucoesPorPagina == instrucoesPorPagina - 1 || indiceInstrucaoAtual == 0)
            {
                pagAtual--;
            }
            AtualizarInstrucao();
        }
    }

    private void AtualizarInstrucao()
    {
        instrucoesText.text = instrucoes[indiceInstrucaoAtual];
        paginaText.text = pagAtual + "/" + instrucoes.Count;
    }

}
