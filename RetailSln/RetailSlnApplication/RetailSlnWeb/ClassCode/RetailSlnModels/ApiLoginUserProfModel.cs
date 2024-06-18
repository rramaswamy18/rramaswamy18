using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ApiLoginUserProfModel
    {
        public long ClientId { set; get; }
        public string AspNetRoleId { set; get; }
        public string AspNetRoleName { set; get; }
        public string AspNetUserId { set; get; }
        public string EmailAddress { set; get; }
        public string FirstName { set; get; }
        public string JwtToken { set; get; }
        public string LastName { set; get; }
        public string LoginPassword { set; get; }
        public string NicknameFirst { set; get; }
        public string NicknameLast { set; get; }
        public long PersonId { set; get; }
        public string PhoneNumber { set; get; }
        public short TelephoneCode { set; get; }
        public long TelephoneCountryId { set; get; }
        public CorpAcctModel CorpAcctModel { set; get; }
        public long DefaultDeliveryDemogInfoCountryId { set; get; }
        public List<KeyValuePair<long, string>> DeliveryCountrys { set; get; }
        public List<KeyValuePair<long, List<KeyValuePair<long, string>>>> DeliveryCountryStates { set; get; }
        public List<ApiCodeDataModel> DeliveryMethods { set; get; }
        public List<ApiCodeDataModel> PaymentModes { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
