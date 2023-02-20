using CRB.TPM.MessageQueue.RabbitMQ;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using MessageQueue.RabbitMQ.Tests.XUnitExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueue.RabbitMQ.Tests.UnitTests
{
    /// <summary>
    /// 主题模式
    /// </summary>
    public class TopicTests
    {
        private readonly RabbitMQClient _rabbitMQ;
        private readonly ILogger<TopicTests> _logger;
        private readonly RabbitMQDeclareSettings settings = new RabbitMQDeclareSettings();
        private static object _Lock__Consumer_Num = new object();

        ExchangeDeclareSettings _exchange;
        BindQuerySettings _settingsBusinessZongBu;
        BindQuerySettings _settingsBusinessSiChuan;
        const string routingKeyBusinessKey1 = "ZongBu.routingKeyBusinessKey";
        const string routingKeyBusinessKey2 = "SiChuan.routingKeyBusinessKey";
        const string TopicroutingKeyBusinessKey1 = "*.routingKeyBusinessKey";
        const string TopicroutingKeyBusinessKey2 = "SiChuan.routingKeyBusinessKey";

        public TopicTests(RabbitMQClient rabbitMQ, ILogger<TopicTests> logger)
        {
            this._rabbitMQ = rabbitMQ;
            this._logger = logger;
            _exchange = new ExchangeDeclareSettings()
            {
                Type = ExchangeType.Topic,
                AutoDelete = false,
                Durable = false,
                Name = "Exchange_Topic"
            };
            var queueZongBuBusiness = new QueueDeclareSettings()
            {
                Durable = false,
                Exclusive = false,
                AutoDelete = false,
                Arguments = null,
                Name = "queueZongBuBusiness_Topic"
            };
            var queueSiChuanBusiness = new QueueDeclareSettings()
            {
                Durable = false,
                Exclusive = false,
                AutoDelete = false,
                Arguments = null,
                Name = "queueSiChuanBusiness_Topic"
            };
            _rabbitMQ.QueueBind(_settingsBusinessZongBu = new BindQuerySettings(_exchange, queueZongBuBusiness, new List<string>() { TopicroutingKeyBusinessKey1 }));
            _rabbitMQ.QueueBind(_settingsBusinessSiChuan = new BindQuerySettings(_exchange, queueSiChuanBusiness, new List<string>() { TopicroutingKeyBusinessKey2 }));
        }

        [Fact, Order(1)]
        public void Producer_Topic()
        {
            int num = 100;
            _logger.LogInformation("主题-直连模式");
            var datas = SP_ConfigBusinessItemL1Entity.CreateTestDataList(num);
            foreach (var data in datas)
            {
                _rabbitMQ.Send(data, _exchange.Name, routingKeyBusinessKey1);
                _rabbitMQ.Send(data, _exchange.Name, routingKeyBusinessKey2);
                _logger.LogInformation("推送业务事项Code：" + data.Code);
                Thread.Sleep(10);
            }

            Assert.True(datas.Any());
        }

        [Fact, Order(2)]
        public void Consumer_Topic()
        {
            int num = 0;
            _logger.LogInformation("消费者-主题模式");
            var consumer1 = _rabbitMQ.Receive<SP_ConfigBusinessItemL1Entity>(_settingsBusinessZongBu.Queue, data =>
            {
                if (data != null)
                {
                    lock (_Lock__Consumer_Num)
                    {
                        num++;
                        _logger.LogInformation("消费业务事项Code：" + data.Code);
                        return true;
                    }
                }
                return false;
            });
            var consumer2 = _rabbitMQ.Receive<SP_ConfigBusinessItemL1Entity>(_settingsBusinessSiChuan.Queue, data =>
            {
                if (data != null)
                {
                    lock (_Lock__Consumer_Num)
                    {
                        num++;
                        _logger.LogInformation("消费业务事项Code：" + data.Code);
                        return true;
                    }
                }
                return false;
            });
            while (num < 300)
            {
                Thread.Sleep(500);
            }
            Assert.Equal(300, num);
        }
    }
}
