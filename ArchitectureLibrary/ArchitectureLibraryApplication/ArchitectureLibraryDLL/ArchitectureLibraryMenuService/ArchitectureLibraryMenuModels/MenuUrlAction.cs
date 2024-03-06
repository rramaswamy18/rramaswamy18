using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryMenuModels
{
    public class MenuUrlAction
    {
        //This class is to get it flattened out from KVP
        //There is no table associated with this
        //Alternatively, this can be part of Menu List class
        public long MenuListId { set; get; }
        public string AccessType { set; get; }
        public string ActionName { set; get; }
        public string AjaxUpdateTargetId { set; get; }
        public string ControllerName { set; get; }
        public string DropDownMenuName { set; get; }
        public string DropDownMenuType { set; get; }
        public string HrefTarget { set; get; }
        public string HrefWidth { set; get; }
        public string LinkText { set; get; }
        public string QueryString { set; get; }
        public string RedirectActionName { set; get; }
        public string RedirectControllerName { set; get; }
        public string RedirectQueryString { set; get; }
        public string RedirectMessage { set; get; }
        public long RedirectMessageId { set; get; }
    }
}
