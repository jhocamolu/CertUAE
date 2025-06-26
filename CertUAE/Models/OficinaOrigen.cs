using System;
using System.Collections.Generic;

namespace CertUAE.Models;

public partial class OficinaOrigen
{
    public string IdOficinaOrigen { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Anotacion> Anotacions { get; set; } = new List<Anotacion>();
}
