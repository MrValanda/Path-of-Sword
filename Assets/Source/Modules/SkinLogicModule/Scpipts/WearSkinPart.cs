using Lean.Pool;
using Sirenix.OdinInspector;
using Source.Scripts.SkinLogic;
using TMPro;
using UnityEngine;

namespace SkinLogic
{
    public class WearSkinPart : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer root;

        private SkinPart _skinPart;

        private GameObject _dressedObject;
        public SkinPart SkinPart => _skinPart;

        public void UpdateSkinPart(SkinPart newSkinPart)
        {
            _skinPart = newSkinPart;
            WearObject();
        }

        public void Clear()
        {
            if (_dressedObject != null)
            {
                LeanPool.Despawn(_dressedObject);
                _dressedObject = null;
            }
        }

        private void WearObject()
        {
            if (_dressedObject != null)
            {
                LeanPool.Despawn(_dressedObject);
            }

            _dressedObject = LeanPool.Spawn(_skinPart.SkinModel, root.transform.position, Quaternion.identity,
                root.transform);

            foreach (var skinnedMeshRenderer in _dressedObject.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                DestroyImmediate(skinnedMeshRenderer.rootBone.gameObject);
                skinnedMeshRenderer.bones = root.bones;
                skinnedMeshRenderer.rootBone = root.rootBone;
            }
        }
        
        [Button]
        private void WearObject(GameObject gb)
        {
            if (_dressedObject != null)
            {
                LeanPool.Despawn(_dressedObject);
            }

            _dressedObject = LeanPool.Spawn(gb, root.transform.position, Quaternion.identity,
                root.transform);

            foreach (var skinnedMeshRenderer in _dressedObject.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                skinnedMeshRenderer.bones = root.bones;
                skinnedMeshRenderer.rootBone = root.rootBone;
            }
        }
    }
}
