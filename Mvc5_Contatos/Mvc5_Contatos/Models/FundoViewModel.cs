using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc5_Contatos.Models
{
    public class FundoViewModel
    {
        [Required(ErrorMessage = "Preenchimento obrigatório.")]
        [DisplayName("Código")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [DisplayName("Nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [DisplayName("CNPJ")]        
        public string Cnpj { get; set; }
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [DisplayName("Tipo de Fundo")]
        public int CodigoTipo { get; set; }
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [DisplayName("Tipo de Fundo")]
        public string NomeTipo { get; set; }
        [DisplayName("Patrimônio")]

        public decimal? Patrimonio { get; set; }
    }
}