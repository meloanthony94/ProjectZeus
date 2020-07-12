using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCommand : MonoBehaviour
{
    [SerializeField]
    public entityType.Type heroCommandType = entityType.Type.Rock;

    [SerializeField]
    public entityType.Type UpcomingCommandType = entityType.Type.Rock;
}
