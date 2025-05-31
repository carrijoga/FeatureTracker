using FeatureTracker.Shared.ViewModel;

namespace FeatureTracker.Domain.Model.Persons;

public class Person
{
    #region Proprieties

    public int PersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Phone { get; set; }
    public DateTime? BirthDay { get; set; }
    public string? Cpf { get; set; }
    public string? Cnpj { get; set; }
    public List<PersonType> Types { get; set; }
    public List<PersonAddress> Address { get; set; }

    #endregion

    #region Methods

    public string GetFullName() =>
        $"{FirstName} {LastName}";

    public Person CreateNewPerson(UserRegisterViewModel userRegisterInfo) =>
        new()
        {
            FirstName = userRegisterInfo.FirstName,
            LastName = userRegisterInfo.LastName,
            Phone = userRegisterInfo?.Phone,
            BirthDay = userRegisterInfo?.BirthDay,
            Cpf = userRegisterInfo?.Cpf,
            Cnpj = userRegisterInfo?.Cnpj,
            Types =
            [
                new()
                {
                    Type = Enums.Persons.TypePersons.User
                }
            ],
        };

    #endregion
}
