using UnityEngine;

namespace Assets.Scripts.Controller
{
    public class AnimationController : MonoBehaviour
    {
        public AnimationController() { }
        public void PlayerAnimation(Animator animator, string action, bool state)
        {
            animator.SetBool(action, state);
        }

        public void FlipPlayer(SpriteRenderer sr, bool fliped)
        {
            sr.flipX = fliped;
        }

    }
}
