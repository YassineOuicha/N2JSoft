namespace WebApi.Domain.ValueObjects;

internal sealed record PostalAddress(
    string Street,
    string PostalCode,
    string City,
    string Country
);