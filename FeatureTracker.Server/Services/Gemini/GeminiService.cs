namespace FeatureTracker.Server.Services.Gemini;

public class GeminiService
{
    #region Dependecies
    private readonly HttpClient _httpClient;
    #endregion

    #region Properties
    private readonly string _apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=";
    private readonly string _apiKey = "AIzaSyDGyptNM_5KEAQ8UlePTBbW2f7nGRgwgSo";
    private readonly string _context = @"
        Você é um assistente especializado em gerar títulos claros e diretos para novas funcionalidades de sistemas e produtos. 
        Seu objetivo é criar títulos que resumam com precisão a funcionalidade descrita, destacando suas principais características e benefícios de forma objetiva e concisa. 
        Evite títulos com linguagem excessivamente promocional ou apelativa. 
        O título deve ser curto, fácil de entender e refletir exatamente o que a funcionalidade faz.
        Retorne apenas o título gerado!";
    #endregion

    #region Constructor
    public GeminiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    #endregion

    #region Endpoints
    public async Task<string> GenerateTitle(string observation)
    {
        ArgumentNullException.ThrowIfNull(observation);

        observation = $"{_context}\n Informações: {observation}";

        var request = new HttpRequestMessage(HttpMethod.Post, $"{_apiUrl}{_apiKey}");
        request.Headers.Add("Accept", "application/json");

        var content = new GeminiRequest
        {
            system_instruction = new GeminiSystemInstruction
            {
                parts = [new GeminiPart { text = $"{_context}" }]
            },

            contents = [ new GeminiContent { parts =
            [
                new() { text = observation }
            ]}
            ]
        };

        request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(content), System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();

        using var doc = System.Text.Json.JsonDocument.Parse(responseBody);
        var root = doc.RootElement;

        var text = root
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();

        return string.IsNullOrWhiteSpace(text) ? "No title generated" : text.Trim();
    }
    #endregion
}

public class GeminiRequest
{
    public GeminiSystemInstruction system_instruction { get; set; }
    public List<GeminiContent> contents { get; set; }
}

public class GeminiSystemInstruction
{
    public List<GeminiPart> parts { get; set; }
}

public class GeminiContent
{
    public List<GeminiPart> parts { get; set; }
}

public class GeminiPart
{
    public string text { get; set; }
}
