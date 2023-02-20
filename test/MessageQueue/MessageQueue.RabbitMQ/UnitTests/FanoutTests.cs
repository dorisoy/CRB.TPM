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
    /// 广播模式
    /// </summary>
    public class FanoutTests
    {
        private readonly RabbitMQClient _rabbitMQ;
        private readonly ILogger<FanoutTests> _logger;
        private static object _Lock__Consumer_Num = new object();
        BindQuerySettings _settingsSiChuan;
        BindQuerySettings _settingsZheJiang;
        BindQuerySettings _settingsTianJin;
        public FanoutTests(RabbitMQClient rabbitMQ, ILogger<FanoutTests> logger)
        {
            this._rabbitMQ = rabbitMQ;
            this._logger = logger;
            var exchange = new ExchangeDeclareSettings()
            {
                Type = ExchangeType.Fanout,
                AutoDelete = false,
                Durable = false,
                Name = "Exchange_Fanout"
            };
            var queueSiChuan = new QueueDeclareSettings()
            {
                Name = "FanoutQueue_SiChuan_Fanout",
                Durable = false,
                Exclusive = false,
                AutoDelete = false,
                Arguments = null,
            };
            var queueTianJin = new QueueDeclareSettings()
            {
                Name = "FanoutQueue_TianJin_Fanout",
                Durable = false,
                Exclusive = false,
                AutoDelete = false,
                Arguments = null,
            };
            var queueZheJiang = new QueueDeclareSettings()
            {
                Name = "FanoutQueue_ZheJiang_Fanout",
                Durable = false,
                Exclusive = false,
                AutoDelete = false,
                Arguments = null,
            };
            _rabbitMQ.QueueBind(_settingsSiChuan = new BindQuerySettings(exchange, queueSiChuan));
            _rabbitMQ.QueueBind(_settingsTianJin = new BindQuerySettings(exchange, queueTianJin));
            _rabbitMQ.QueueBind(_settingsZheJiang = new BindQuerySettings(exchange, queueZheJiang));
        }

        [Fact, Order(1)]
        public void Producer_Fanout()
        {
            int num = 1;
            _logger.LogInformation("生产者-广播模式");
            var datas = SP_ConfigBusinessItemL1Entity.CreateTestDataList(num);

            foreach (var data in datas)
            {
                _rabbitMQ.Send(data, "Exchange_Fanout");
                _logger.LogInformation("推送业务事项Code：" + data.Code);
                Thread.Sleep(10);
            }
            Assert.True(datas.Any());
        }

        [Fact, Order(2)]
        public void Consumer_Fanout()
        {
            int num = 0;
            _logger.LogInformation("消费者-广播模式");
            var consumer1 = _rabbitMQ.Receive<SP_ConfigBusinessItemL1Entity>(_settingsSiChuan.Queue, data =>
            {
                if (data != null)
                {
                    lock (_Lock__Consumer_Num)
                    {
                        num++;
                        _logger.LogInformation("四川 -> 消费业务事项Code：" + data.Code);
                        return true;
                    }
                }
                return false;
            });
            var consumer2 = _rabbitMQ.Receive<SP_ConfigBusinessItemL1Entity>(_settingsTianJin.Queue, data =>
            {
                if (data != null)
                {
                    lock (_Lock__Consumer_Num)
                    {
                        num++;
                        _logger.LogInformation("浙江 —> 消费业务事项Code：" + data.Code);
                        return true;
                    }
                }
                return false;
            });
            var consumer3 = _rabbitMQ.Receive<SP_ConfigBusinessItemL1Entity>(_settingsZheJiang.Queue, data =>
            {
                if (data != null)
                {
                    lock (_Lock__Consumer_Num)
                    {
                        num++;
                        _logger.LogInformation("天津 —> 消费业务事项Code：" + data.Code);
                        return true;
                    }
                }
                return false;
            });
            while (num < 3)
            {
                Thread.Sleep(500);
            }
            Assert.Equal(3, num);
        }
    }
}
