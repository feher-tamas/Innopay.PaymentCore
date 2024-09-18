using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastrucure
{
    public class PaymentStatusRepository
    {
        private readonly string _connectionString = @"Data Source=(localdb)\readpaymentdb;Integrated Security=True;Encrypt=True";
        private readonly ILogger<PaymentStatusRepository>_logger;
        public PaymentStatusRepository(ILogger<PaymentStatusRepository>logger)
        {
            _logger=logger;
        }
        public void InsertPaymentStatus(int paymentRequestId, string status, DateTime processedAt)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "INSERT INTO PaymentStatus (PaymentRequestId, Status, ProcessedAt) VALUES (@PaymentRequestId, @Status, @ProcessedAt)";
                var newPaymentStatus = new { PaymentRequestId = paymentRequestId, Status = status, ProcessedAt= processedAt };
                var rowsAffected = connection.Execute(sql, newPaymentStatus);
                _logger.LogInformation($"{rowsAffected} row(s) inserted.");
            }
        }
    }
}
