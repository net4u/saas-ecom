﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SaasEcom.Core.Models
{
    public class SaasEcomUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime CreatedAt { get; set; }

        public string StripeCustomerId { get; set; }
        
        public virtual IList<Subscription> Subscriptions { get; set; }

        public virtual IList<Invoice> Invoices { get; set; }

        public virtual IList<CreditCard> CreditCards { get; set; } // The actual credit card number is not stored! 

        public string IPAddress { get; set; }

        public string IPAddressCountry { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<SaasEcomUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public bool HasUserDetails()
        {
            if (string.IsNullOrEmpty(FirstName) ||
                string.IsNullOrEmpty(LastName))
            {
                return false;
            }

            return true;
        }

        public bool HasPaymentDetails()
        {
            return CreditCards != null && CreditCards.Any();
        }
    }
}