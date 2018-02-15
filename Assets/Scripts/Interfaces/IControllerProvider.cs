using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllerProvider
{
	Ball GetBall();
	TouchRadius GetTouchRadius();
}
