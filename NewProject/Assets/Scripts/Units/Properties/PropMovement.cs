using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMovement : PropBase
{
    [HideInInspector]
    public EventOnMove OnMove = new EventOnMove();
    
}
