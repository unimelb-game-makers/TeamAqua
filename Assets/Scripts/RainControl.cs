using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainControl : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem rainParticle;
    public const float RAINCHANCE = 0.3f;
    private Coroutine stopRoutine;
  