using Xunit;
using Forum.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ClasseSpace.Tests
{
    [Fact]
    public void Utilisateur_Prenom_Doivent_Avoir_Au_Moins_5_Caracteres()
    {
        // Arrange
        var utilisateur = new Utilisateurs
        {
            Nom = "TestNom",
            Prenom = "Al", // Trop court (2 caractères)
            Email = "test@email.com",
            Motdepasse = "motdepasse123"
        };

        var context = new ValidationContext(utilisateur);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(utilisateur, context, results, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage == "Le prénom doit avoir au moins 5 caractères.");
    }

}
