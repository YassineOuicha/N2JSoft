namespace WebApi.Domain.ValueObjects;

public sealed record PostalAddress(
    string Street,
    string PostalCode,
    string City,
    string Country
);