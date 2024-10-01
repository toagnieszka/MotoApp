namespace MotoApp
{
    public class App : IApp
    {
        private readonly IEventMethod _eventMethod;
        private readonly IUserCommunication _userCommunication;

       

        public App(
            IEventMethod eventsHandler,
            IUserCommunication userCommunication
            )
        {
            _eventMethod = eventsHandler;
            _userCommunication = userCommunication;
        }

        public void Run()
        {            
            _eventMethod.UseEvents();
            _userCommunication.UseUserCommunication();
        }        
    }
}
