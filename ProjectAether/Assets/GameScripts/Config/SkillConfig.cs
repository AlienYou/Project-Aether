using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectAether.Config
{
    public class SkillConfig : IConfigRow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public float Cooldown { get; set; }
    }
}