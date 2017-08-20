using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLoop
{
    public class StateSystem
    {
        Dictionary<string, IGameObject> _stateStore = new Dictionary<string, IGameObject>();
        IGameObject _CurrentState = null;
   
        public StateSystem()
        {

        }

        public void Update (double elapsedTime)
        {
            if (_CurrentState == null) { return; } else { _CurrentState.Update(elapsedTime); }
        }

        public void Render(int fbo_screen)
        {
            if (_CurrentState == null) { return; } else { _CurrentState.Render( fbo_screen); }
        }

        public void Render ()
        {
            if (_CurrentState == null) { return; } else { _CurrentState.Render(); }
        }

        public void AddState (string stateId, IGameObject state)
        {
            if (!_stateStore.ContainsKey(stateId)) {
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
            if (_stateStore.ContainsKey(stateId))
            {
               _CurrentState= _stateStore[stateId];
            }
            else
            {
                Exception e = new Exception("State "+stateId+ " Does Not Already Exist");
                throw e;
            }
        }

        public bool Exists (string stateId)
        {
            return _stateStore.ContainsKey(stateId);
        }

        public IGameObject CurrentState() => _CurrentState;
    }
}
