﻿using Application.Domain;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Infrastrucure
{
    public class PaymentStatusRepository
    {
        // Warning: Ilyet soha ne csináljatok, a connecton string ne legyen benna a kódban
        private readonly string _connectionString = @"Data Source=(localdb)\readpaymentdb;Integrated Security=True;Encrypt=True";
        private readonly ILogger<PaymentStatusRepository> _logger;
        public PaymentStatusRepository(ILogger<PaymentStatusRepository> logger)
        {
            _logger = logger;
        }
        public void InsertPaymentStatus(Guid paymentRequestId, string status, DateTime processedAt)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO PaymentStatus (PaymentRequestId, Status, ProcessedAt)" +
                           "VALUES (@PaymentRequestId, @Status, @ProcessedAt)";
                var newPaymentStatus = new
                { 
                    PaymentRequestId = paymentRequestId.ToString(), 
                    Status = status, 
                    ProcessedAt = processedAt 
                };

                var rowsAffected = connection.Execute(sql, newPaymentStatus);
                _logger.LogInformation($"{rowsAffected} row(s) inserted.");
            }
        }
        public void UpdatePaymentStatus(Guid paymentRequestId, string status, DateTime processedAt)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "Update PaymentStatus " +
                    "SET Status = @Status, " +
                    "ProcessedAt=@ProcessedAt " +
                    "WHERE PaymentRequestId=@PaymentRequestId";

                var newPaymentStatus = new 
                { 
                    PaymentRequestId = paymentRequestId.ToString(),
                    Status = status,
                    ProcessedAt = processedAt 
                };
                var rowsAffected = connection.Execute(sql, newPaymentStatus);
                _logger.LogInformation($"{rowsAffected} row(s) updated.");
            }
        }
        public List<PaymentStatusDto> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM PaymentStatus";
                var allPaymentStatus = connection.Query<PaymentStatusDto>(sql);
                return allPaymentStatus.ToList();
            }
        }
    }
}
