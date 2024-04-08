using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AuthSystem.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string FirstName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    [StringLength(100)] // Optional: Limit the length of the URL
    [Url] // Validate that the property is a valid URL
    public string LinkedinURL { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    [StringLength(100)] // Optional: Limit the length of the URL
    [Url] // Validate that the property is a valid URL
    public string FacebookURL { get; set; }



    [PersonalData]
    public List<Additional>? Additional {  get; set; }
}

