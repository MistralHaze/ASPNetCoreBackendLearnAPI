using BackendLearnUdemy.Controllers;

namespace BackendLearnUdemy.Services
{
    public class PeopleService : IPeopleService
    {
        public bool Validate(People people)
        {
            return !string.IsNullOrEmpty(people.Name);
        }
    }
}
