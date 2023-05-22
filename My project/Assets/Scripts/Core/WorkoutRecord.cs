using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class WorkoutRecord
    {
        public long Id { get; internal set; }
        public int User { get; internal set; }
        public int Coach { get; internal set; }
        public DateTime WorkoutDate { get; internal set; }
        public DateTime WorkoutTime { get; internal set; }
        public string Cost { get; internal set; }
    }
}