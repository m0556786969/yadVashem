using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace server;

public static class Program
{
    //פונקציה שמקבלת מספר סידורי ומחזירה את הכותרת
    public static string GetNum(int num)
    {
        string myString = num.ToString();
        // Path to your JSON file
        string filePath = @"./data.json";

        // Number to search for in the JSON file
        string targetCollectionSymbolization = myString;

        // Call the function to get the title
        string title = GetTitleForCollectionSymbolization(filePath, targetCollectionSymbolization);

        if (title != null)
        {
            return title;
        }
        else
        {
            return ($"Collection symbolization '{targetCollectionSymbolization}' not found.");
        }
    }

    //פונקציה שמחזירה את הכותרת של המספר הסידורי
    static string? GetTitleForCollectionSymbolization(string filePath, string targetCollectionSymbolization)
    {
        try
        {
            // Read JSON file content
            string jsonContent = File.ReadAllText(filePath);

            // Parse JSON content
            JObject jsonObject = JObject.Parse(jsonContent);

            // Search for the target collectionSymbolization
            foreach (var item in jsonObject)
            {
                if (item.Key == "collectionSymbolization" && item.Value?.ToString() == targetCollectionSymbolization)
                {
                    // If found, get the title
                    return jsonObject["title"]?.ToString();
                }
            }

            // If not found
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }

    //הולכת לתקיה לפי המספר ובודקת שמה את המספר של התמונה האחרונה 
    public static int LastNumImge(int num)
    {
        string myString = num.ToString();
        //string folderName = "images"; // Replace with the folder name you want to search
        string lastImage = GetLastImageNameInFolder(myString);
        if (lastImage.EndsWith("_XX.jpg"))
        {
            lastImage = lastImage.Substring(0, lastImage.Length - 3);
        }
        int lastImageName = int.Parse(lastImage.Substring(0, lastImage.Length - 4));
        if (lastImageName != 0)
        {
            return lastImageName;
        }
        else
        {
            return 0;
        }

    }
    //פונקציה שממיינת את התמונות בקובץ ומחזירה את מספר הקובץ האחרון
    static string GetLastImageNameInFolder(string folderName)
    {
        try
        {
            // Path to the folder where you want to search for the images
            string folderPath = $@"./images/{folderName}";

            // Check if the folder exists
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder '{folderName}' not found");
                return null;
            }

            // Get all image files in the folder
            var imageFiles = Directory.GetFiles(folderPath)
                .Where(file => file.ToLower().EndsWith(".jpg") || file.ToLower().EndsWith("_xx.jpg"))
                .OrderBy(file => Path.GetFileNameWithoutExtension(file)) // Sort the files by name (ascending order)
                .ToList();

            // Get the last image file name
            string lastImageName = imageFiles.LastOrDefault();

            if (lastImageName != null)
            {
                return Path.GetFileName(lastImageName);
            }
            else
            {
                Console.WriteLine($"No image found in folder '{folderName}'");
                return "0000";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }
}
