﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace aula_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool jogadorAtual = false; // Variavel que simboliza o jogador, sendo false o jogador X e true o jogador O
        bool houveGanhador = false; // Variavel que simboliza se houve vitória
        List<Button> listaBotoes;
        string nomeJogadorX;
        string nomeJogadorO;
        Menu menu;
        public MainWindow(string nomeJogadorX, string nomeJogadorO, Menu menu) // Recebe como parametro os nomes dos jogadores e a tela de menu
        {
            InitializeComponent();
            listaBotoes = new List<Button>();
            listaBotoes.Add(btnA1); // Posição 0
            listaBotoes.Add(btnA2); // Posição 1
            listaBotoes.Add(btnA3); // Posição 2
            listaBotoes.Add(btnB1); // Posição 3
            listaBotoes.Add(btnB2); // Posição 4
            listaBotoes.Add(btnB3); // Posição 5
            listaBotoes.Add(btnC1); // Posição 6
            listaBotoes.Add(btnC2); // Posição 7
            listaBotoes.Add(btnC3); // Posição 8
            btnNovoJogo.Visibility = Visibility.Hidden;
            this.nomeJogadorX = nomeJogadorX;
            this.nomeJogadorO = nomeJogadorO;
            this.menu = menu;
        }

        /// <summary>
        /// Insere o valor X no conteúdo do botão
        /// </summary>
        /// <param name="buttonX">Botão escolhido</param>
        private void InsereX(Button buttonX)
        {
            buttonX.Content = "X";
        }

        /// <summary>
        /// Insere o valor O no conteúdo do botão
        /// </summary>
        /// <param name="buttonO">Botão escolhido</param>
        private void InsereO(Button buttonO)
        {
            buttonO.Content = "O";
        }

        /// <summary>
        /// Lógica do click do botão do tabuleiro
        /// </summary>
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender; // Converto o object em Button, que é um botão
            if(btn.Content.ToString() == "" && houveGanhador == false) // Verifico se o botão está vazio para poder inseri o X ou O e se ainda não houve ganhador
            {
                if (jogadorAtual == false) // Verifico se o jogador atual é o X, sendo ele valor falso 
                {
                    jogadorAtual = true; // Sendo o jogador falso, inverto para true para que seja a vez do jogador O
                    InsereX(btn); // Envio o botão para o método InsereX, para poder escrever o valor no botão
                    VerificaGanhador("X"); // Verifica se o jogador X Ganhou a partida
                }
                else
                {
                    jogadorAtual = false; // Sendo o jogador true, inverto para false para que seja a vez do jogador X
                    InsereO(btn);
                    VerificaGanhador("O");// Verifica se o jogador O Ganhou a partida
                }                
            }

            if(houveGanhador == false) // verifica se não houve ganhador
            {
                bool houveVelha = VerificaVelha(); // retorna a resposta do metódo VerificaVelha
                if(houveVelha == true)
                {
                    FinalizaJogo($"Deu velha!!!");
                }
            }
        }

        private bool VerificaVelha()
        {
            foreach(Button btn in listaBotoes)
            {
                if (btn.Content.ToString() == "")
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Verifica todas as possibilidades de vitória do game
        /// </summary>
        private void VerificaGanhador(string jogador) // parametro recebe o valor do jogador, sendo ele X ou O
        {
            PossibilidadeDeVitoria(jogador, 0, 1, 2); // Verificar se a vitória ocorreu através do A1, A2 e A3
            PossibilidadeDeVitoria(jogador, 3, 4, 5); // Verificar se a vitória ocorreu através do B1, B2 e B3
            PossibilidadeDeVitoria(jogador, 6, 7, 8); // Verificar se a vitória ocorreu através do C1, C2 e C3

            PossibilidadeDeVitoria(jogador, 0, 3, 6); // Verificar se a vitória ocorreu através do A1, B1 e C1
            PossibilidadeDeVitoria(jogador, 1, 4, 7); // Verificar se a vitória ocorreu através do A2, B2 e C2
            PossibilidadeDeVitoria(jogador, 2, 5, 8); // Verificar se a vitória ocorreu através do A3, B3 e C3

            PossibilidadeDeVitoria(jogador, 0, 4, 8); // Verificar se a vitória ocorreu através do A1, B2 e C3
            PossibilidadeDeVitoria(jogador, 6, 4, 2); // Verificar se a vitória ocorreu através do A1, B2 e C3
        }

        /// <summary>
        /// Verifica a possibilidade da vitória do jogador
        /// </summary>
        /// <param name="jogador">tipo do jogador, sendo X ou O</param>
        /// <param name="idPrimeiroBotao">identificador do primeiro botão a ser comparado</param>
        /// <param name="idSegundoBotao">identificador do Segundo botão a ser comparado</param>
        /// <param name="idTerceiroBotao">identificador do Terceiro botão a ser comparado</param>
        private void PossibilidadeDeVitoria(string jogador, int idPrimeiroBotao, int idSegundoBotao, int idTerceiroBotao)
        {
            if (listaBotoes[idPrimeiroBotao].Content.ToString() == jogador
                && listaBotoes[idSegundoBotao].Content.ToString() == jogador
                && listaBotoes[idTerceiroBotao].Content.ToString() == jogador) // Verifica se houve ganhador na Coluna A do jogo da velha
            {
                houveGanhador = true;
                if(jogador == "X")
                {
                    FinalizaJogo($"O ganhador foi {nomeJogadorX}");
                }
                else
                {
                    FinalizaJogo($"O ganhador foi {nomeJogadorO}");
                }
                
                return;
            }
        }

        private void FinalizaJogo(string fraseDeVitoria)
        {
            txtFraseGanhador.Text = fraseDeVitoria;
            btnNovoJogo.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Remove os conteúdos dos botões
        /// </summary>
        private void LimpaBotoes()
        {
            foreach(Button btn in listaBotoes)
            {
                btn.Content = "";
            }
        }

        private void NovoJogo(object sender, RoutedEventArgs e)
        {
            LimpaBotoes(); // Limpa o conteúdo dos botões
            jogadorAtual = false; // Faz o jogador X ser o primeiro a jogar novamente
            txtFraseGanhador.Text = ""; // Limpa a frase de vitória
            houveGanhador = false; // reinicia a possibilidade de um novo ganhador
            btnNovoJogo.Visibility = Visibility.Hidden; // oculta o botão novo jogo
        }

        private void SairDoJogo(object sender, RoutedEventArgs e)
        {
            menu.Show();
            Close();
        }
    }
}
