using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NativeWebSocket;
using TMPro;

public class instrucoes_professor : MonoBehaviour
{
    //DEFINE E MANIPULA AS INSTRUCOES DO JOGO

    public GameObject painelInstrucoes;
    public TMP_Text instrucoesText;
    public TMP_Text paginaText;
    public List<string> instrLista = new List<string>();
    private int indiceInstrucaoAtual = 0;
    private int pagAtual = 1;
    private int instrucoesPorPagina = 1;
    public Button btnProximo;
    public Button btnAnterior;

    private void Start()
    {
        
        DefinirInstrucoes();
        AtualizarInstrucao();
    }

    public void DefinirInstrucoes()
    {
        // string inst1 = "INSTRUÇÔES PAGINA 1";
        // string inst2 = "INSTRUÇÔES PAGINA 2";   
        // string inst3 = "INSTRUÇÔES PAGINA 3";
        // string inst4 = "INSTRUÇÔES PAGINA 4";

        string inst1 = "<align=\"center\"><b>ENTRADA</b></align>\n\n" +
                            "Na tela de configurações, insira o número de equipes desejado, a quantidade máxima de alunos por equipe, o tempo máximo permitido por questão e o seu nome.\n" +
                            "Clique em “Criar”, as equipes serão geradas com um código exclusivo. Compartilhe esses códigos com os alunos para que possam formar suas equipes.\n" +
                            "Aguarde até que todos os alunos tenham ingressado em suas equipes.\n" +
                            "Clique em “Iniciar”.";

        string inst2 = "<align=\"center\"><b>DURANTE O JOGO</b></align>\n\n" +
                              "Se uma equipe tiver dúvidas ou enfrentar conflitos, ela pode chamar o moderador por meio do chat para obter assistência. Um sinal de “exclamação” será exibido sobre o nome da equipe para indicar essa necessidade de auxílio.\n" +
                              "O moderador tem a capacidade de visualizar o chat da equipe e pode oferecer assistência em caso de dúvidas ou ajudar a conciliar conflitos.\n";

        string inst3 =  "<align=\"center\"><b>ENCERRAMENTO</b></align>\n\n";


        instrLista.Add(inst1);
        instrLista.Add(inst2);
        instrLista.Add(inst3);
    }

    public void AtivaDesativaInstrucoes()
    {
        painelInstrucoes.SetActive(!painelInstrucoes.activeSelf);
    }

    public void ProximaInstrucao()
    {
        if (indiceInstrucaoAtual < instrLista.Count - 1)
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
        instrucoesText.text = instrLista[indiceInstrucaoAtual];
        paginaText.text = pagAtual + "/" + instrLista.Count;
    }


    private void Update()
    {
        if (pagAtual == 1) btnAnterior.gameObject.SetActive(false);
        else btnAnterior.gameObject.SetActive(true);

        if (pagAtual == 3) btnProximo.gameObject.SetActive(false);
        else btnProximo.gameObject.SetActive(true);
    }
}
