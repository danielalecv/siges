using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

using siges.Models;

namespace siges.Areas.Identity.Data {
  public class RoatechIdentityUser : IdentityUser, IComparable<RoatechIdentityUser> {
    public Persona per { get; set; }
    public Direccion dir { get; set; }

    public int CompareTo(RoatechIdentityUser comparePart) {
      if (comparePart == null)
        return 1;
      else
        return (this.per.Nombre+this.per.Paterno+this.per.Materno).CompareTo(comparePart.per.Nombre+comparePart.per.Paterno+comparePart.per.Materno);
    }
  }
}
