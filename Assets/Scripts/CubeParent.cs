using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeParent {

	float x;
	float y;
	float z;

	public CubeParent(){
		x = 0;
		y = 0;
		z = 0;
	}

	public void setXYZ(float xx, float yy, float zz){
		x = xx;
		y = yy;
		z = zz;
	}

	public float getX(){

		return x;
	}

	public float getY(){

		return y;
	}

	public float getZ(){

		return z;
	}
}
