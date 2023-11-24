using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singleton;

public class EnemyMovement : Singleton<EnemyMovement>
{
    [SerializeField] Movement[] MovementPatterns;
    private Dictionary<string, Movement> MovementDictionary;

    private new void Awake()
    {
        MovementDictionary = new Dictionary<string, Movement>();
        foreach(Movement mov in MovementPatterns)
        {
            MovementDictionary.Add(mov.movementTag, mov);
        }
    }

    public void FollowMovementPattern(string moveTag, Transform thisTransform, float[] times = null, float[] pauses = null, bool loop = false)
    {
        Sequence movementSequence = DOTween.Sequence();
        Movement thisMovement = MovementDictionary[moveTag];
        float[] movementTimes = (times == null ? thisMovement.times : times);
        float[] movementPauses = (pauses == null ? thisMovement.pauses : pauses);

        for (int movimento = 0; movimento < thisMovement.endPoints.Length; movimento++)
        {
            movementSequence.Append(thisTransform.DOMove(thisMovement.endPoints[movimento].position, movementTimes[movimento]));
            if (movementPauses[movimento] != 0)
            {
                movementSequence.AppendInterval(movementPauses[movimento]);
            }
        }

        if (loop)
        {
            movementSequence.SetLoops(-2, LoopType.Yoyo);
        }
    }

    [System.Serializable]
    public class Movement
    {
        public string movementTag;
        public Transform[] endPoints;
        public float[] times;
        public float[] pauses;
    }
}
