using System.Collections.Generic;
using UnityEngine;

namespace AutomateNPC {
    public class NpcModelData {
        public string name;

        public NpcModelData()
        {
            name = getNpcModelName();
        }

        public string getNpcModelName()
        {
            return "";
        }

    }
}
