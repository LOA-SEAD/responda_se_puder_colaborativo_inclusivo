using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Narrativa : MonoBehaviour
{
    public AudioClip[] falas;
    public AudioSource fala;

    public AudioClip[] efeitos;
    public AudioSource efeito;

    private String[] roteiro;
    public Text texto;

    private string roteiro_aux;

    public Image fundo;
    public Sprite[] fundos;

    public Image UI;

    public Sprite[] UIs;

    public Image UI_TV;
    public Sprite[] UIs_TV;

    private String[] nomes;
    public Text nome;

    private int carrega;
    public int num_texto;

    public float velocidade;

    public int pular;
    public bool fim;

    public float transitionTime = 0.5f;

    public AudioSource pularaudio;

    // Start is called before the first frame update
    void Start()
    {
        CarregarTexto();
        StartCoroutine(TransitionSprite(UIs,UI));
        if(UIs_TV[num_texto] != null)
            StartCoroutine(TransitionSprite(UIs_TV,UI_TV));
        carrega = 0;
        fim = false;
        roteiro_aux = roteiro[num_texto++];
        texto.text = "";

    }

    
    private IEnumerator TransitionSprite(Sprite[] UIs, Image UI)
    {

        Sprite originalSprite = UI.sprite;
        float elapsedTime = 0f;
        Color originalColor = UI.color;
        Color newColor = originalColor;
        newColor.a = 0f;

        // Transição do Sprite original para o novo Sprite
        while (elapsedTime < transitionTime)
        {
            // Atualiza o tempo decorrido
            elapsedTime += Time.deltaTime;

            // Calcula a fração de tempo decorrido
            float t = Mathf.Clamp01(elapsedTime / transitionTime);

            // Interpola as cores para fazer o fade in/out
            UI.color = Color.Lerp(originalColor, newColor, t);
            
            // Espera o próximo frame
            yield return null;
        }

        // Troca o Sprite e reinicia a transição para fade in

        if(UIs[num_texto] != null)
            UI.sprite = UIs[num_texto];
        elapsedTime = 0f;

        while (elapsedTime < transitionTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / transitionTime);
            UI.color = Color.Lerp(newColor, originalColor, t);
            yield return null;
        }

        // Garante que a transparência final seja restaurada
        UI.color = originalColor;
    }

    public void Pular(){
        Informacoes.SetStatus(0);
		Informacoes.SetNivel(0);
		SceneManager.LoadScene("Jogo");
    }

    public void Prosseguir(){

        if(num_texto == 12){
            Pular();
        }

        if(fim == true){
            roteiro_aux = roteiro[num_texto];
            StartCoroutine(TransitionSprite(UIs,UI));
            if(UIs_TV[num_texto] != null && num_texto < 3)
                StartCoroutine(TransitionSprite(UIs_TV,UI_TV));
            num_texto++;
            texto.text = "";
            carrega = 0;
            fim = false;
        }else{
            roteiro_aux = roteiro[num_texto-1];
            texto.text = roteiro_aux;
            carrega = roteiro_aux.Length;
            fim = true;
            velocidade = 0;
        }
    }

    void CarregarTexto(){
        roteiro = new String[12];
        texto.text = "";
        num_texto = 0;
        roteiro[0] = " Onde..... estou.....? Tudo está tão escuro e enferrujado.";
        roteiro[1] = " Uau.... o que aconteceu? Sinto-me... vivo novamente....";
        roteiro[2] = " O que foi isso?! Sinto-me diferente.... Como se estivesse conectado novamente.";
        roteiro[3] = " E agora, nosso robô moderno Smartio, responda esta pergunta: - Quem foi o humano que formulou as nossas 3 leis ?";
        roteiro[4] = " 1. um robô não pode ferir um humano ou permitir que um humano sofra algum mal.";
        roteiro[5] = " 2. os robôs devem obedecer às ordens dos humanos, exceto nos casos em que essas ordens entrem em conflito com a primeira lei.";
        roteiro[6] = " 3. os robôs devem proteger a sua própria existência, desde que tal proteção não entre em conflito com a primeira ou a segunda lei.";
        roteiro[7] = " ...";
        roteiro[8] = " Parece que nosso robô moderno está com dificuldades. Mas se você, aí assistindo, sabe a resposta, ligue para o número na tela e participe do nosso novo programa - Responda Se Puder!";
        roteiro[9] = " Responda Se Puder... Acho que sei a resposta, essas são as três leis da robótica, criadas por Asimov.";
        roteiro[10] = " Hum... talvez seja a minha chance de sair deste lixão e dar um upgrade na minha vida! Parece interessante. Acho que posso fazer isso.";
        roteiro[11] = " Você poderia me ajudar? ";

    }
    // Update is called once per frame

    void CarregaTextoNaTela(){

        velocidade += Time.deltaTime*10;
        if(velocidade > 0.7){
            texto.text += roteiro_aux[carrega];
            carrega++;
            velocidade = 0;

            if(carrega == roteiro_aux.Length){
                fim = true;
                
            }

        }
    }
    void Update()
    {
        if(fim == false){
            CarregaTextoNaTela();
        }
        
        if (Input.GetKeyDown(KeyCode.Return)) {
            Prosseguir();
        }

        if(num_texto > 3 && num_texto < 8 || num_texto == 9){
            nome.text = "Apresentador";
        }else if(num_texto == 8){
            nome.text = "Smartio";
        }else{
            nome.text = "Robs";
        }
        
    }

}
