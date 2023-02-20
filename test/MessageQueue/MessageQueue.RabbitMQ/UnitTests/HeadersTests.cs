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
    /// 头交换机模式
    /// </summary>
    public class HeadersTests
    {
        private readonly RabbitMQClient _rabbitMQ;
        private readonly ILogger<HeadersTests> _logger;
        private readonly RabbitMQDeclareSettings settings = new RabbitMQDeclareSettings();
        private static object _Lock__Consumer_Num = new object();

        ExchangeDeclareSettings _exchange;
        BindQuerySettings _settingsBusinessZongBu;
        BindQuerySettings _settingsBusinessSiChuan;
        Dictionary<string, object> ZongBuBindHeader = new Dictionary<string, object>()
        {
            { "x-match", "any" },
            { "pwd", "123" },
            { "BusinesName", "Bkey1" }
        };
        Dictionary<string, object> SiChuanBindHeader = new Dictionary<string, object>()
        {
            { "x-match", "all" },
            { "pwd", "321" },
            { "BusinesName", "Bkey1" }
        };

        Dictionary<string, object> headerKeyBusinessKey1 = new Dictionary<string, object>()
        {
            { "BusinesName", "Bkey1" }
        };
        Dictionary<string, object> headerKeyBusinessKey2 = new Dictionary<string, object>()
        {
            { "pwd", "321" },
            { "BusinesName", "Bkey1" }
        };

        public HeadersTests(RabbitMQClient rabbitMQ, ILogger<HeadersTests> logger)
        {
            this._rabbitMQ = rabbitMQ;
            this._logger = logger;
            _exchange = new ExchangeDeclareSettings()
            {
                Type = ExchangeType.Headers,
                AutoDelete = false,
                Durable = false,
                Name = "Exchange_Headers",

            };
            var queueZongBuBusiness = new QueueDeclareSettings()
            {
                Durable = false,
                Exclusive = false,
                AutoDelete = false,
                Arguments = null,
                Name = "queueZongBuBusiness_Headers"
            };
            var queueSiChuanBusiness = new QueueDeclareSettings()
            {
                Durable = false,
                Exclusive = false,
                AutoDelete = false,
                Arguments = null,
                Name = "queueSiChuanBusiness_Headers"
            };
            _rabbitMQ.QueueBind(_settingsBusinessZongBu = new BindQuerySettings(_exchange, queueZongBuBusiness, ZongBuBindHeader));
            _rabbitMQ.QueueBind(_settingsBusinessSiChuan = new BindQuerySettings(_exchange, queueSiChuanBusiness, SiChuanBindHeader));
        }

        [Fact, Order(1)]
        public void Producer_Headers()
        {
            int num = 100;
            _logger.LogInformation("头交换机-直连模式");
            var datas = SP_ConfigBusinessItemL1Entity.CreateTestDataList(num);
            foreach (var data in datas)
            {
                _rabbitMQ.Send(data, headerKeyBusinessKey1, _exchange.Name);
                _rabbitMQ.Send(data, headerKeyBusinessKey2, _exchange.Name);
                _logger.LogInformation("推送业务事项Code：" + data.Code);
                Thread.Sleep(10);
            }

            Assert.True(datas.Any());
        }

        [Fact, Order(2)]
        public void Consumer_Headers()
        {
            int num = 0;
            _logger.LogInformation("消费者-头交换机模式");
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
            }, headerKeyBusinessKey1);
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
            }, headerKeyBusinessKey2);
            while (num < 300)
            {
                Thread.Sleep(500);
            }
            Assert.Equal(300, num);
        }
    }
}
