using APICatalogo.Services.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models
{
    [Table("Produtos")]
    public class Produto : IValidatableObject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(80)]
        [FirstLetterUppercase]
        public string Nome { get; set; }
        [Required]
        [MaxLength(200)]
        public string Descricao { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Preco { get; set; }
        [Required]
        [MaxLength(200)]
        public string ImagemUrl { get; set; }
        [Range(0,1000)]
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            string firstLetter = Nome.ToString()[0].ToString();
            if (firstLetter != firstLetter.ToUpper())
            {
                yield return 
                    new ValidationResult("A primeira letra do nome deve ser maiúscula.",new[] { nameof(Nome) });
            }
        }
    }
}
