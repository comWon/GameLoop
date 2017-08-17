using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLoop
{
    class StateSystem
    {
        Dictionary<string, IGameObject> _stateStore = new Dictionary<string, IGameObject>();
        IGameObject _CurrentState = null;
        IGameObject _nullIGameObject = null;

        public void Update (double elapsedTime)
        {
            if (_CurrentState == null) { return; } else { _CurrentState.Update(elapsedTime); }
        }

        public void Render ()
        {
            if (_CurrentState == null) { return; } else { _CurrentState.Render(); }
        }

        public void AddState (string stateId, IGameObject state)
        {
            if (_stateStore.Where(f => f.Key == stateId).Count() == 0) {
                _stateStore.Add(stateId, state);
            }
            else
            {
                Exception e = new Exception("State Already Exists");
                throw e;
            }

        }

        public void ChangeState(string stateId)
        {
            if (_stateStore.Where(f => f.Key == stateId).Count() != 0)
            {
               _CurrentState= _stateStore.First(f => f.Key==stateId).Value;
            }
            else
            {
                Exception e = new Exception("State "+stateId+ " Does Not Already Exist");
                throw e;
            }
        }
    }
}
