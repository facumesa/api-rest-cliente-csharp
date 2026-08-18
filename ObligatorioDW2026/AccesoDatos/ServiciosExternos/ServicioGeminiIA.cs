using AccesoDatos.Migrations;
using Microsoft.Extensions.Configuration;
using Negocio.Dominio;
using Negocio.InterfacesServicios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace AccesoDatos.ServiciosExternos
{
    public class ServicioGeminiIA : IServicioGeminiIA
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public ServicioGeminiIA(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Gemini:ApiKey"];
        }

        public ResultadoEvaluacionIA EvaluarAdecuacion(Telescopio telescopio, Montura montura, Camara camaraOpcional, Ocular ocularOpcional, ObjetoCeleste objetoCeleste)
        {
            try
            {
                // 1. Forzar protocolos TLS modernos (Google rechaza conexiones viejas de .NET)
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls13;

                var datosParaEvaluacion = new
                {
                    Telescopio = telescopio != null ? new { telescopio.Apertura_mm, telescopio.RelacionFocal, telescopio.DistanciaFocal_mm } : null,
                    Montura = montura != null ? new { montura.Tipo, montura.CargaUtil_kg, montura.EsComputarizado } : null,
                    Camara = camaraOpcional != null ? new { camaraOpcional.Resolucion, camaraOpcional.TipoSensor, camaraOpcional.TamanioPixel } : null,
                    Ocular = ocularOpcional != null ? new { ocularOpcional.Diametro_mm, ocularOpcional.AnguloVision_grados } : null,
                    Objeto_Celeste = objetoCeleste != null ? new { objetoCeleste.Nombre, objetoCeleste.MagnitudAparente, objetoCeleste.Tipo } : null
                };

                string datosJson = JsonSerializer.Serialize(datosParaEvaluacion);

                string prompt = $@"
                Actúa como un experto en astronomía. Evalúa la adecuación de la siguiente observacion astronómica basada en este JSON:\n{datosJson}\n
                REQUISITOS OBLIGATORIOS DE RESPUESTA:
                Debes responder estrictamente en formato JSON con la siguiente estructura exacta:
                {{
                    ""indicador"": ""VALOR"",
                    ""detalle"": ""TEXTO""
                }}
                Donde ""indicador"" DEBE ser únicamente uno de estos tres valores: IDEAL, ADECUADO o NO RECOMENDABLE.
                Donde ""detalle"" debe ser una breve explicación técnica de un máximo de 300 caracteres.";

                // Armamos el JSON exactamente como lo exige la API v1 de Google
                var bodyObjetos = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = prompt }
                            }
                        }
                    }
                };

                var opcionesSerializar = new JsonSerializerOptions { PropertyNamingPolicy = null };
                string jsonBody = JsonSerializer.Serialize(bodyObjetos, opcionesSerializar);

                string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3.5-flash:generateContent?key={_apiKey}";
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                // 2. EVITAR EL DEADLOCK: Task.Run evita que el hilo principal se congele en aplicaciones web
                HttpResponseMessage response = Task.Run(() => _httpClient.PostAsync(url, content)).GetAwaiter().GetResult();

                // 3. Capturar el error real si Google responde un código de error (como 400 o 403)
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = Task.Run(() => response.Content.ReadAsStringAsync()).GetAwaiter().GetResult();
                    return new ResultadoEvaluacionIA
                    {
                        Indicador = "NO RECOMENDABLE",
                        Detalle = $"Error Google ({response.StatusCode}): {errorContent}"
                    };
                }

                string jsonResponse = Task.Run(() => response.Content.ReadAsStringAsync()).GetAwaiter().GetResult();

                using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                {
                    string textoGeneradoJson = doc.RootElement
                        .GetProperty("candidates")[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text").GetString();

                    // Limpieza básica de Markdown si la IA lo incluyó
                    if (textoGeneradoJson.Contains("```json"))
                        textoGeneradoJson = textoGeneradoJson.Replace("```json", "").Replace("```", "").Trim();
                    else if (textoGeneradoJson.Contains("```"))
                        textoGeneradoJson = textoGeneradoJson.Replace("```", "").Trim();

                    var resultadoFinal = JsonSerializer.Deserialize<ResultadoEvaluacionIA>(textoGeneradoJson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return resultadoFinal ?? new ResultadoEvaluacionIA { Indicador = "NO RECOMENDABLE", Detalle = "Formato vacío devuelto por la IA." };
                }
            }
            catch (Exception ex)
            {
                // Si hay una falla física de red, el mensaje aparecerá directo en la pantalla en el campo Motivo
                return new ResultadoEvaluacionIA
                {
                    Indicador = "NO RECOMENDABLE",
                    Detalle = $"Excepción interna: {ex.Message} -> {ex.InnerException?.Message}"
                };
            }
        }
    }
}
