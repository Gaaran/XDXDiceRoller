using System;
using System.Collections.Generic;
using System.Text;

namespace XDX_dice_roller
{

    class Context
    {
        private State state = null;

        public Context(State _state)
        {
            this.TransitionTo(_state);
        }

        public void TransitionTo(State _state)
        {
            this.state = _state;
            this.state.SetContext(this);
        }

        public void Request1()
        {
            this.state.Handle1();
        }

        public void Request2()
        {
            this.state.Handle2();
        }
    }

    abstract class State
    {
        protected Context context;

        public void SetContext(Context _context)
        {
            this.context = _context;
        }

        public abstract void Handle1();
        public abstract void Handle2();
    }
    /*
    none,
    normal,
    savage,
    hexagon,
    wta,
    aventure,
    MAXVALUE,
    */

    class Normal : State
    {
        public override void Handle1()
        {
            throw new NotImplementedException();
        }

        public override void Handle2()
        {
            throw new NotImplementedException();
        }
    }

    class Hexagon : State
    {
        public override void Handle1()
        {
            throw new NotImplementedException();
        }

        public override void Handle2()
        {
            throw new NotImplementedException();
        }
    }

    class Savage : State
    {
        public override void Handle1()
        {
            throw new NotImplementedException();
        }

        public override void Handle2()
        {
            throw new NotImplementedException();
        }
    }

    class WTA : State
    {
        public override void Handle1()
        {
            throw new NotImplementedException();
        }

        public override void Handle2()
        {
            throw new NotImplementedException();
        }
    }

    class Aventure : State
    {
        public override void Handle1()
        {
            throw new NotImplementedException();
        }

        public override void Handle2()
        {
            throw new NotImplementedException();
        }
    }

}
