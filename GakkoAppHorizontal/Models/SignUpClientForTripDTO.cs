using System;
using System.Collections.Generic;

namespace GakkoHorizontalSlice.Models;

public class SignUpClientForTripDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string Pesel { get; set; }
    public DateTime? PaymentDate { get; set; }
}
