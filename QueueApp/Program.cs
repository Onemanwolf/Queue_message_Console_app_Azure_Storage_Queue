using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System.Linq;

namespace QueueApp
{
    class Program
    {
        public static string Exit { get; private set; } = "no-exit";
        private static QueueClient queueClient;
        public static int _batchSize { get; set; }

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // Set
            string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
    
            queueClient = new QueueClient(connectionString, "js-queue-items");
            Console.WriteLine("Enter Batch Size:");
            var bactchSize = Console.ReadLine();
            _batchSize = int.Parse(bactchSize);
            await BatchJob(queueClient, _batchSize );

              Console.WriteLine("Enter exit to quite or batchSize");
             var arg = Console.ReadLine();
             Exit = arg;
              if(arg == "exit"){
                Environment.Exit(0);
              }
              
              _batchSize = int.Parse(arg);
              await BatchJob(queueClient, _batchSize );
              Console.WriteLine("Done");
        }

        private static async Task BatchJob(QueueClient queue, int batchSize)
        {
           var queueMessage = "Hello Keda";
            var messageBatch = new List<string>();
            for(int i = 0; i < batchSize; i++){
               messageBatch.Add(queueMessage);
            }
            Console.WriteLine($"Sending {messageBatch.Count} messages to queue");
              if(messageBatch.Count != 0){
              while(messageBatch.Count > 0){
               var messagetoque =  messageBatch.FirstOrDefault(message =>  message != String.Empty);
               messageBatch.Remove(messagetoque);
               Console.WriteLine($"Sending {messageBatch.Count} to queue");
                     await InsertMessageAsync(queue, messagetoque);
                     };
                     
              }
             Console.WriteLine("Enter exit to quite or batchSize");
             var arg = Console.ReadLine();
             Exit = arg;
              if(Exit == "exit"){

                Environment.Exit(0);}
                else{
                 await BatchJob(queue, _batchSize);
                };
                              
        }

        static async Task InsertMessageAsync(QueueClient theQueue, string newMessage)
        {
            if (null != await theQueue.CreateIfNotExistsAsync())
            {
                 Console.WriteLine("The queue was created.");
            }
           
                await theQueue.SendMessageAsync(newMessage);
            
           // await theQueue.SendMessageAsync(newMessage);
        }
    
    }
}



