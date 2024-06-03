namespace TaskManagement.Core.Domain.Commons;

/// <summary>
/// Clase que representa las propiedades de auditoria.
/// </summary>
public class AuditoryProperties
{
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