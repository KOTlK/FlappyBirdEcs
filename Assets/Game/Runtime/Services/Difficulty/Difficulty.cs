using System;
using UnityEngine;

namespace Game.Runtime.Services.Difficulty
{
    [Serializable]
    public class Difficulty
    {
        [field: SerializeField] public float PipesMovementSpeed { get; set; }
        [field: SerializeField] public float BirdFallSpeed { get; set; }
        [field: SerializeField] public float BirdJumpForce { get; set; }
        [field: Range(0, 1)]
        [field: SerializeField] public float SoloPipeHeight { get; set; }
        [field: Range(0, 1)]
        [field: SerializeField] public float SoloPipeWidth { get; set; }
        [field: SerializeField] public int ScorePerPipe { get; set; }
    }
}