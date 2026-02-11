using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ShoppingCartWIPHdrModel
    {
        public long ShoppingCartWIPHdrId { set; get; }
        public long ClientId { set; get; }
        public long CorpAcctLocationId { set; get; }
        public long CreatedForPersonId { set; get; }
        public long PersonId { set; get; }
        public float SeqNum { set; get; }
    }
}
