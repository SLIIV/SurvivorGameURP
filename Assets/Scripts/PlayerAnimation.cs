using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Animator _shadowsAnimator;
    [SerializeField] private Animator _handsAnimator;
    private StarterAssetsInputs _inputs;
    private PlayerAttack _attack;
    private enum AttackList
    {
        jab = 0,
        cross = 1
    }

    public void Start()
    {
        _inputs = gameObject.GetComponent<StarterAssetsInputs>();
        _attack = gameObject.GetComponent<PlayerAttack>();
        Animator[] animators = {_handsAnimator, _playerAnimator, _shadowsAnimator};
        _attack.OnPunch.AddListener(() => PlayerAttack(animators));
    }

    public void Update()
    {
        UpdatePlayerMoveAnimations(_playerAnimator, _inputs.move);
        UpdatePlayerMoveAnimations(_shadowsAnimator, _inputs.move);
        UpdatePlayerMoveAnimations(_handsAnimator, _inputs.move);
        UpdatePlayerAnimationState(_handsAnimator);

    }

    private void UpdatePlayerMoveAnimations(Animator animator, Vector2 value)
    {
        animator.SetFloat("MoveX", value.x);
        animator.SetFloat("MoveY", value.y);
    }

    private void UpdatePlayerAnimationState(Animator animator) {
        if(_inputs.move == Vector2.zero)
        {
            animator.SetBool("IsRunning", false);
        }
        else
        {
            animator.SetBool("IsRunning", true);
        }
    }

    private void PlayerAttack(Animator[] animator)
    {
        int randomPunch = Random.Range(0, 2);
        for(int i = 0; i < animator.Length; i++)
        {
            animator[i].SetTrigger("Punch");
            animator[i].SetFloat("RandomPunch", randomPunch);
        }

    }
}
