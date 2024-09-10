using Confluent.Kafka;
using FileProcessConsume;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace Consumer
{
    public class KafkaConsumer
    {
        private readonly ConsumerConfig _config;
        private readonly string _topic;
        private readonly string _connString;

        public KafkaConsumer(IConfiguration configuration)
        {
            _config = new ConsumerConfig
            {
                GroupId = configuration["EventBrokerConfig:groupId"],
                BootstrapServers = configuration["EventBrokerConfig:brokerList"],
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _topic = configuration["EventBrokerConfig:topics"];
            _connString = configuration["ConnectionStrings:DefaultConnection"];
            Task.Run(() =>  Subscribe(CancellationToken.None));
        }

        public async Task Subscribe(CancellationToken stoppingToken)
        {
            using var consumer = new ConsumerBuilder<string, string>(_config).Build();
            FilesData filesData = new(_connString);
            JObject obj = new JObject();
            try
            {
                consumer.Subscribe(_topic.Split(",").Distinct());
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(stoppingToken);
                        if (consumeResult?.Message == null)
                        {
                            continue;
                        }

                        obj = JsonConvert.DeserializeObject<JObject>(consumeResult?.Message.Value);
                        filesData.UpdateStatusByFileName(Convert.ToInt64(obj["Id"]), obj["FileName"].ToString(), "InProgress", obj["FilePath"].ToString());
                        Console.WriteLine(consumeResult?.Message.Value);
                        await ImportCsv.ImportCsvUsingFilePath(obj["FilePath"].ToString(), _connString);
                        filesData.UpdateStatusByFileName(Convert.ToInt64(obj["Id"]), obj["FileName"].ToString(), "Completed", obj["FilePath"].ToString());
                        consumer.Commit(consumeResult);
                    }
                    catch (ConsumeException ex)
                    {
                        Console.WriteLine("Consume Execption Occurred : " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception Occurred at Subscribe Method  while : " + ex.Message);
                        filesData.UpdateStatusByFileName(Convert.ToInt64(obj["Id"]), obj["FileName"].ToString(), "Completed", obj["FilePath"].ToString());
                    }
                  
                }
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine("Operation Canceled Exception : " + ex.Message);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occurred at Subscribe Method : " + ex.Message);
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}