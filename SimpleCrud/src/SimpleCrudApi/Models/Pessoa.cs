using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleCrudApi.Models
{
    class BrDatetimeConverter : IsoDateTimeConverter
    {
        public BrDatetimeConverter()
        {
            base.DateTimeFormat = "dd/MM/yyyy";
        }
    }

    [Table("TB_PESSOA")]
    public class Pessoa
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPessoa { get; set; }
        [Column("NOME")]
        public string Nome { get; set; }
        [Column("SOBRENOME")]
        public string Sobrenome { get; set; }
        [Column("CPF")]
        public string Cpf { get; set; }
        [Column("DATANASCIMENTO")]
        [DataType(DataType.DateTime)]
        [JsonConverter(typeof(BrDatetimeConverter))]
        public DateTime DataDeNascimento { get; set; }
        [JsonIgnore]
        [Column("DATAREGISTRO")]
        [DataType(DataType.DateTime)]
        public DateTime DataDeRegistro { get; set; }
    }
}