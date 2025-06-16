using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using Game;
using UnityEngine;

namespace Manager
{
    public class TileDataManager
    {
        private readonly CharacterResources _characterResources = new CharacterResources();
        

        public void ReadTileData(string path)
        {
            string filePath = Path.Combine(Application.dataPath, path);

            try
            {
                using var reader = new StreamReader(filePath, Encoding.UTF8);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                // 跳过表头
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var charData = new CharacterData
                    {
                        Character = csv.GetField(1)?.Trim(),
                        Traditional = csv.GetField(2)?.Trim(),
                        Pinyin = csv.GetField(3)?.Trim(),
                        Components = csv.GetField(4)?.Trim().Split('，').ToList()
                    };

                    if (charData.Components != null)
                        foreach (var component in charData.Components)
                        {
                            if (!string.IsNullOrEmpty(component))
                                _characterResources.AddComponent(component);
                        }

                    _characterResources.AddCharacter(charData);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error reading character data: {ex.Message}");
            }
        }

        public CharacterResources GetCharacterResources()
        {
            return _characterResources;
        }
    }
}