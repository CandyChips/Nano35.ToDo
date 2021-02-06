using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nano35.ToDo.Api.Requests
{
    public interface IPipelineNode<TIn, TOut>
    {
        Task<TOut> Ask(TIn input);
    }

    public class Pipe<TIn, TOut>
    {
        private readonly List<IPipelineNode<TIn, TOut>> _nodes;

        public Pipe(List<IPipelineNode<TIn, TOut>> nodes)
        {
            _nodes = nodes;
        }

        public Pipe<TIn, TOut> AddNode(IPipelineNode<TIn, TOut> nextNode)
        {
            _nodes.Add(nextNode);
            return this;
        }
    }
}