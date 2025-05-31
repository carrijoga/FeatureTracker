namespace FeatureTracker.Domain.Enums.Persons;

/// <summary>
///     Defines the different types of persons in the system.
/// </summary>
public enum TypePersons
{
    /// <summary>
    ///     Represents an undefined or unassigned person type.
    /// </summary>
    None = 0,

    /// <summary>
    ///     System administrator with full access and control.
    /// </summary>
    Admin = 1,

    /// <summary>
    ///     Manager role with high-level management capabilities.
    /// </summary>
    Manager = 2,

    /// <summary>
    ///     Supervisor role with oversight responsibilities.
    /// </summary>
    Supervisor = 3,

    /// <summary>
    ///     Regular employee of the organization.
    /// </summary>
    Employee = 4,

    /// <summary>
    ///     Person authorized to operate vehicles.
    /// </summary>
    Driver = 5,

    /// <summary>
    ///     Standard system user with basic access.
    /// </summary>
    User = 6,

    /// <summary>
    ///     Person with read-only access to specific content.
    /// </summary>
    Viewer = 7,

    /// <summary>
    ///     Customer or service recipient.
    /// </summary>
    Client = 8,

    /// <summary>
    ///     Person or entity that provides goods or services.
    /// </summary>
    Supplier = 9,

    /// <summary>
    ///     Person or company that transports goods.
    /// </summary>
    Carrier = 10,

    /// <summary>
    ///     Temporary visitor with limited access.
    /// </summary>
    Visitor = 11
}
