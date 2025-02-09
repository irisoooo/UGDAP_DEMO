using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAttackComponent : MonoBehaviour {

    public abstract void Attack(Vector2 startPoint, Vector2 forward);

}
