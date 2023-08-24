using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEnumerations
{
    public enum CodeTypeEnum : int
    {
        [Description("Address Type")]
        AddressType = 100,
        [Description("Building Type")]
        BuildingType = 200,
        [Description("Citizenship")]
        Citizenship = 300,
        [Description("Contact Type")]
        ContactType = 400,
        [Description("Document Category")]
        DocumentCategory = 500,
        [Description("Document Type")]
        DocumentType = 600,
        [Description("Email Address Type")]
        EmailAddressType = 700,
        [Description("Generic Status")]
        GenericStatus = 800,
        [Description("Keyword Type")]
        KeywordType = 900,
        [Description("Login Type")]
        LoginType = 1000,
        [Description("Marital Status")]
        MaritalStatus = 1100,
        [Description("Person Relationship")]
        PersonRelationship = 1200,
        [Description("Recipient Type")]
        RecipientType = 1300,
        [Description("Salutation")]
        Salutation = 1400,
        [Description("Status")]
        Status = 1500,
        [Description("Suffix")]
        Suffix = 1600,
        [Description("Telephone Type")]
        TelephoneType = 1700,
        [Description("Template Type")]
        TemplateType = 1800,
        [Description("User Status")]
        UserStatus = 1900,
        [Description("User Type")]
        UserType = 2000,
        [Description("Yes No")]
        YesNo = 2100,
        [Description("Social Media Type")]
        SocialMediaType = 2200,
        [Description("Signature Text")]
        SignatureText = 2300,
        [Description("Initials Text")]
        InitialsText = 2400,
        [Description("Code Type")]
        CodeType = 2500,
        [Description("Control Type")]
        ControlType = 2600,
        [Description("Data Type")]
        DataType = 2700,
        [Description("Response Type")]
        ResponseType = 2800,
        [Description("Enrollment Type")]
        EnrollmentType = 2801,
    }
}
