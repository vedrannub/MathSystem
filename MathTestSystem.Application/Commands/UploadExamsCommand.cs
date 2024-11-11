using MediatR;
using System.IO;

namespace MathTestSystem.Application.Commands
{
    public class UploadExamsCommand : IRequest<bool>
    {
        public Stream XmlStream { get; }

        public UploadExamsCommand(Stream xmlStream)
        {
            XmlStream = xmlStream;
        }
    }
}
