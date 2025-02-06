namespace PropertyListing.Domain.ValueObjects;

public record Address
{
    public string Street { get; init; }
    public string District { get; init; }
    public string City { get; init; }
    public string PostalCode { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }

    public Address(string street, string district, string city, string postalCode, double latitude, double longitude)
    {
        Street = street;
        District = district;
        City = city;
        PostalCode = postalCode;
        Latitude = latitude;
        Longitude = longitude;
    }
} 