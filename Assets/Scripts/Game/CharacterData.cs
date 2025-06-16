using System.Collections.Generic;

namespace Game
{
    public class CharacterData
    {
        public string Character { get; set; }
        public string Traditional { get; set; }
        public string Pinyin { get; set; }
        public List<string> Components { get; set; }
    }
}
