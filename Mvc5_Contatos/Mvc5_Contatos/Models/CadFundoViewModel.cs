using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc5_Contatos.Models
{
    public class CadFundoViewModel
    {
        public FundoViewModel Fundo { get; set; }

        public IEnumerable<TipoFundoViewModel> TipoFundoList { get; set; }
    }
}