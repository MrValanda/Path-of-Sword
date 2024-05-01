#if UNITY_EDITOR
using UnityEngine;
using XftWeapon;

namespace Source.Scripts.WeaponModule.EditorUtils
{
    public class TrailEditorView : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            XWeaponTrail xWeaponTrail = GetComponent<XWeaponTrail>();
            if (xWeaponTrail == null) return;
            if (xWeaponTrail.PointStart == null || xWeaponTrail.PointEnd == null) return;

            Gizmos.DrawLine(xWeaponTrail.PointStart.position, xWeaponTrail.PointEnd.position);
        }
    }
}
#endif