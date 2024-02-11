using System;

public class Address
{
    public Address(int studentId, string street, string city, string country)
    {
        Id = studentId;
        Street = street;
        City = city;
        Country = country;
    }

    public int Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

}
