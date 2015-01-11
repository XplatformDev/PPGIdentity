using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System;

namespace PPG.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        // Add First and Last Name Properties
        [Required]
        [Display(Name = "First Name")]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(45)]
        public string LastName { get; set; }
        // Full Name Propery
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                string dspFirstName = string.IsNullOrWhiteSpace(this.FirstName) ? "" : this.FirstName;
                string dspLastName = string.IsNullOrWhiteSpace(this.LastName) ? "" : this.LastName;

                return string.Format("{0} {1}", dspFirstName, dspLastName);
            }
        }

        // Add Address Properties
        [Required]
        [Display(Name = "Address Line 1")]
        [StringLength(100)]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        [StringLength(100)]
        public string AddressLine2 { get; set; }

        [Required]
        [Display(Name = "City")]
        [StringLength(100)]
        public string City { get; set; }

        [Required]
        [Display(Name = "State")]
        [StringLength(2)]
        public string State { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        [StringLength(100)]
        public string PostalCode { get; set; }

        [Display(Name = "Address")]
        public string DisplayAddress
        {
            get
            {
                string dspAddressLine1 = string.IsNullOrWhiteSpace(this.AddressLine1) ? "" : this.AddressLine1;
                string dspAddressLine2 = string.IsNullOrWhiteSpace(this.AddressLine2) ? "" : this.AddressLine2;
                string dspCity = string.IsNullOrWhiteSpace(this.City) ? "" : this.City;
                string dspState = string.IsNullOrWhiteSpace(this.State) ? "" : this.State;
                string dspPostalCode = string.IsNullOrWhiteSpace(this.PostalCode) ? "" : this.PostalCode;

                return string.Format("{0} {1} {2}, {3} {4}", dspAddressLine1, dspAddressLine2, dspCity, dspState, dspPostalCode);
            }
        }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() {  }
        public ApplicationRole(string name) : base(name) {  }
        public string Description { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}