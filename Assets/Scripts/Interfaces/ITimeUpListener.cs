using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ITimeUpDependant
{
	Action TimeUpAction { get; set; }
	void OnTimeIsUp();
}
