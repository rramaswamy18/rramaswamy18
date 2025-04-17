using ArchitectureLibraryDocumentModels;
using ArchitectureLibraryEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderApprovalModel : AuditInfoModel
    {
        public long? OrderApprovalId { set; get; }
        public long ClientId { set; get; }
        public string ApprovalOTPCode { set; get; }
        public string ApprovalOTPCompletedDateTime { set; get; }
        public string ApprovalOTPCreatedDateTime { set; get; }
        public string ApprovalOTPExpiryDateTime { set; get; }
        public int? ApprovalOTPExpiryDuration { set; get; }
        public OTPSendTypeEnum? ApprovalOTPSendTypeId { set; get; }
        public long ApprovalRequestedByPersonId { set; get; }
        public string ApprovalRequestedDateTime { set; get; }
        public long ApprovalRequestedForPersonId { set; get; }
        public string ApprovalStatusNameDesc { set; get; }
        public long? ApprovedByPersonId { set; get; }
        public string ApprovedDateTime { set; get; }
        public string ApproverComments { set; get; }
        public bool ApproverConsent { set; get; }
        public long? ApproverInitialsTextId { set; get; }
        public string ApproverInitialsTextValue { set; get; }
        public long? ApproverSignatureTextId { set; get; }
        public string ApproverSignatureTextValue { set; get; }
        public string Comments { set; get; }
        public long OrderHeaderId { set; get; }

        public string ApprovalRequestedByEmailAddress { set; get; }
        public string ApprovalRequestedByFirstName { set; get; }
        public string ApprovalRequestedByLastName { set; get; }
        public string ApprovalRequestedByTelephoneNumber { set; get; }
        public string ApprovalRequestedForEmailAddress { set; get; }
        public string ApprovalRequestedForFirstName { set; get; }
        public string ApprovalRequestedForLastName { set; get; }
        public string ApprovalRequestedForTelephoneNumber { set; get; }

        public bool ApprovalApproveOrDeny { set; get; }

        public string OrderFormHtmlString { set; get; }
        public string OrderHtmlString { set; get; }
    }
}
