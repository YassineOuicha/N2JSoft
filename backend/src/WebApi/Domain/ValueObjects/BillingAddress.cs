namespace WebApi.Domain.ValueObjects;

public sealed record BillingAddress(
    string Brand,
    string Street,
    string PostalCode,
    string City
);