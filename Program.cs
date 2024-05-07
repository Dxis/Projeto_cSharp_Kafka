using System;

class Program
{
    static void Main(string[] args)
    {
        //classe teste de boas vindas
        Teste teste = new Teste();        
        teste.PrintHelloWorld();

        //Classe publica de produção de mensagem com topico  fixo - mensageria 
        Producer producer = new Producer();
        producer.Produce();

        //Classe publica de produção de mensagem com topico  fixo - mensageria 
        //Consumer consumer = new Consumer();
        //consumer.Consume();
    }
}
