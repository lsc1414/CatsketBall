using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDependant<T>
{
	System.Func<T> GetVital { get; set; }
}
