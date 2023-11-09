using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryModels
{
    public class SessionObjectModel
    {
        public Dictionary<string, AuthorizeTokenModel> AuthorizeTokenModels { set; get; }
        public string AspNetRoleId { set; get; }
        public string AspNetRoleName { set; get; }
        public string ControllerName { set; get; }
        public string ActionName { set; get; }
        public string AspNetUserId { set; get; }
        public long ClientId { set; get; }
        public long CorpAcctId { set; get; }
        public string EmailAddress { set; get; }
        public string FirstName { set; get; }
        public long InitialsTextId { set; get; }
        public string InitialsTextValue { set; get; }
        public string LastName { set; get; }
        public string LoggedInUserId { set; get; }
        public string NicknameFirst { set; get; }
        public string NicknameLast { set; get; }
        public long PersonId { set; get; }
        public string PhoneNumber    { set; get; }
        public long SignatureTextId { set; get; }
        public string SignatureTextValue { set; get; }
        public bool UserProfileAdultAge { set; get; }
        public bool UserProfileUpdated { set; get; }
    }
}
