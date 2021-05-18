using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.ToDo.Processor.Services;

namespace Nano35.ToDo.Processor.UseCases
{
    public class TransactedUseCasePipeNode<TIn, TOut> : UseCasePipeNodeBase<TIn, TOut>
        where TIn : class, IRequest
        where TOut : class, IResult
    {
        private readonly ApplicationContext _context;
        public TransactedUseCasePipeNode(ApplicationContext context, IUseCasePipeNode<TIn, TOut> next) : base(next) => _context = context;
        public override async Task<UseCaseResponse<TOut>> Ask(TIn input, CancellationToken cancellationToken)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var response = await DoNext(input, cancellationToken);
                if (!response.IsSuccess())
                {
                    await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                    return response;
                }
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return response;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                return new UseCaseResponse<TOut>($"{typeof(TIn)} transaction refused.");
            }
        }
    }
}