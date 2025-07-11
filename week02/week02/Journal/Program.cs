using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text;

class Entry
{
    public DateTime Timestamp { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Mood { get; set; } // 😊 😐 😢
}

class Journal
{
    private List<Entry> entries = new List<Entry>();
    private static Random rng = new Random();

    public void AddEntry(string prompt, string response, string mood)
    {
        entries.Add(new Entry
        {
            Timestamp = DateTime.Now,
            Prompt = prompt,
            Response = response,
            Mood = mood
        });
    }

    public void DisplayAll()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine($"[{entry.Timestamp}] {entry.Mood} - {entry.Prompt}\n{entry.Response}\n");
        }
    }

    public void SaveToJson(string filename)
    {
        string json = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filename, json);
    }

    public void LoadFromJson(string filename)
    {
        if (File.Exists(filename))
        {
            string json = File.ReadAllText(filename);
            entries = JsonSerializer.Deserialize<List<Entry>>(json) ?? new List<Entry>();
        }
    }

    public void SaveToCsv(string filename)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Timestamp,Mood,Prompt,Response");

        foreach (var entry in entries)
        {
            string safePrompt = entry.Prompt.Replace("\"", "\"\"");
            string safeResponse = entry.Response.Replace("\"", "\"\"").Replace("\n", " ");
            sb.AppendLine($"\"{entry.Timestamp}\",\"{entry.Mood}\",\"{safePrompt}\",\"{safeResponse}\"");
        }

        File.WriteAllText(filename, sb.ToString());
    }

    public void SearchByKeyword(string keyword)
    {
        foreach (var entry in entries)
        {
            if (entry.Response.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                entry.Prompt.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"[{entry.Timestamp}] {entry.Mood} - {entry.Prompt}\n{entry.Response}\n");
            }
        }
    }

    public void SearchByDate(DateTime date)
    {
        foreach (var entry in entries)
        {
            if (entry.Timestamp.Date == date.Date)
            {
                Console.WriteLine($"[{entry.Timestamp}] {entry.Mood} - {entry.Prompt}\n{entry.Response}\n");
            }
        }
    }

    public void ShowRandomEntry()
    {
        if (entries.Count == 0)
        {
            Console.WriteLine("Nenhuma entrada para mostrar.");
            return;
        }

        var entry = entries[rng.Next(entries.Count)];
        Console.WriteLine($"📖 Flashback de [{entry.Timestamp}]: {entry.Mood} - {entry.Prompt}\n{entry.Response}\n");
    }

    public void ShowStats()
    {
        Console.WriteLine($"📊 Total de entradas: {entries.Count}");

        var porMes = new Dictionary<string, int>();
        var humorContagem = new Dictionary<string, int>();

        foreach (var e in entries)
        {
            string mes = e.Timestamp.ToString("yyyy-MM");
            if (!porMes.ContainsKey(mes)) porMes[mes] = 0;
            porMes[mes]++;

            if (!humorContagem.ContainsKey(e.Mood)) humorContagem[e.Mood] = 0;
            humorContagem[e.Mood]++;
        }

        Console.WriteLine("Entradas por mês:");
        foreach (var kv in porMes)
            Console.WriteLine($"  {kv.Key}: {kv.Value}");

        Console.WriteLine("Humor mais frequente:");
        foreach (var kv in humorContagem)
            Console.WriteLine($"  {kv.Key}: {kv.Value}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        string filenameJson = "diario.json";
        bool sair = false;

        Console.WriteLine("Bem-vindo ao Diário Digital! 📝");

        while (!sair)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Escrever nova entrada");
            Console.WriteLine("2. Mostrar todas as entradas");
            Console.WriteLine("3. Salvar em JSON");
            Console.WriteLine("4. Carregar de JSON");
            Console.WriteLine("5. Salvar em CSV");
            Console.WriteLine("6. Buscar por palavra-chave");
            Console.WriteLine("7. Buscar por data");
            Console.WriteLine("8. Relembrar uma entrada aleatória");
            Console.WriteLine("9. Ver estatísticas");
            Console.WriteLine("0. Sair");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Write("Escreva seu prompt: ");
                    string prompt = Console.ReadLine();

                    Console.Write("Sua resposta: ");
                    string resposta = Console.ReadLine();

                    Console.Write("Como você se sente hoje? 😊 😐 😢: ");
                    string humor = Console.ReadLine();

                    journal.AddEntry(prompt, resposta, humor);
                    Console.WriteLine("✅ Entrada adicionada!");
                    break;

                case "2":
                    journal.DisplayAll();
                    break;

                case "3":
                    journal.SaveToJson(filenameJson);
                    Console.WriteLine("💾 Diário salvo em JSON.");
                    break;

                case "4":
                    journal.LoadFromJson(filenameJson);
                    Console.WriteLine("📂 Diário carregado.");
                    break;

                case "5":
                    Console.Write("Nome do arquivo CSV (ex: diario.csv): ");
                    string arquivoCsv = Console.ReadLine();
                    journal.SaveToCsv(arquivoCsv);
                    Console.WriteLine("✅ Exportado para CSV.");
                    break;

                case "6":
                    Console.Write("Digite a palavra-chave: ");
                    string palavra = Console.ReadLine();
                    journal.SearchByKeyword(palavra);
                    break;

                case "7":
                    Console.Write("Digite a data (yyyy-mm-dd): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime data))
                    {
                        journal.SearchByDate(data);
                    }
                    else
                    {
                        Console.WriteLine("❌ Data inválida.");
                    }
                    break;

                case "8":
                    journal.ShowRandomEntry();
                    break;

                case "9":
                    journal.ShowStats();
                    break;

                case "0":
                    sair = true;
                    break;

                default:
                    Console.WriteLine("❌ Opção inválida.");
                    break;
            }
        }

        Console.WriteLine("👋 Até logo!");
    }
}
