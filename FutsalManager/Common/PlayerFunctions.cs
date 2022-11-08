using Attribute = FutsalManager.Models.Attribute;

namespace FutsalManager.Common;

public static class PlayerFunctions
{

    private static readonly List<string> AvailableNationalities = new()
    {
        "Afghan",
"Albanian",
"Algerian",
"American",
"Andorran",
"Angolan",
"Antiguans",
"Argentinean",
"Armenian",
"Australian",
"Austrian",
"Azerbaijani",
"Bahamian",
"Bahraini",
"Bangladeshi",
"Barbadian",
"Barbudans",
"Batswana",
"Belarusian",
"Belgian",
"Belizean",
"Beninese",
"Bhutanese",
"Bolivian",
"Bosnian",
"Brazilian",
"British",
"Bruneian",
"Bulgarian",
"Burkinabe",
"Burmese",
"Burundian",
"Cambodian",
"Cameroonian",
"Canadian",
"Cape Verdean",
"Central African",
"Chadian",
"Chilean",
"Chinese",
"Colombian",
"Comoran",
"Congolese",
"Costa Rican",
"Croatian",
"Cuban",
"Cypriot",
"Czech",
"Danish",
"Djibouti",
"Dominican",
"Dutch",
"East Timorese",
"Ecuadorean",
"Egyptian",
"Emirian",
"Equatorial Guinean",
"Eritrean",
"Estonian",
"Ethiopian",
"Fijian",
"Filipino",
"Finnish",
"French",
"Gabonese",
"Gambian",
"Georgian",
"German",
"Ghanaian",
"Greek",
"Grenadian",
"Guatemalan",
"Guinea-Bissauan",
"Guinean",
"Guyanese",
"Haitian",
"Herzegovinian",
"Honduran",
"Hungarian",
"I-Kiribati",
"Icelander",
"Indian",
"Indonesian",
"Iranian",
"Iraqi",
"Irish",
"Israeli",
"Italian",
"Ivorian",
"Jamaican",
"Japanese",
"Jordanian",
"Kazakhstani",
"Kenyan",
"Kittian and Nevisian",
"Kuwaiti",
"Kyrgyz",
"Laotian",
"Latvian",
"Lebanese",
"Liberian",
"Libyan",
"Liechtensteiner",
"Lithuanian",
"Luxembourger",
"Macedonian",
"Malagasy",
"Malawian",
"Malaysian",
"Maldivian",
"Malian",
"Maltese",
"Marshallese",
"Mauritanian",
"Mauritian",
"Mexican",
"Micronesian",
"Moldovan",
"Monacan",
"Mongolian",
"Moroccan",
"Mosotho",
"Motswana",
"Mozambican",
"Namibian",
"Nauruan",
"Nepalese",
"New Zealander",
"Ni-Vanuatu",
"Nicaraguan",
"Nigerian",
"Nigerien",
"North Korean",
"Northern Irish",
"Norwegian",
"Omani",
"Pakistani",
"Palauan",
"Panamanian",
"Papua New Guinean",
"Paraguayan",
"Peruvian",
"Polish",
"Portuguese",
"Qatari",
"Romanian",
"Russian",
"Rwandan",
"Saint Lucian",
"Salvadoran",
"Samoan",
"San Marinese",
"Sao Tomean",
"Saudi",
"Scottish",
"Senegalese",
"Serbian",
"Seychellois",
"Sierra Leonean",
"Singaporean",
"Slovakian",
"Slovenian",
"Solomon Islander",
"Somali",
"South African",
"South Korean",
"Spanish",
"Sri Lankan",
"Sudanese",
"Surinamer",
"Swazi",
"Swedish",
"Swiss",
"Syrian",
"Taiwanese",
"Tajik",
"Tanzanian",
"Thai",
"Togolese",
"Tongan",
"Trinidadian or Tobagonian",
"Tunisian",
"Turkish",
"Tuvaluan",
"Ugandan",
"Ukrainian",
"Uruguayan",
"Uzbekistani",
"Venezuelan",
"Vietnamese",
"Welsh",
"Yemenite",
"Zambian",
"Zimbabwean"
    };

    public static bool ValidateNationality(string nat) =>
        !string.IsNullOrWhiteSpace(nat) && AvailableNationalities.Find(n => n == nat) != null;

    public static int GetOverallRating(Player? player)
    {
        int rating = 0;

        if (player?.Attribute != null && player?.Position != null)
        {
            rating = player.Position.Pos.ToLower() switch
            {
                "goalkeeper" => rating = CalculateRating(player.Age,
                    new[] { player.Attribute.Tackling, player.Attribute.Vision, player.Attribute.Reaction }),
                "defender" => rating = CalculateRating(player.Age,
                    new[] { player.Attribute.Passing, player.Attribute.Tackling, player.Attribute.Reaction }),
                "pivot" => rating = CalculateRating(player.Age,
                    new[]
                    {
                        player.Attribute.Passing, player.Attribute.Vision, player.Attribute.Positioning,
                        player.Attribute.Dribbling
                    }),
                "winger" => rating = CalculateRating(player.Age,
                    new[]
                    {
                        player.Attribute.Passing, player.Attribute.Vision, player.Attribute.Shooting,
                        player.Attribute.Positioning, player.Attribute.Juggling
                    }),
                "universal" => rating = CalculateRating(player.Age,
                    new[]
                    {
                        player.Attribute.Dribbling, player.Attribute.Passing, player.Attribute.Shooting,
                        player.Attribute.Reaction, player.Attribute.Positioning, player.Attribute.Juggling
                    }),
                _ => rating
            };
        }

        return rating;
    }

    private static int CalculateRating(int playerAge, params int[] attr)
    {
        var avg = attr.Aggregate<int, double>(0, (current, val) => current + val);

        switch (playerAge)
        {
            case > 30 and < 35:
                avg *= 0.85;
                break;
            case > 35 and < 40:
                avg *= 0.70;
                break;
            case > 40:
                avg *= 0.65;
                break;
        }
        
        return (int)(avg / attr.Length);
    }
}