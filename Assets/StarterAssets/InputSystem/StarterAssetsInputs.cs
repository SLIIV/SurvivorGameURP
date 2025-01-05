using Unity.VisualScripting;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public interface IPlayersInput
	{
		public bool CursorLocked {get; set;}
		public bool CursorInputForLook {get; set; }
	}

	public interface IPlayersUIInput
	{
		public bool Inventory {get;}
	}
	public class StarterAssetsInputs : MonoBehaviour, IPlayersInput, IPlayersUIInput
	{
		public bool CursorLocked { get { return cursorLocked; } set { cursorLocked = value; } }
		public bool CursorInputForLook { get { return cursorInputForLook; } set { cursorInputForLook = value; } }
		public bool Inventory {get { return inventory; } }

		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Attack Settings")]
		public bool attack;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		[Header("Inventory")]
		public bool inventory = false;
#if ENABLE_INPUT_SYSTEM

		public void OnMove(InputValue value)
		{
				MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
			else
			{
				LookInput(Vector2.zero);
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		public void OnAttack(InputValue value)
		{
			AttackInput(value.isPressed);
		}
		public void OnInventory(InputValue value)
		{
			InventoryInput();
		}
#endif

		private void Start()
		{
			SetCursorState(true);
		}

		public void MoveInput(Vector2 newMoveDirection)
		{
			if(cursorInputForLook)
			{
				move = newMoveDirection;
			}
			else
			{
				move = Vector2.zero;
			}

		}

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void AttackInput(bool newAttackState)
		{
			if(cursorLocked && cursorInputForLook)
			{
				attack = newAttackState;
			}
		}

		public void InventoryInput()
		{
			inventory = !inventory;
			SetCursorState(!inventory);
			if(inventory)
			{
				move = Vector2.zero;
			}
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
			Debug.Log(hasFocus);
		}
		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}


	}

}