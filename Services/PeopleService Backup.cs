using BackendLearnUdemy.Controllers;

namespace BackendLearnUdemy.Services
{
    public class PeopleServiceBackup : IPeopleService
    {
        public bool Validate(People people)
        {
            return (!string.IsNullOrEmpty(people.Name)) && 
                people.Name.Length<100 && 
                people.Name.Length>3 ;
        }
    }
}
