using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace Sam
{
	public abstract class State 
	{
		public abstract void Start();
		public abstract void End();
		public abstract void Update();

		public State(int id)
		{
			this.ID = id;
		}
		public int ID;				// Make this an enum in the child class if possible.
	}
	
	public class StateTransition
	{
		public int From {get; private set;}
		public int To {get; private set;}
		public Func<bool> Condition {get; private set;}

		public StateTransition(int from, int to, Func<bool> condition)
		{
			From = from;
			To = to;
			Condition = condition;
		}
	}
	
	public abstract class StateMachine : MonoBehaviour 
	{
		protected delegate void Method();
		protected Method CurrentMethod = new Method( () => {});
		protected State CurrentState;
		protected List<StateTransition> stateTransitions = new List<StateTransition>();
		protected Dictionary< int, State> states = new Dictionary<int, State>();

		//If you override update don't forget to call ManageTransitions() and CurrentMethod()
		protected virtual void Update()
		{
			CurrentMethod();
			ManageTransitions ();
		
			//Debug.Log (CurrentState.ID);
		}
		
		protected void ManageTransitions()
		{
			foreach(StateTransition transition in stateTransitions)
			{
				if(CurrentState.ID == transition.From && transition.Condition() == true)
				{
					CurrentState.End();
					CurrentState = states[transition.To];
					CurrentState.Start();
					CurrentMethod = CurrentState.Update;
				}
			}
		}
	}
}

