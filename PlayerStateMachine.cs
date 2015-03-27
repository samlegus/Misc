using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Sam;

namespace Sam
{
	public class PlayerStateMachine : StateMachine 
	{
		//Enum is not supported by base class
		private enum PlayerStates : int
		{
			IDLE = 0,
			MOVE = 1,
			ATTACK = 2,
			STUNNED = 3
		};

		//Enum support implemented here, could just integers but enums are nice for clarity.
		void SetCurrentState(PlayerStates ID)
		{
			CurrentState = states[(int)ID];
		}

		void AddState(State state)
		{
			states.Add (state.ID, state);
		}
			        
		void Start()
		{
			IdleState idleState = new IdleState((int)PlayerStates.IDLE);
			AttackState attackState = new AttackState((int)PlayerStates.ATTACK);

			AddState (idleState);
			AddState (attackState);

			stateTransitions.Add( new StateTransition((int)PlayerStates.IDLE, 
			                                          (int)PlayerStates.ATTACK,
			                                          () => Input.GetMouseButtonDown (0)));


			stateTransitions.Add( new StateTransition((int)PlayerStates.ATTACK, 
			                                          (int)PlayerStates.IDLE,
			                                          () => Input.GetMouseButtonDown (1)));

			CurrentState = states[(int)PlayerStates.IDLE];
			CurrentMethod = CurrentState.Update;

		}
	}
	
	public class IdleState : State
	{
		public IdleState(int id) : base(id)
		{

		}
	
		public override void Start()
		{
			Debug.Log ("IDLE START");
		}

		public override void End()
		{
			Debug.Log ("IDLE END");
		}

		public override void Update()
		{
		
		}
	}

	public class AttackState : State
	{
		public AttackState(int id) : base (id)
		{
			
		}
		
		public override void Start()
		{
			Debug.Log ("ATTACK START");
		}
		
		public override void End()
		{
			Debug.Log ("ATTACK END");
		}
		
		public override void Update()
		{
			
		}
	}
}

