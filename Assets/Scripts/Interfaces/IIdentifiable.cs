using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIdentifiable<T>
{
	T ID { get; }
}
