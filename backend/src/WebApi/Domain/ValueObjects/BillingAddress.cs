namespace WebApi.Domain.ValueObjects;

internal sealed record BillingAddress(
    string Brand,
    string Street,
    string PostalCode,
    string City
);