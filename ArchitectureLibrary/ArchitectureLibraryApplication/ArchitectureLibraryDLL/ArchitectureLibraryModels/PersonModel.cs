using ArchitectureLibraryDocumentModels;
using ArchitectureLibraryEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArchitectureLibraryModels
{
    public class PersonModel : AuditInfoModel
    {
        public long? PersonId { set; get; }

        public long? ClientId { set; get; }

        public string AspNetUserId { set; get; }

        public long? CertificateDocumentId { set; get; }

        [Display(Name = "Certificate Document")]
        public HttpPostedFileBase CertificateDocumentHttpPostedFileBase { get; set; }

        public DocumentModel CertificateDocumentModel { set; get; }

        [Display(Name = "Citizenship")]
        [Required(ErrorMessage = "Select Citizenship")]
        public CitizenshipEnum? CitizenshipId { set; get; }

        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Enter valid date of birth")]
        public DateTime? DateOfBirth { set; get; }

        [Display(Name = "License State")]
        [Required(ErrorMessage = "DL State")]
        public long? DriverLicenseDemogInfoSubDivisionId { set; get; }

        [Display(Name = "License Expiry")]
        [Required(ErrorMessage = "Enter valid expiry date")]
        public DateTime? DriverLicenseExpiryDate { set; get; }

        [Display(Name = "Driver License#")]
        [Required(ErrorMessage = "Enter License#")]
        public string DriverLicenseNumber { set; get; }

        [Display(Name = "License Class")]
        [Required(ErrorMessage = "Class")]
        public string DriverLicenseType { set; get; }

        [Display(Name = "Consent accepted at")]
        public string ElectronicSignatureConsent { get; set; }

        [Display(Name = "I hereby agree to use my initials & signature")]
        [Required(ErrorMessage = "Select electronic consent")]
        public bool ElectronicSignatureConsentAccepted { get; set; }

        [Required(ErrorMessage = "Enter first name")]
        [Display(Name = "First Name")]
        public string FirstName { set; get; }

        public long? HomeDemogInfoAddressId { set; get; }

        public DemogInfoAddressModel HomeDemogInfoAddressModel { set; get; }

        [Display(Name = "Select your initials")]
        [Required(ErrorMessage = "Please select your initials")]
        public long? InitialsTextId { set; get; }

        [Display(Name = "Type initials")]
        [Required(ErrorMessage = "Please type initials")]
        [StringLength(3, MinimumLength = 1, ErrorMessage = "Please type valid initials")]
        public string InitialsTextValue { set; get; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Enter last name")]
        public string LastName { set; get; }

        [Display(Name = "Marital Status")]
        [Required(ErrorMessage = "Select marital status")]
        public MaritalStatusEnum? MaritalStatusId { set; get; }

        [Display(Name = "Middle Name")]
        public string MiddleName { set; get; }

        [Display(Name = "Military Service")]
        public YesNoEnum? MilitaryServiceId { set; get; }

        [Display(Name = "Nickname first")]
        [Required(ErrorMessage = "Enter nickname first")]
        public string NicknameFirst { set; get; }

        [Display(Name = "Nickname last")]
        [Required(ErrorMessage = "Enter nickname last")]
        public string NicknameLast { set; get; }

        [Display(Name = "Salutation")]
        [Required(ErrorMessage = "Select Salutation")]
        public SalutationEnum? SalutationId { set; get; }

        [Display(Name = "Select signature")]
        [Required(ErrorMessage = "Please select your signature")]
        public long? SignatureTextId { set; get; }

        [Display(Name = "Type signature")]
        [Required(ErrorMessage = "Please type signature")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Please type valid signature")]
        public string SignatureTextValue { set; get; }

        [Display(Name = "SSN#")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Please enter valid 9 digits SSN#")]
        [Required(ErrorMessage = "Enter SSN#")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Please enter 9 digit SSN#")]
        public string SSN { set; get; }

        public GenericStatusEnum? StatusId { set; get; }

        [Display(Name = "Suffix")]
        [Required(ErrorMessage = "Select Suffix")]
        public SuffixEnum? SuffixId { set; get; }

        public AspNetUserModel AspNetUserModel { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
