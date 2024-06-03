using Microsoft.AspNetCore.Identity;
using TaskManagement.Core.Domain.Commons;

namespace TaskManagement.Infraestructure.Identity;

/// <summary>
/// Clase para representar a los usuarios del sistema.
/// Hereda de IdentityUser, para tener ya algunas de las propiedades
/// que representan un usuario,que las trae Identity
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Nombre del usuario
    /// </summary>
    public string FirstName { get; set; }
    /// <summary>
    /// Apellido del usuario
    /// </summary>
    public string LastName { get; set; }
    /// <summary>
    /// Persona el cual creo al registro
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Persona cual modifico el registro
    /// </summary>
    public string? ModifiedBy { get; set; }

    /// <summary>
    /// Fecha cuando se creo el registro
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Fecha cuando se modifico el registro.
    /// </summary>
    public DateTime ModifiedDate { get; set; }
    
    /// <summary>
    /// Representa si el registro esta activo o no, para implementar SoftDelete
    /// </summary>
    public bool IsActive { get; set; }
    
}