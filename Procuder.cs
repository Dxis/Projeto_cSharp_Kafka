using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Confluent.Kafka;

public class Producer
{     
    public static IConfiguration readConfig() {
    // reads the client configuration from client.properties
    // and returns it as a configuration object
    return new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddIniFile("client.properties", false)
    .Build();
  }

  public void Produce()
    {

      // producer and consumer code here
    IConfiguration configuration = readConfig();
    const string topic = "topic_0";

    // creates a new producer instance
    using (var producer = new ProducerBuilder<string, string>(configuration.AsEnumerable()).Build()) {
    // produces a sample message to the user-created topic and prints
    // a message when successful or an error occurs
    producer.Produce(topic, new Message<string, string> { Key = "key", Value = "mensagem teste 14:09" },
    (deliveryReport) => {
      if (deliveryReport.Error.Code != ErrorCode.NoError) {
        Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
      } else {
        Console.WriteLine($"Produced event to topic {topic}: key = {deliveryReport.Message.Key, -10} value = {deliveryReport.Message.Value}");
      }
    }
  );

  // send any outstanding or buffered messages to the Kafka broker
  producer.Flush(TimeSpan.FromSeconds(10));
}

    }


}


