using Assets.scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class hero : character
{
    

    private void Update()
    {
        if (_controlIsLocked)
            return;

        if (isGrounded) State = CharState.Idle;

        if (Input.GetButton("Horizontal") || Input.GetAxis("Horizontal") != 0) if(!_inBlock) Run(Input.GetAxis("Horizontal"));
        if (Input.GetButtonDown("Jump"))
            if (isGrounded)
                Jump(jumpForce);

        if (Input.GetButton("Punch")) if (!isGrounded) PunchInTheAir(); else StartCoroutine(Punch(CharState.PunchFrontHand));
        if (Input.GetButton("Punch2")) if (!isGrounded) PunchInTheAir(); else StartCoroutine(Punch(CharState.PunchBackHand));
        if (Input.GetButton("Block")) { if (isGrounded) Block(); } else _inBlock = false;
    }

   
}

