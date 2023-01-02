using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomNodeTypeListSO", menuName = "Scriptable Objects/Ship/Room Node Type List")]
public class RoomNodeTypeListSO : ScriptableObject
{
  [Space(10)]
  [Header("Room Node Type List")]
  [Tooltip("This list should be populated with all the RoomNodeTypeSO for the game")]
  public List<RoomNodeTypeSO> list;

  #region Validation
#if UNITY_EDITOR
  private void OnValidate()
  {
    HelperUtilities.ValidateCheckEnumerableValues(this, nameof(list), list);
  }
#endif
  #endregion
}
