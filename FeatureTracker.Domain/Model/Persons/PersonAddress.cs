namespace FeatureTracker.Domain.Model.Persons;

public class PersonAddress
{
    #region Properties

    public int PersonAddressId { get; set; }
    public int PersonId { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string Complement { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
    public string? Reference { get; set; }
    public bool IsMain { get; set; }

    public Person Person { get; set; }

    #endregion

    #region Methods

    public string GetFullAddress() =>
        $"{Street}, {Number} - {Neighborhood}, " +
        $"{City} - {State}, {Country} - {ZipCode}";

    public string GetAddress() =>
        $"{Street}, {Number} - {Neighborhood}";

    #endregion
}
