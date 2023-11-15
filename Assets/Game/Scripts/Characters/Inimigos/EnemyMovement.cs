using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Movement[] MovementPatterns;
    private Dictionary<string, Movement> MovementDictionary;

    private void Awake()
    {
        MovementDictionary = new Dictionary<string, Movement>();
        foreach(Movement mov in MovementPatterns)
        {
            MovementDictionary.Add(mov.movementTag, mov);
        }
    }

    public void FollowMovementPattern(string moveTag, Transform thisTransform, float[] times = null, float[] pauses = null)
    {
        Sequence movementSequencce = DOTween.Sequence();
        Movement thisMovement = MovementDictionary[moveTag];
        float[] movementTimes = (times == null ? thisMovement.times : times);
        float[] movementPauses = (pauses == null ? thisMovement.pauses : pauses);

        for (int movimento = 0; movimento < thisMovement.endPoints.Length; movimento++)
        {
            movementSequencce.Append(thisTransform.DOMove(thisMovement.endPoints[movimento].position, movementTimes[movimento]));
            if(movementPauses[movimento] != 0)
            {
                movementSequencce.AppendInterval(movementPauses[movimento]);
            }
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
