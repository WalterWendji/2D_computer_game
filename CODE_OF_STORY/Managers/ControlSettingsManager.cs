using System;
using System.IO;
using System.Text.Json;
using CODE_OF_STORY.Core;

namespace CODE_OF_STORY.Managers;

public static class ControlSettingsManager
{
    private static readonly string SettingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "controlSettings.json");

    public static void SaveControls()
    {
        try
        {
            var options = new JsonSerializerOptions {WriteIndented = true};
            string jsonString = JsonSerializer.Serialize(PlayerControls.Settings, options);
            File.WriteAllText(SettingsFile, jsonString);
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Fehler beim Speichern der Steuerungseinstellungen: {ex.Message}");
        }
    }

    public static void LoadControls()
    {
        try
        {
            if(File.Exists(SettingsFile))
            {
                string jsonString = File.ReadAllText(SettingsFile);
                ControlSettings loadedSettings = JsonSerializer.Deserialize<ControlSettings>(jsonString);
                if(loadedSettings != null)
                {
                    PlayerControls.UpdateControls(loadedSettings);
                }
            }
            else
            {
                SaveControls();
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Fehler beim Speichern der Steuerungseinstellungen: {ex.Message}");
        }
    }
}
