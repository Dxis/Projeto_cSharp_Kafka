using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Confluent.Kafka;

public class Consumer
{     
    public static IConfiguration readConfig() 
    {
    // reads the client configuration from client.properties
    // and returns it as a configuration object
    return new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddIniFile("client.properties", false)
    .Build();
    }

    public void Consume()
    {
        // producer and consumer code here      
        IConfiguration configuration = readConfig();
        const string topic = "topic_0";
        
        configuration["group.id"] = "csharp-group-1";
        configuration["auto.offset.reset"] = "earliest";
        
        // creates a new consumer instance 
        using (var consumer = new ConsumerBuilder<string, string>(configuration.AsEnumerable()).Build()) 
        {
            consumer.Subscribe(topic);
            try
            {
                while (true) 
                {
                    // consumes messages from the subscribed topic and prints them to the console
                    var cr = consumer.Consume();
                    Console.WriteLine($"Consumed event from topic {topic}: key = {cr.Message.Key,-10} value = {cr.Message.Value}");
                }
            }
            catch (OperationCanceledException)
            {
                // Quando o loop for interrompido, essa exceção será lançada
            }
            finally
            {
                // fecha a conexão do consumidor
                consumer.Close();
            }
        }
    }
}





