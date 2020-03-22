using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlotProvider
{
    GameObject GetActiveSlot();
    Animator GetGlowAnimator();
    GameObject GetActiveGlow();
}