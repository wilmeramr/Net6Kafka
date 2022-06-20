using Banking.Account.Command.Application.Aggregates;
using Banking.Account.Command.Application.Contracts.Persistence;
using Banking.Account.Command.Application.Models;
using Banking.Account.Command.Infrastructure.KafkaEvents;
using Banking.Account.Command.Infrastructure.Repositories;
using Banking.Cqrs.Core.Events;
using Banking.Cqrs.Core.Handlers;
using Banking.Cqrs.Core.Infrastructure;
using Banking.Cqrs.Core.Producers;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;

namespace Banking.Account.Command.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var kafkaSettingHostName = configuration.GetSection("KafkaSettings:HostName").Value;
            var kafkaSettingPort = configuration.GetSection("KafkaSettings:Port").Value;


            BsonClassMap.RegisterClassMap<BaseEvent>();
            BsonClassMap.RegisterClassMap<AccountOpenedEvent>();
            BsonClassMap.RegisterClassMap<AccountClosedEvent>();
            BsonClassMap.RegisterClassMap<FundsDepositedEvent>();
            BsonClassMap.RegisterClassMap<FundsWithdrawnEvent>();

            var bankTopic = new string[]
               {
                    typeof(AccountOpenedEvent).Name,
                    typeof(AccountClosedEvent).Name,
                    typeof(FundsDepositedEvent).Name,
                    typeof(FundsWithdrawnEvent).Name,
               };
            using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = $"{kafkaSettingHostName}:{kafkaSettingPort}" }).Build();
            
                try
                {
                  //  adminClient.DeleteTopicsAsync(bankTopic).Wait();

                    bankTopic.ToList().ForEach(x =>
                    {
                        var metada = adminClient.GetMetadata(x, TimeSpan.FromMilliseconds(3000))!.Topics!.FirstOrDefault()!.Partitions.Count == 0;
                       // var metada2 = adminClient.GetMetadata(x, TimeSpan.FromMilliseconds(300))!.Topics!.FirstOrDefault()!.Partitions.Count == 0;

                        //if (metada)
                        //adminClient.CreateTopicsAsync(new TopicSpecification[] {
                        //     new TopicSpecification { Name =  x, ReplicationFactor = 1, NumPartitions = 2 } }).Wait();
                    });
                
                }
                catch (CreateTopicsException e)
                {
                    Console.WriteLine($"An error occured creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
                }


            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddScoped<EventProducer, AccountEventProducer>();
            services.AddTransient<IEventStoreRepository, EventStoreRepository>();
            services.AddTransient<EventStore, AccountEventStore>();
            services.AddTransient<EventSourcingHandler<AccountAggregate>, AccountEventSourcingHandler>();

            return services;
        }

    }
}
