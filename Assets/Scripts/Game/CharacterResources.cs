using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CharacterResources
{
    private List<CharacterData> characterDataList = new List<CharacterData>();
    private HashSet<string> componentList = new HashSet<string>();

    public void AddCharacter(CharacterData characterData)
    {
        characterDataList.Add(characterData);
    }

    public void AddComponent(string component) { 
        componentList.Add(component);
    }

    public List<CharacterData> GetAllCharacters()
    {
        return characterDataList;
    }

    public HashSet<string> GetComponents()
    {
        return componentList;
    }

    public int GetComponentCount()
    {
        return componentList.Count;
    }

}