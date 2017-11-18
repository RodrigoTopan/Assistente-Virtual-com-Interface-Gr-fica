using System.IO;
using AIMLbot;
using System.Xml;
using System;
using System.Collections.Generic;
public class AIML
        {
            const string UserId = "CityU.Scm.David";



            public static string pathFiles = "aiml";//Pasta dos arquivos aiml
                                                    //método para pegar palavras e expressoes dos aiml
            public static string[] GetWordsOrSentences()
            {
                string[] files = Directory.GetFiles(pathFiles, "*.aiml");//vetor de caminhos para arquivos aiml
                List<string> wordsOrSentences = new List<string>();//resultado
                foreach (var file in files)
                {//for each para passar por todos os arquivos
                    try
                    {
                        XmlDocument doc = new XmlDocument();// instancia de um documento do tipo xml
                        doc.Load(file);//carregando arquivo
                        foreach (XmlElement ele in doc.GetElementsByTagName("pattern"))
                        { //pegando o pattern do documento
                            string word = ele.InnerText.Replace("*", "");
                            word = word.Trim();
                            wordsOrSentences.Add(word); //addicionando palavra ou expressão na lista
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Erro no arquivo: " + file + "\nErro: " + ex.Message);
                    }
                }
                return wordsOrSentences.ToArray();
            }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        public static string[] getRespostas()
            {
                string[] files = Directory.GetFiles(pathFiles, "*.aiml");//vetor de caminhos para arquivos aiml
                List<string> respostas = new List<string>();// lista de respostas
                foreach (var file in files)
                {//for each para passar por todos os arquivos
                    try
                    {
                        XmlDocument doc = new XmlDocument();// instancia de um documento do tipo xml
                        doc.Load(file);//carregando arquivo
                        foreach (XmlElement ele in doc.GetElementsByTagName("template"))
                        { //pegando os templates do documento
                            string word = ele.InnerText.Replace("*", "");
                            word = word.Trim();
                            respostas.Add(word); //addicionando palavra ou expressão na lista
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Erro no arquivo: " + file + "\nErro: " + ex.Message);
                    }
                }
                return respostas.ToArray();
            }


            public static void ConfigAIMLFiles()
            {

                string[] files = Directory.GetFiles(pathFiles, "*.aiml");//vetor de caminho para arquivos aiml
                int index = 0;
                foreach (var file in files)//passando por todos os arquivos
                {
                    string[] lines = File.ReadAllLines(file);//ler todas as linhas. Vetor de Linhas

                    foreach (var line in lines)//PASSANDO POR TODAS AS LINHAS DE TODOS OS ARQUIVOS
                    {
                        if (line.StartsWith("<pattern>"))//PROCURAR TAGS PATTERN
                        {
                            string temp = RemoverAcentos(line);//CHAMADA DE METODO REMOVER ACENTOS

                        }

                    }

                    index++;// aumenta a cada arquivo encontrado
                }
            }

            // Loads all the AIML files in the \AIML folder         
            public static string GetOutputChat(string chat)//Método para saida
            {
                Bot Bot = new Bot();//Instanciando bot
                User myUser = new User(UserId, Bot);
                Bot.loadSettings();
                Bot.isAcceptingUserInput = false;
                Bot.loadAIMLFromFiles();
                Bot.isAcceptingUserInput = true;

                //myBot.loadSettings();//Carregando configurações
                //User user = new User("pedro", myBot);//user
                //myBot.isAcceptingUserInput = false;

                //myBot.loadAIMLFromFiles();//carregando arquivos aiml
                //myBot.isAcceptingUserInput = true;


                Request r = new Request(RemoverAcentos(chat), myUser, Bot);
                Result res = Bot.Chat(r);
                return res.Output;
            }



            public static string RemoverAcentos(string texto)
            {
                if (string.IsNullOrEmpty(texto))
                    return String.Empty;
                else
                {
                    byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(texto);
                    return System.Text.Encoding.UTF8.GetString(bytes);
                }
            }
        }




 






