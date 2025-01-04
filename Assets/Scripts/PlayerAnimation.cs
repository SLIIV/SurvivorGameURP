using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Animator _shadowsAnimator;
    [SerializeField] private Animator _handsAnimator;
    private IPersonController _personController;
    private StarterAssetsInputs _inputs;
    private PlayerAttack _attack;
    private enum AttackList
    {
        jab = 0,
        cross = 1
    }

    public void Start()
    {
        _personController = gameObject.GetComponent<IPersonController>();
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
        UpdatePlayerAnimationState(_playerAnimator);
        UpdatePlayerAnimationState(_shadowsAnimator);
        UpdatePlayerAnimationState(_handsAnimator);
        UpdatePlayerSprintAnimations(_playerAnimator, _inputs.sprint);
        UpdatePlayerSprintAnimations(_shadowsAnimator, _inputs.sprint);
        UpdatePlayerSprintAnimations(_handsAnimator, _inputs.sprint);
        UpdatePlayerJumpStages(_shadowsAnimator);
        UpdatePlayerJumpStages(_playerAnimator);
        UpdatePlayerJumpStages(_handsAnimator);

    }

    private void UpdatePlayerMoveAnimations(Animator animator, Vector2 value)
    {
        animator.SetFloat("MoveX", value.x);
        animator.SetFloat("MoveY", value.y);
    }

    private void UpdatePlayerSprintAnimations(Animator animator, bool value)
    {
        animator.SetBool("Sprint", value);
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

    private void UpdatePlayerJumpStages(Animator animator)
    {
        animator.SetBool("Grounded", _personController.IsGrounded);
        animator.SetFloat("VerticalVelocity", _personController.VerticalVelocity);
    }
}
