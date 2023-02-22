using UnityEngine;

namespace Game.Runtime.Services.Rendering.Physics
{
    public class ColliderDebugFactory : MonoBehaviour
    {
        [SerializeField] private CircleCollider _prefab;

        public ICircleColliderDebugRenderer Create(float radius)
        {
            var obj = Instantiate(_prefab);
            obj.SetRadius(radius);

            return obj;
        }
    }
}