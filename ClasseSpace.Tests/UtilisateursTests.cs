using Xunit;
using Forum.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ClasseSpace.Tests
{
    public class UtilisateursTests
    {
        [Fact]
        public void Nom_Doit_Avoir_Minimum_2_Caracteres()
        {
            var utilisateur = new Utilisateurs { Nom = "AAAAAAA" };

            var context = new ValidationContext(utilisateur);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(utilisateur, context, results, true);


            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage.Contains("au moins 8 caractères"));
        }
    }
}
