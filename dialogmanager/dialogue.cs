using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    [System.Serializable]
    public class dialogue 
    {
        public string name;
        [TextArea(3,10)]
        public string[] sentences;
    }
}
