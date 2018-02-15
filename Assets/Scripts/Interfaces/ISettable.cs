using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISettable<T>
{
	void Set(T sentT);
}
