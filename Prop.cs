using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Appccelerate.StateMachine;

namespace JuggleLibNet
{
    public class CProp
    {
        public enum States : int
        {
            Dropped,
            Flight,
            InHand,
            Dwell,
            Catching,
            Tossing

        };

        public enum Events : int
        {
            Toss,
            Caught,
            Drop,
            Pickup,
            Tick
        }


        public int Id { get; set; }

        public  Initialize()
        {

            machine = new PassiveStateMachine<States, Events>();

            machine.DefineHierarchyOn(States.InHand)
                .WithHistoryType(HistoryType.None)
                .WithInitialSubState(States.Catching)
                .WithSubState(States.Dwell)
                .WithSubState(States.Tossing);

            machine.In(States.Dropped).ExecuteOnEntry(() => { Dropped(this, new DropReport))
                .On(Events.Pickup).Goto(States.Dwell)
                .Execute<CHand>((hand)=> { });
            machine.In(States.Dwell)
                .On(Events.Toss).Goto(States.Tossing)
                .On(Events.Drop).Goto(States.Dropped);
                
            machine.In(States.Flight).On(Events.Caught).Goto(States.Catching);
            machine.In(States.Catching).On(Events.Caught).Goto(States.Dwell);
            machine.In(States.Catching).On(Events.Drop).Goto(States.Dropped);
        }

        private void tossAction(Toss toss)
        {   
            
        }

        private void pickupAction(CHand hand)
        {

        }





        
        public string Name { get; set; }


        public bool IsDropped()     { return States.Dropped == CurrentState; }
        public bool IsInFlight()    { return States.Flight == CurrentState; }
        public bool IsInHand()      { return States.Catch == CurrentState || State.Dwell == CurrentState; }

        public void Toss(Toss toss)
        {
            ;

        public void Caught();

        public void Collision(DropReport drop);

        public void Pickup(CHand hand);

        public void Tick();
     
        public delegate void TossedHandler(CProp prop, CHand to);
        public event TossedHandler Tossed;

        
        public delegate void DroppedHandler(CProp prop, DropReport report);
        public event DroppedHandler Dropped;

        public delegate void CaughtHandler(CProp prop, CHand by);
        public event CaughtHandler Caught;

        public string ToString();

        private Toss toss_;
        private CHand hand_;

        private PassiveStateMachine<States, Events> machine = new PassiveStateMachine<States, Events>("Prop");


    }
}
