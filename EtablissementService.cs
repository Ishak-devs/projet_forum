using Forum.Models;
using System.Text.Json;

public class EtablissementService
{
    private readonly HttpClient _httpClient;

    public EtablissementService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Etablissements>> GetEtablissementsAsync(string region, string niveau)
    {
        string url = $"https://data.education.gouv.fr/api/records/1.0/search/?" +
                     $"dataset=fr-en-annuaire-education&q=&" +
                     $"rows=100&refine.region_academique={region}&" +
                     $"refine.nature_uai={niveau}";

        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode) return new List<Etablissements>();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonDocument.Parse(content);

        var etablissements = new List<Etablissements>();

        foreach (var record in result.RootElement.GetProperty("records").EnumerateArray())
        {
            var fields = record.GetProperty("fields");

            var etablissement = new Etablissements();

            if (fields.TryGetProperty("type_etablissement", out JsonElement typeElem))
                etablissement.nom_etablissement = typeElem.GetString();
            else
                etablissement.type_etablissement = "Inconnu";

            if (fields.TryGetProperty("region_academique", out JsonElement regionElem))
                etablissement.Region = regionElem.GetString();
            else
                etablissement.Region = "Non spécifiée";

            etablissements.Add(etablissement);
        }


        return etablissements;
    }
}
