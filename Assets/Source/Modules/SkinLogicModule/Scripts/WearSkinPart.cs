using System.Collections.Generic;
using System.Linq;
using Lean.Pool;
using Sirenix.OdinInspector;
using Source.Modules.SkinLogicModule.Scripts;
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
                Transform[] baseRoot = GetIntersectionBones(skinnedMeshRenderer);

                if (skinnedMeshRenderer.gameObject.TryGetComponent(out RootLinker rootLinker))
                {
                    rootLinker.Initialize(baseRoot, root.bones);
                }
                else
                {
                    skinnedMeshRenderer.gameObject.AddComponent<RootLinker>()
                        .Initialize(baseRoot, root.bones);
                }

                skinnedMeshRenderer.bones = root.bones;
                skinnedMeshRenderer.rootBone = root.rootBone;
            }
        }

        private Transform[] GetIntersectionBones(SkinnedMeshRenderer skinnedMeshRenderer)
        {
            Transform[] baseRoot = skinnedMeshRenderer.bones;

            string[] linkedRootNames = root.bones.Select(x => x.name).ToArray();

            List<string> usedNames = new List<string>(111);
            List<Transform> resultRootBone = new List<Transform>();

            foreach (var bone in baseRoot)
            {
                string boneName = bone.name;
                if (usedNames.Contains(boneName)) continue;

                if (linkedRootNames.Contains(boneName))
                {
                    resultRootBone.Add(bone);
                    usedNames.Add(boneName);
                }
            }

            baseRoot = resultRootBone.ToArray();
            return baseRoot;
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