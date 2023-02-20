using CRB.TPM.MessageQueue.RabbitMQ;
using MessageQueue.RabbitMQ.Tests.XUnitExtensions;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueue.RabbitMQ.Tests.UnitTests
{
    /// <summary>
    /// 消息确认测试
    /// </summary>
    public class AckTests
    {
        private readonly RabbitMQClient _rabbitMQ;
        private readonly ILogger<DirectTests> _logger;
        private readonly RabbitMQDeclareSettings settings = new RabbitMQDeclareSettings();
        private static object _Lock__Consumer_Num = new object();

        ExchangeDeclareSettings _exchange;
        BindQuerySettings _settingsBusinessZongBu;
        const string routingKey1 = "AckTest";


        public AckTests(RabbitMQClient rabbitMQ, ILogger<DirectTests> logger)
        {
            this._rabbitMQ = rabbitMQ;
            this._logger = logger;
            _exchange = new ExchangeDeclareSettings()
            {
                Type = ExchangeType.Direct,
                AutoDelete = false,
                Durable = false,
                Name = "Exchange_Direct_Ack"
            };
            var queueZongBuBusiness_Direct = new QueueDeclareSettings()
            {
                Durable = false,
                Exclusive = false,
                AutoDelete = false,
                Arguments = null,
                Name = "queueZongBuBusiness_Direct_Ack"
            };
            _rabbitMQ.QueueBind(_settingsBusinessZongBu = new BindQuerySettings(_exchange, queueZongBuBusiness_Direct, new List<string>() { routingKey1, routingKey1 }));
        }

        [Fact, Order(1)]
        public void Producer_Direct_Ack()
        {
            int num = 100;
            _logger.LogInformation("生产者-直连模式");
            var datas = SP_ConfigBusinessItemL1Entity.CreateTestDataList(num);
            foreach (var data in datas)
            {
                _rabbitMQ.Send(data, _exchange.Name, routingKey1);
                _logger.LogInformation("推送业务事项Code：" + data.Code);
                Thread.Sleep(10);
            }
            Assert.True(datas.Any());
        }

        [Fact, Order(2)]
        public void Consumer_Direct()
        {
            int num = 0;
            _logger.LogInformation("消费者-直连模式");
            var consumer1 = _rabbitMQ.Receive<SP_ConfigBusinessItemL1Entity>(_settingsBusinessZongBu.Queue, data =>
            {
                if (data != null)
                {
                    lock (_Lock__Consumer_Num)
                    {
                        num++;
                    }
                }
                //模拟前10个正常接受
                if (num <= 10)
                {
                    _logger.LogInformation("消费业务事项Code：" + data?.Code);
                    return true;
                }
                else
                {
                    return false;
                }
            });

            while (num < 100)
            {
                Thread.Sleep(500);
            }
            Assert.True(num > 10);
        }
    }
}
