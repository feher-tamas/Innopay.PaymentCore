
-- Create the ReadPaymentStatus database and schema for CQRS system: PaymentStatusView
CREATE DATABASE ReadPaymentStatus;

USE ReadPaymentStatus;

CREATE TABLE PaymentStatusView (
    PaymentRequestId UNIQUEIDENTIFIER PRIMARY KEY,
    Status NVARCHAR(20) NOT NULL,
    ProcessedAt DATETIME NOT NULL
);
