using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    public static TransitionController Instance;

    private Dictionary<BrigeTail, TriggersCount> _transitions;

    private TriggersCount PlayerBrigeTail;
    private TriggersCount TargetBrigeTail;

    public Action<BrigeTail, BrigeTail> OnTransitionComplete;

    [Serializable]
    public class TriggersCount
    {
        public int Count = 0;
        public BrigeTail Tail;
        public bool ReadyToTransit;

        public TriggersCount(int count, BrigeTail targetTail)
        {
            Count = count;
            Tail = targetTail;
        }

        public TriggersCount()
        {
            Count = 0;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _transitions = new Dictionary<BrigeTail, TriggersCount>();
        PlayerBrigeTail = new TriggersCount();
        TargetBrigeTail = new TriggersCount();
    }

    private void Start()
    {
        TouchInputHandler.Instance.OnDoubleTap += TryTransit;
    }

    private void OnDisable()
    {
        TouchInputHandler.Instance.OnDoubleTap -= TryTransit;
    }

    public void Contact(BrigeTail brigeTail, bool isEdgeTrigger)
    {
        if (brigeTail.IsPlayerHere)
        {
            PlayerBrigeTail.ReadyToTransit = true;
            PlayerBrigeTail.Tail = brigeTail;
        }
        else if (PlayerBrigeTail.Tail.TargetTail)
        {
            if (brigeTail.CanTransitToThisCircle && PlayerBrigeTail.Tail.TargetTail == brigeTail)
            {
                TargetBrigeTail.Tail = brigeTail;
                TargetBrigeTail.ReadyToTransit = true;
            }
        }
        //if (brigeTail.IsPlayerHere)
        //{
        //    Debug.Log("SDSAD");
        //    PlayerBrigeTail.Tail = brigeTail;
        //    PlayerBrigeTail.Count++;
        //}
        //else if (PlayerBrigeTail.Tail.TargetTail)
        //{
        //    if (brigeTail.CanTransitToThisCircle && PlayerBrigeTail.Tail.TargetTail == brigeTail)
        //    {
        //        TargetBrigeTail.Tail = brigeTail;
        //        TargetBrigeTail.Count++;
        //    }
        //}
    }

    public void Break(BrigeTail brigeTail, bool isEdgeTrigger)
    {
        if (brigeTail == PlayerBrigeTail.Tail)
        {
            if (isEdgeTrigger)
            {
                PlayerBrigeTail.ReadyToTransit = false;
            }
        }
        else if (brigeTail == TargetBrigeTail.Tail)
        {
            if (isEdgeTrigger)
            {
                TargetBrigeTail.ReadyToTransit = false;
            }
        }
        //if (brigeTail == PlayerBrigeTail.Tail)
        //{
        //    PlayerBrigeTail.Count--;
        //}
        //else if (brigeTail == TargetBrigeTail.Tail)
        //{
        //    TargetBrigeTail.Count--;
        //}
    }

    private void TryTransit()
    {
        if (PlayerBrigeTail.ReadyToTransit && TargetBrigeTail.ReadyToTransit)
        {
            Debug.Log("Ïîïûòêà ïåğåõîäà");
            if (!PlayerBrigeTail.Tail.CheckTransitionPossibility())
            {
                Debug.Log("ÍÅÓÑÏÅØÍÎ");
                return;
            }
            Debug.Log("ÓÑÏÅØÍÎ");
            PlayerBrigeTail.Count = 0;
            TargetBrigeTail.Count = 0;
            PlayerBrigeTail.Tail.TryTransit(false);
            TargetBrigeTail.Tail.TryTransit(true);
            OnTransitionComplete?.Invoke(PlayerBrigeTail.Tail, TargetBrigeTail.Tail.TargetTail);
        }

        //if (PlayerBrigeTail.Count > 0 && TargetBrigeTail.Count > 0)
        //{
        //    Debug.Log("Ïîïûòêà ïåğåõîäà");
        //    if (!PlayerBrigeTail.Tail.CheckTransitionPossibility())
        //    {
        //        Debug.Log("ÍÅÓÑÏÅØÍÎ");
        //        return;
        //    }
        //    Debug.Log("ÓÑÏÅØÍÎ");
        //    PlayerBrigeTail.Count = 0;
        //    TargetBrigeTail.Count = 0;
        //    PlayerBrigeTail.Tail.TryTransit(false);
        //    TargetBrigeTail.Tail.TryTransit(true);
        //    OnTransitionComplete?.Invoke(PlayerBrigeTail.Tail, TargetBrigeTail.Tail.TargetTail);
        //}
    }
}