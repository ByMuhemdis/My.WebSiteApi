

namespace My.Application.Exceptions.about
{
    public sealed class AboutNotfoundException : NotFoundException
    {
        public AboutNotfoundException(int id) : base($"The about with Id :{id} could not found.")
        {

        }
    }
}
