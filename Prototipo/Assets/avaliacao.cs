using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

    public class avaliacao : MonoBehaviour
    {

        public Button estrela_comunicativo_1;
        public Button estrela_comunicativo_2;
        public Button estrela_comunicativo_3;
        public Button estrela_engajado_1;
        public Button estrela_engajado_2;
        public Button estrela_engajado_3;
        public Button estrela_gentil_1;
        public Button estrela_gentil_2;
        public Button estrela_gentil_3;

        // public int id_avaliacao;
        public GameObject quadro;

        int estrela_comunicativo = 0;
        int estrela_engajado = 0;
        int estrela_gentil = 0;


        public Sprite estrela_clicada;
        public Sprite estrela_n_clicada;


        public void setElogio(int elogio, int valor)
        {

                for (int i = 0; i < dadosTimes.meuTime.Count; i++)
                {
                    // Debug.Log(dadosTimes.meuTime[i].id);
                    // Debug.Log("ID da avaliação: " +  quadro.GetComponent<avaliacao>().id_avaliacao);

                    if (dadosTimes.meuTime[i].id ==  quadro.GetComponent<id_avaliacao>().id_auxiliar_avaliacao)
                    {
                        if (elogio == 0){
                            dadosTimes.meuTime[i].elogio1 = valor;
                        }
                        if (elogio == 1){
                            dadosTimes.meuTime[i].elogio2 = valor;
                        }
                        if (elogio == 2){
                            dadosTimes.meuTime[i].elogio3 = valor;
                        }
                        

                        Debug.Log("O player " + dadosTimes.meuTime[i].name + " tem os seguintes elogios: ");
                        Debug.Log("Comunicativo " + dadosTimes.meuTime[i].elogio1);
                        Debug.Log("Engajado " + dadosTimes.meuTime[i].elogio2);
                        Debug.Log("Gentil " + dadosTimes.meuTime[i].elogio3);
                    }
                }
        }

        public void resetaEstrelas()
        {

                estrela_comunicativo = 0;
                Image image_comunicativo_1 = estrela_comunicativo_1.GetComponent<Image>();
                image_comunicativo_1.sprite = estrela_n_clicada;

                Image image_comunicativo_2 = estrela_comunicativo_2.GetComponent<Image>();
                image_comunicativo_2.sprite = estrela_n_clicada;

                Image image_comunicativo_3 = estrela_comunicativo_3.GetComponent<Image>();
                image_comunicativo_3.sprite = estrela_n_clicada;

                setElogio(0,0);

                estrela_engajado = 0;
                Image image_engajado_1 = estrela_engajado_1.GetComponent<Image>();
                image_engajado_1.sprite = estrela_n_clicada;

                Image image_engajado_2 = estrela_engajado_2.GetComponent<Image>();
                image_engajado_2.sprite = estrela_n_clicada;

                Image image_engajado_3 = estrela_engajado_3.GetComponent<Image>();
                image_engajado_3.sprite = estrela_n_clicada;
            
                setElogio(1, 0);

                estrela_gentil = 0;
                Image image_gentil_1 = estrela_gentil_1.GetComponent<Image>();
                image_gentil_1.sprite = estrela_n_clicada;

                Image image_gentil_2 = estrela_gentil_2.GetComponent<Image>();
                image_gentil_2.sprite = estrela_n_clicada;

                Image image_gentil_3 = estrela_gentil_3.GetComponent<Image>();
                image_gentil_3.sprite = estrela_n_clicada;

                setElogio(2, 0);

        }


        public void click_estrela_comunicativo_1()
        {
            if (estrela_comunicativo != 1) {
                estrela_comunicativo = 1;
                Image image_comunicativo_1 = estrela_comunicativo_1.GetComponent<Image>();
                image_comunicativo_1.sprite = estrela_clicada;

                Image image_comunicativo_2 = estrela_comunicativo_2.GetComponent<Image>();
                image_comunicativo_2.sprite = estrela_n_clicada;

                Image image_comunicativo_3 = estrela_comunicativo_3.GetComponent<Image>();
                image_comunicativo_3.sprite = estrela_n_clicada;

                setElogio(0,1);
            }
            else {
                estrela_comunicativo = 0;
                Image image_comunicativo_1 = estrela_comunicativo_1.GetComponent<Image>();
                image_comunicativo_1.sprite = estrela_n_clicada;

                Image image_comunicativo_2 = estrela_comunicativo_2.GetComponent<Image>();
                image_comunicativo_2.sprite = estrela_n_clicada;

                Image image_comunicativo_3 = estrela_comunicativo_3.GetComponent<Image>();
                image_comunicativo_3.sprite = estrela_n_clicada;

                setElogio(0,0);

            }
        }

        public void click_estrela_comunicativo_2()
        {
            if (estrela_comunicativo != 2) {
                estrela_comunicativo = 2;
                Image image_comunicativo_1 = estrela_comunicativo_1.GetComponent<Image>();
                image_comunicativo_1.sprite = estrela_clicada;

                Image image_comunicativo_2 = estrela_comunicativo_2.GetComponent<Image>();
                image_comunicativo_2.sprite = estrela_clicada;

                Image image_comunicativo_3 = estrela_comunicativo_3.GetComponent<Image>();
                image_comunicativo_3.sprite = estrela_n_clicada;

                setElogio(0,2);

            }
            else {
                estrela_comunicativo = 0;
                Image image_comunicativo_1 = estrela_comunicativo_1.GetComponent<Image>();
                image_comunicativo_1.sprite = estrela_clicada;

                Image image_comunicativo_2 = estrela_comunicativo_2.GetComponent<Image>();
                image_comunicativo_2.sprite = estrela_n_clicada;

                Image image_comunicativo_3 = estrela_comunicativo_3.GetComponent<Image>();
                image_comunicativo_3.sprite = estrela_n_clicada;

                setElogio(0,1);

            }

        }

        public void click_estrela_comunicativo_3()
        {
            if (estrela_comunicativo != 3) {
                estrela_comunicativo = 3;
                Image image_comunicativo_1 = estrela_comunicativo_1.GetComponent<Image>();
                image_comunicativo_1.sprite = estrela_clicada;

                Image image_comunicativo_2 = estrela_comunicativo_2.GetComponent<Image>();
                image_comunicativo_2.sprite = estrela_clicada;

                Image image_comunicativo_3 = estrela_comunicativo_3.GetComponent<Image>();
                image_comunicativo_3.sprite = estrela_clicada;
            
                setElogio(0,3);

                Debug.Log("Entrou");

            }
            else {
                estrela_comunicativo = 0;
                Image image_comunicativo_1 = estrela_comunicativo_1.GetComponent<Image>();
                image_comunicativo_1.sprite = estrela_clicada;

                Image image_comunicativo_2 = estrela_comunicativo_2.GetComponent<Image>();
                image_comunicativo_2.sprite = estrela_clicada;

                Image image_comunicativo_3 = estrela_comunicativo_3.GetComponent<Image>();
                image_comunicativo_3.sprite = estrela_n_clicada;

                setElogio(0, 2);
                Debug.Log("Entrou");

            }

        }

        public void click_estrela_engajado_1()
        {
            if (estrela_engajado != 1) {
                estrela_engajado = 1;
                Image image_engajado_1 = estrela_engajado_1.GetComponent<Image>();
                image_engajado_1.sprite = estrela_clicada;

                Image image_engajado_2 = estrela_engajado_2.GetComponent<Image>();
                image_engajado_2.sprite = estrela_n_clicada;

                Image image_engajado_3 = estrela_engajado_3.GetComponent<Image>();
                image_engajado_3.sprite = estrela_n_clicada;

                setElogio(1, 1);
            }
            else {
                estrela_engajado = 0;
                Image image_engajado_1 = estrela_engajado_1.GetComponent<Image>();
                image_engajado_1.sprite = estrela_n_clicada;

                Image image_engajado_2 = estrela_engajado_2.GetComponent<Image>();
                image_engajado_2.sprite = estrela_n_clicada;

                Image image_engajado_3 = estrela_engajado_3.GetComponent<Image>();
                image_engajado_3.sprite = estrela_n_clicada;
            
                setElogio(1, 0);
            }
        }

        public void click_estrela_engajado_2()
        {
            if (estrela_engajado != 2) {
                estrela_engajado = 2;
                Image image_engajado_1 = estrela_engajado_1.GetComponent<Image>();
                image_engajado_1.sprite = estrela_clicada;

                Image image_engajado_2 = estrela_engajado_2.GetComponent<Image>();
                image_engajado_2.sprite = estrela_clicada;

                Image image_engajado_3 = estrela_engajado_3.GetComponent<Image>();
                image_engajado_3.sprite = estrela_n_clicada;
            
                setElogio(1, 2);
            }
            else {
                estrela_engajado = 0;
                Image image_engajado_1 = estrela_engajado_1.GetComponent<Image>();
                image_engajado_1.sprite = estrela_clicada;

                Image image_engajado_2 = estrela_engajado_2.GetComponent<Image>();
                image_engajado_2.sprite = estrela_n_clicada;

                Image image_engajado_3 = estrela_engajado_3.GetComponent<Image>();
                image_engajado_3.sprite = estrela_n_clicada;

                setElogio(1, 1);
            }

        }

        public void click_estrela_engajado_3()
        {
            if (estrela_engajado != 3) {
                estrela_engajado = 3;
                Image image_engajado_1 = estrela_engajado_1.GetComponent<Image>();
                image_engajado_1.sprite = estrela_clicada;

                Image image_engajado_2 = estrela_engajado_2.GetComponent<Image>();
                image_engajado_2.sprite = estrela_clicada;

                Image image_engajado_3 = estrela_engajado_3.GetComponent<Image>();
                image_engajado_3.sprite = estrela_clicada;

                setElogio(1, 3);
            }
            else {
                estrela_engajado = 0;
                Image image_engajado_1 = estrela_engajado_1.GetComponent<Image>();
                image_engajado_1.sprite = estrela_clicada;

                Image image_engajado_2 = estrela_engajado_2.GetComponent<Image>();
                image_engajado_2.sprite = estrela_clicada;

                Image image_engajado_3 = estrela_engajado_3.GetComponent<Image>();
                image_engajado_3.sprite = estrela_n_clicada;

                setElogio(1, 2);
            }

        }

        public void click_estrela_gentil_1()
        {
            if (estrela_gentil != 1) {
                estrela_gentil = 1;
                Image image_gentil_1 = estrela_gentil_1.GetComponent<Image>();
                image_gentil_1.sprite = estrela_clicada;

                Image image_gentil_2 = estrela_gentil_2.GetComponent<Image>();
                image_gentil_2.sprite = estrela_n_clicada;

                Image image_gentil_3 = estrela_gentil_3.GetComponent<Image>();
                image_gentil_3.sprite = estrela_n_clicada;

                setElogio(2, 1);
            }
            else {
                estrela_gentil = 0;
                Image image_gentil_1 = estrela_gentil_1.GetComponent<Image>();
                image_gentil_1.sprite = estrela_n_clicada;

                Image image_gentil_2 = estrela_gentil_2.GetComponent<Image>();
                image_gentil_2.sprite = estrela_n_clicada;

                Image image_gentil_3 = estrela_gentil_3.GetComponent<Image>();
                image_gentil_3.sprite = estrela_n_clicada;

                setElogio(2, 0);
            }

        }

        public void click_estrela_gentil_2()
        {
            if (estrela_gentil != 2) {
                estrela_gentil = 2;
                Image image_gentil_1 = estrela_gentil_1.GetComponent<Image>();
                image_gentil_1.sprite = estrela_clicada;

                Image image_gentil_2 = estrela_gentil_2.GetComponent<Image>();
                image_gentil_2.sprite = estrela_clicada;

                Image image_gentil_3 = estrela_gentil_3.GetComponent<Image>();
                image_gentil_3.sprite = estrela_n_clicada;

                setElogio(2, 2);
            }
            else {
                estrela_gentil = 0;
                Image image_gentil_1 = estrela_gentil_1.GetComponent<Image>();
                image_gentil_1.sprite = estrela_clicada;

                Image image_gentil_2 = estrela_gentil_2.GetComponent<Image>();
                image_gentil_2.sprite = estrela_n_clicada;

                Image image_gentil_3 = estrela_gentil_3.GetComponent<Image>();
                image_gentil_3.sprite = estrela_n_clicada;

                setElogio(2, 1);
            }

        }

        public void click_estrela_gentil_3()
        {
            if (estrela_gentil != 3) {
                estrela_gentil = 3;
                Image image_gentil_1 = estrela_gentil_1.GetComponent<Image>();
                image_gentil_1.sprite = estrela_clicada;

                Image image_gentil_2 = estrela_gentil_2.GetComponent<Image>();
                image_gentil_2.sprite = estrela_clicada;

                Image image_gentil_3 = estrela_gentil_3.GetComponent<Image>();
                image_gentil_3.sprite = estrela_clicada;

                setElogio(2, 3);
            }
            else {
                estrela_gentil = 0;
                Image image_gentil_1 = estrela_gentil_1.GetComponent<Image>();
                image_gentil_1.sprite = estrela_clicada;

                Image image_gentil_2 = estrela_gentil_2.GetComponent<Image>();
                image_gentil_2.sprite = estrela_clicada;

                Image image_gentil_3 = estrela_gentil_3.GetComponent<Image>();
                image_gentil_3.sprite = estrela_n_clicada;

                setElogio(2, 2);
            }

        }

        // Start is called before the first frame update
        void Start()
        {        
            
        }

        // Update is called once per frame
        void Update()
        {
            // if (Manager.reset_estrelas == 1){
            //     resetaEstrelas();
            //     Manager.reset_estrelas = 0;
            // }
            
        }

        void OnEnable()
        {
            // Chama a função reseta
            resetaEstrelas();
        }

    }
