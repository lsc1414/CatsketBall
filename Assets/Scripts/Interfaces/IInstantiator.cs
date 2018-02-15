using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInstantiator
{
	T InstantiateObject<T>();
}
