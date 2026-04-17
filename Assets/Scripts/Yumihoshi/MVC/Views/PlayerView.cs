// *****************************************************************************
// @author: Yumihoshi
// @email: xiaoyuesun915@gmail.com
// @creationDate: 2025/04/28 15:42
// @version: 1.0
// @description:
// *****************************************************************************

using UnityEngine;

namespace Yumihoshi.MVC.Views
{
    public class PlayerView : MonoBehaviour
    {
        private static readonly int IsMoveID = Animator.StringToHash("IsMove");
        private static readonly int InputXID = Animator.StringToHash("InputX");
        private static readonly int InputYID = Animator.StringToHash("InputY");
        private Actor _actor;
        private ActorInfo _actorInfo;
        private Animator _animator;
        private bool _tag;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _actor = GetComponent<Actor>();
            _actorInfo = _actor.ActorInfo;
        }

        private void Update()
        {
            if (_actor.ActorState == Actor.EActorState.Idle)
                _animator.SetBool(IsMoveID, false);
        }

        private void OnEnable()
        {
            _actor.MoveEvent.AddListener(PlayAnim);
        }

        private void OnDisable()
        {
            _actor.MoveEvent.RemoveListener(PlayAnim);
        }

        private void PlayAnim()
        {
            _animator.SetBool(IsMoveID, true);
            _animator.SetFloat(InputXID, _actorInfo.faceDirection.x);
            _animator.SetFloat(InputYID, _actorInfo.faceDirection.y);
        }
    }
}
