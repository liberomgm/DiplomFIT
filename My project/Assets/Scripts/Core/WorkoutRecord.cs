using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class WorkoutRecord
    {
        public long Id { get; set; }
        public long User { get; set; }
        public long Coach { get; set; }
        public DateTime WorkoutDate { get; set; }
        public DateTime WorkoutTime { get; set; }
        public string Cost { get; set; }
    }
}