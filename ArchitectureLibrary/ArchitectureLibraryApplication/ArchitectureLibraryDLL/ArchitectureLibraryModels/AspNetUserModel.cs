using ArchitectureLibraryEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArchitectureLibraryModels
{
    public class AspNetUserModel : AuditInfoModel
    {
        public string AspNetUserId { set; get; }
        public long ClientId { get; set; }
        public string Email { set; get; }
        public bool EmailConfirmed { set; get; }
        public string PasswordHash { set; get; }
        public string SecurityStamp { set; get; }
        public long? TelephoneCountryId { set; get; }

        [Display(Name = "Telephone#")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter 10 digit valid phone#")]
        [Required(ErrorMessage = "Enter phone#")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please enter 10 digit phone#")]
        public string PhoneNumber { set; get; }

        public bool PhoneNumberConfirmed { set; get; }
        public bool TwoFactorEnabled { set; get; }
        public string LockoutEndDateUtc { set; get; }
        public bool LockoutEnabled { set; get; }
        public int AccessFailedCount { set; get; }
        public string UserName { set; get; }
        public string EmailConfirmationToken { set; get; }
        public LoginTypeEnum? LoginTypeId { set; get; }
        public UserTypeEnum? UserTypeId { set; get; }
        public UserStatusEnum? UserStatusId { set; get; }
        public string LoginPassword { set; get; }
        public string PasswordExpiry { set; get; }
        public string ResetPasswordQueryString { set; get; }
        public string ResetPasswordExpiryDateTime { set; get; }
        public string ResetPasswordKey { get; set; }
        public string ResetPasswordCompletedDateTime { get; set; }
        public AspNetUserRoleModel AspNetUserRoleModel { set; get; }
        public PersonModel PersonModel { get; set; }
        public virtual ICollection<PersonModel> PersonModels { get; set; }
    }
}
