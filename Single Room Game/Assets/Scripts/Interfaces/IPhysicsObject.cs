using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPhysicsObject {

    void Throw(Vector3 direction);
    void PlaceBack();
	
}
