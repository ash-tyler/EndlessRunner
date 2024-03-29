﻿using UnityEngine;

public abstract class ControllableCharacterBehaviour : CharacterBehaviour
{
    #region Inspector Fields
    public TouchArcadeButton behaviourButton = null;
    #endregion

    protected Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}