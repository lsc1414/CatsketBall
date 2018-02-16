using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class AndroidStoreHandler : StoreHandler
{
	public override string PopCornProductID { get { return "com.dsa.catsketballest.popcorn"; } }
	public override string ThreeDeeGlassesID { get { return "com.dsa.catsketballtest.3d_glasses"; } }

	public override void RestoreReciepts(IExtensionProvider sentProvider)
	{
		throw new NotImplementedException();
	}
}
