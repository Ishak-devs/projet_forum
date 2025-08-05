using Xunit;
using Forum.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ClasseSpace.Tests
{
    public class UtilisateursTests
    {
        [Fact]
        public void Nom_Doigt_Avoir_Minimum_5_Caracteres()
        {
            var utilisateur = new Utilisateurs { Nom = "ABCD" }; // 4 caractères, ça doit échouer

            var context = new ValidationContext(utilisateur);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(utilisateur, context, results, true);

            Assert.False(isValid); // On veut que la validation échoue
            Assert.Contains(results, r => r.ErrorMessage.Contains("au moins 5 caractères"));
        }
    }
}
