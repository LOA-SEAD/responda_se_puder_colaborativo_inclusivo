using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NativeWebSocket;
using TMPro;

public class instrucoes2 : MonoBehaviour
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

        string inst1 = "<align=\"center\"><b>REGRAS DO JOGO</b></align>\n\n" +
                              "O jogo tem 3 níveis: fácil, médio e difícil.\n" +
                              "O jogo possui momentos individuais e momentos em equipe.\n\n" +
                            "<b>Momento individual</b>\n" +
                              "No momento individual, cada jogador deve responder à pergunta dentro do tempo disponível.\n" +
                              "Uma Dica estará disponível para consulta, sem custo.\n\n" +
                            "<b>Momento em grupo</b>\n" +
                              "No momento em grupo, vamos ver as respostas de todos e discutir no chat para decidir a resposta correta.\n" +
                              "O líder guiará a conversa e enviará a resposta escolhida pelo grupo. O líder também pode pedir ajuda, como eliminar metade das opções ou pular para outra pergunta (apenas uma vez).";

        string inst2 =  "<align=\"center\"><b>PONTUAÇÃO</b></align>\n\n" +
                                "<u>A cada questão:</u>\n" +
                                "   10 pontos se resposta correta\n" +
                                "   até 3 pontos de colaboração\n" +
                                "<u>Ao final do jogo:</u>\n" +
                                "   bônus para cada Ajuda NÃO usada (50-50 ou Pular)\n" +
                                "A pontuação da sua equipe e o ranking das equipes serão apresentados no final. " +
                                "Aguarde até que todas as equipes terminem para ver a classificação geral.";
        
        string inst3 = "<align=\"center\"><b>LÍDER</b></align>\n\n" +
                            "A cada etapa, um jogador da equipe será escolhido aleatoriamente para ser o líder.\n" +
                            "O líder é a pessoa que irá estimular a comunicação entre os jogadores para decidir a resposta da equipe.\n\n" +
                        "<align=\"center\"><b>MODERADOR/PROFESSOR</b></align>\n\n" +
                            "Se houver dúvida, discórdia ou comportamento inadequado, você poderá apertar o botão “PROFESSOR” para chamar o professor.";


        string inst4 = "<align=\"center\"><b>AVALIAÇÃO DA COLABORAÇÃO</b></align>\n\n" +
                            "Ao final de cada fase, haverá um momento de reflexão sobre a colaboração do grupo.\n" +
                            "Cada integrante deverá avaliar os seus colegas com relação ao engajamento, comunicação e gentileza.";


        instrLista.Add(inst1);
        instrLista.Add(inst2);
        instrLista.Add(inst3);
        instrLista.Add(inst4);
        

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

        if (pagAtual == 4) btnProximo.gameObject.SetActive(false);
        else btnProximo.gameObject.SetActive(true);
    }

}
