using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.CAP.Abstractions
{
    /// <summary>
    /// CAP Data Provider
    /// </summary>
    public enum CAPProvider
    {
        [Description("SqlServer")]
        SqlServer,

        [Description("MySql")]
        MySql,

        [Description("PostgreSql")]
        PostgreSql,

        [Description("MongoDB")]
        MongoDB
    }

    /// <summary>
    /// CAP MQ Provider
    /// </summary>
    public enum CAPMQProvider
    {
        [Description("RabbitMQ")]
        RabbitMQ,

        [Description("Kafka")]
        Kafka
    }
}
