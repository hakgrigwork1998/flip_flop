using flip_flop_processor.Models;

namespace flip_flop_processor.Processors
{
    public interface IProcessor
    {
        IAuthProcessorResponse ProcessAuth(IAuthProcessor authProcessorModel);
        IFlipProcessorResponse ProcessFlip(IFlipProcessor flipProcessorModel);
    }
}
